using System;
using System.ComponentModel.DataAnnotations;

namespace PredictWeb.Models
{
    public class CovidViewModel
    {
        public int Genero { set; get; }
        public int Idade { set; get; }
        public double ASC { set; get; }
        public string ASC_text { set; get; }
        public int DM { set; get; }
        public int HAS { set; get; }
        public int Cir_Cardiaca_Previa { set; get; }
        public int Cir_Combinada { set; get; }
        public int Cir_Urgencia { set; get; }
        public int CEC { set; get; }
        public double Hb_pre { set; get; }
        public string Hb_pre_text { set; get; }
        public double Crea_pre { set; get; }
        public string Crea_pre_text { set; get; }
        public int Congenito { set; get; }
        public int Revascularizacao { set; get; }
        public int Transplante { set; get; }
        public double Valvular { set; get; }
        public string Valvular_text { set; get; }
        public bool? Result { get; set; }

        #nullable enable
        public string? ResultDesc {
            get
            {
                switch (Result)
                {
                    
                    case true: 
                        return "Alto Risco";
                        break;
                    case false: 
                        return "Baixo Risco";
                        break;
                    default:
                        return "Não avaliado";
                }
            }
        }
        
    }
}
