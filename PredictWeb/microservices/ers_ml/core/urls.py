from django.urls import path
from .views import CovidAPIView, ImageAPIView

urlpatterns = [
    path('covid', CovidAPIView.as_view())
]