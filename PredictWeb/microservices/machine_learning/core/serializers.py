from dataclasses import field, fields
from rest_framework import serializers
from .models import PredictionResult, Measures

class MeasuresSerializer(serializers.ModelSerializer):
    class Meta:
        model = Measures
        fields = '__all__'

class PredictionSerializer(serializers.ModelSerializer):
    class Meta:
        model = PredictionResult
        fields = '__all__'