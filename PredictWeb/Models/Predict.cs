using System;
using System.ComponentModel.DataAnnotations;

namespace PredictWeb.Models
{
    public class IrisViewModel
    {

        [Required(ErrorMessage = "O campo 'Nome' é obrigatório")]
        [UIHint("TamanhoSepala")]
        [Display(Name = "TamanhoSepala")]
        public double SepalLength { get; set; }

        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório")]
        [UIHint("LarguraSepala")]
        [Display(Name = "LarguraSepala")]
        public double SepalWidth { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatório")]
        [UIHint("TamanhoPetala")]
        [Display(Name = "TamanhoPetala")]
        public double PetalLength { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatório")]
        [UIHint("LarguraPetala")]
        [Display(Name = "LarguraPetala")]
        public int PetalWidth { get; set; }

        public int? Result { get; set; }
        public string? ResultDesc
        {
            get
            {
                switch (Convert.ToInt16(Result))
                {
                    case 0: return "Setosa";
                        break;
                    case 1: return "Versicolor";
                        break;
                    case 2: return "Virginica";
                        break;
                    default : return null;
                }
            }
        }

    }
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
