from rest_framework import serializers
from .models import CovidPred

class CovidSerializer(serializers.ModelSerializer):
    class Meta:
        model = CovidPred
        fields = '__all__'


#Add class img