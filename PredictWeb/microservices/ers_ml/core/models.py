from django.db import models

class CovidPred(models.Model):
    Genero = models.IntegerField
    Idade = models.IntegerField
    ASC = models.FloatField
    DM = models.IntegerField
    HAS = models.IntegerField
    Cir_Cardiaca_Previa = models.IntegerField
    Cir_Combinada = models.IntegerField
    Cir_Urgencia = models.IntegerField
    CEC = models.IntegerField
    Hb_pre = models.FloatField
    Crea_pre = models.FloatField
    Congenito = models.IntegerField
    Revascularizacao = models.IntegerField
    Transplante = models.IntegerField
    Valvular = models.IntegerField
    Result = models.BooleanField

