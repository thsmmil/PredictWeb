from django.db import models

# Create your models here.
class Measures(models.Model):
    sepal_length = models.DecimalField(max_digits=5, decimal_places=2)
    sepal_width = models.DecimalField(max_digits=5, decimal_places=2)
    petal_length = models.DecimalField(max_digits=5, decimal_places=2)
    petal_width = models.DecimalField(max_digits=5, decimal_places=2)
    

class PredictionResult(models.Model):
    target = models.PositiveSmallIntegerField()