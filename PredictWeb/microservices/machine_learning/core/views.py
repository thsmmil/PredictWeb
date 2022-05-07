from django.http import HttpResponse
from rest_framework.response import Response
from rest_framework.views import APIView
from sklearn.datasets import load_iris
from sklearn import svm
from sklearn.linear_model import LogisticRegression
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import accuracy_score
from sklearn.model_selection import train_test_split
from sklearn.model_selection import cross_val_score
from sklearn.model_selection import cross_val_predict

from .serializers import MeasuresSerializer, PredictionSerializer
from .models import PredictionResult

# Create your views here.
class MeasuresAPIView(APIView):
    def post(self, request):
        serializer = MeasuresSerializer(data=request.data)
        serializer.is_valid(raise_exception=True)

        #Start scikit-learn
        X, y = load_iris(return_X_y=True)

        #Criação dos modelos preditivos
        clf = svm.SVC(kernel='linear',C=1.0, random_state=25)
        rfc = RandomForestClassifier(random_state=0)
        lr = LogisticRegression(random_state=12, max_iter=150)

        #Retornando base após cross-validation
        clf_pred = cross_val_predict(clf, X, y, cv=5)
        rfc_pred = cross_val_predict(rfc, X, y, cv=5)
        lr_pred = cross_val_predict(lr, X, y, cv=5)

        #Separação dos dados em treinamento e teste
        X_train_clf, X_test_clf, y_train_clf, y_test_clf = train_test_split(X, clf_pred, test_size=0.4, random_state=13)
        X_train_rfc, X_test_rfc, y_train_rfc, y_test_rfc = train_test_split(X, rfc_pred, test_size=0.4, random_state=13)
        X_train_lr, X_test_lr, y_train_lr, y_test_lr = train_test_split(X, lr_pred, test_size=0.4, random_state=13)
        #Sem cross validation
        X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.4, random_state=13)

        #Treinar cada modelo
        clf.fit(X_train_clf, y_train_clf)
        rfc.fit(X_train_rfc, y_train_rfc)
        lr.fit(X_train_lr, y_train_lr)

        #Predição com os dados do usuário
        results =clf.predict([serializer.data])
        rfc.predict(serializer.data)
        lr.predict(serializer.data)

        #pontuação de cada modelo
        score_clf = accuracy_score(clf.predict(X_test_clf), y_test_clf)
        score_rfc =accuracy_score(rfc.predict(X_test_rfc), y_test_rfc)
        score_lr =accuracy_score(lr.predict(X_test_lr), y_test_lr)
        
        return Response(results)




class PredictAPIView(APIView):
    def get(self, request):
        serializer = PredictionSerializer(data=request.query_params)
        serializer.is_valid(raise_exception=True)

        #Start scikit-learn
        X, y = load_iris(return_X_y=True)

        #Criação dos modelos preditivos
        clf = svm.SVC(kernel='linear',C=1.0, random_state=25)
        rfc = RandomForestClassifier(random_state=0)
        lr = LogisticRegression(random_state=12, max_iter=150)

        #Retornando base após cross-validation
        clf_pred = cross_val_predict(clf, X, y, cv=5)
        rfc_pred = cross_val_predict(rfc, X, y, cv=5)
        lr_pred = cross_val_predict(lr, X, y, cv=5)

        #Separação dos dados em treinamento e teste
        X_train_clf, X_test_clf, y_train_clf, y_test_clf = train_test_split(X, clf_pred, test_size=0.4, random_state=13)
        X_train_rfc, X_test_rfc, y_train_rfc, y_test_rfc = train_test_split(X, rfc_pred, test_size=0.4, random_state=13)
        X_train_lr, X_test_lr, y_train_lr, y_test_lr = train_test_split(X, lr_pred, test_size=0.4, random_state=13)
        #Sem cross validation
        X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.4, random_state=13)

        #Treinar cada modelo
        clf.fit(X_train_clf, y_train_clf)
        rfc.fit(X_train_rfc, y_train_rfc)
        lr.fit(X_train_lr, y_train_lr)

        #Predição com os dados do usuário
        results =clf.predict([serializer.data])
        rfc.predict(serializer.data)
        lr.predict(serializer.data)

        #pontuação de cada modelo
        score_clf = accuracy_score(clf.predict(X_test_clf), y_test_clf)
        score_rfc =accuracy_score(rfc.predict(X_test_rfc), y_test_rfc)
        score_lr =accuracy_score(lr.predict(X_test_lr), y_test_lr)

        return HttpResponse(results)