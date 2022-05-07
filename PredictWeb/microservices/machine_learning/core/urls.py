from django.urls import path
from .views import MeasuresAPIView, PredictAPIView

urlpatterns = [
    path('predict', PredictAPIView.as_view()),
    path('measures', MeasuresAPIView.as_view())
    
]