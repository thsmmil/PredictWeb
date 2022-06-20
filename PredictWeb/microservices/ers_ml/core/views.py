from django.http import HttpResponse
from rest_framework.response import Response
from rest_framework.views import APIView
from rest_framework.renderers import JSONRenderer

from .serializers import CovidSerializer
from .models import CovidPred

#region LIBRARIES
import scipy
import itertools
import numpy as np
import pandas as pd
from scipy.stats import ttest_ind
from sklearn.linear_model import LogisticRegression
from sklearn.neighbors import KNeighborsClassifier
from sklearn.svm import SVC
from sklearn.ensemble import RandomForestClassifier
from sklearn.ensemble import GradientBoostingClassifier
from imblearn.over_sampling import SMOTE, ADASYN 
from sklearn.preprocessing import MinMaxScaler
from sklearn.model_selection import GridSearchCV, RepeatedStratifiedKFold, train_test_split
from imblearn.pipeline import Pipeline as imbpipeline
from sklearn.metrics import classification_report
from sklearn.metrics import ConfusionMatrixDisplay
from sklearn.metrics import accuracy_score, f1_score, precision_score, recall_score
from scipy import stats
#endregion

class CovidAPIView(APIView):
    def post(self, request):
        #Constants
        RANDOM_STATE = 42
        ALPHA = 0.05
        REPEATS = 5
        K = 2
        serializer = CovidSerializer(data = request.data)
        serializer.is_valid(raise_exception=True)
        print('Entrou na view')
        print(type(request.data))
        print(type(request))
        print(request)
        #Read file
        df = pd.read_excel('./core/ERS.xlsx',
                  usecols=['Gênero', 'Idade', 'Peso', 'Altura', 'IMC', 'ASC', 'DM', 'HAS', 'Classif Cirurgia', 
                           'Cir Cardiaca Prévia', 'Cir Combinada', 'Cir Urgência', 'CEC',
                           'Hb pré', 'Crea pré', 'HTF', 'Quant CH'])

        df.columns = df.columns.str.replace(' ','_')
        
        #Variaveis Continuas e Categoricas
        continuas = [var for var in df.columns if df[var].dtype != 'O']
        categoricas = [var for var in df.columns if df[var].dtype == 'O' and df[var].nunique() < 10]
        for col in categoricas:
            dummies = pd.get_dummies(df[col])
            df.drop(col,axis=1,inplace=True)
            df = pd.concat([df,dummies],axis=1)
        output = 'HTF'

        if output == 'HTF':

            print('Each row in the dataset have one of two possible classes: no (False) and yes (True).')
            
            #df['HTF'].value_counts().plot(kind='bar')
            
            y = df['HTF'] == 1

        else:
            
            print('Each row in the dataset have one of two possible classes: low risk (False) and high risk (True). The figure below shows the distribution of Quant CH')
                
            #df['Quant_CH'].value_counts().plot(kind='bar')
                
            y = df['Quant_CH'] >= 2 # Ajustando alvo para uma Classificação Binária
            
        X = df.drop(columns=['HTF','Quant_CH'])
        
        minority = min(y.value_counts())
        majority = max(y.value_counts())
        print(minority,'/',majority,'=',minority/majority)

        X_train, X_test, y_train, y_test = train_test_split(X, 
                                                    y,
                                                    stratify=y,
                                                    test_size=0.3,
                                                    random_state=RANDOM_STATE)
        for col in ['Hb_pré', 'Crea_pré']:
            median = X[col].median()
            X[col].fillna(median,inplace=True)
            X[col].fillna(median,inplace=True)
        
        def drop(column):
            X.drop(column,inplace=True,axis=1)
        for col in ['Combinada', 'Altura', 'Peso', 'IMC']:
            drop(col)
        df = pd.concat([X_train,y_train],axis=1)
        # def inspection(column):
        #     unstack = df.groupby([output, column])[column].size().unstack()
        #     unstack = unstack/unstack.sum()
        #     unstack.transpose().plot.bar(stacked=True)
        #     print(unstack)
        drop('outras')
        drop('Aorta')

        smote = SMOTE(random_state=RANDOM_STATE)
        X_res, y_res = smote.fit_resample(X,y)
        clf = RandomForestClassifier(max_depth=4, random_state=42)
        clf.fit(X_res,y_res)
        print(type(serializer.data) )
        print(serializer)
        teste_re = [
            request.data['Genero'],
            request.data['Idade'],
            request.data['ASC'],
            request.data['DM'],
            request.data['HAS'],
            request.data['Cir_Cardiaca_Previa'],
            request.data['Cir_Combinada'],
            request.data['Cir_Urgencia'],
            request.data['CEC'],
            request.data['Hb_pre'],
            request.data['Crea_pre'],
            request.data['Congenito'],
            request.data['Revascularizacao'],
            request.data['Transplante'],
            request.data['Valvular'],
        ]
        print(teste_re)
        result = clf.predict([teste_re])
        serializer.data['Result'] = result
        print(serializer)
        print(type(result))
        result = result.item()
        request.data['Result'] = result
        return Response(request.data)




class ImageAPIView(APIView):
    def get(self, request):
        import shap
        shap.initjs()

        explainer = shap.TreeExplainer(clf)

        shap_values = explainer.shap_values(X_test)
        shap.summary_plot(shap_values[1], X_test, max_display=15)

        idx = 3
        X_idx = X_test.iloc[[idx]]
        shap.force_plot(explainer.expected_value[1], shap_values[1][idx,:], X_idx)
