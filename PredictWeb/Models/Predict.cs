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
        public Int16 Genero { set; get; }
        public Int16 Idade { set; get; }
        public Double ASC { set; get; }
        public Int16 DM { set; get; }
        public Int16 HAS { set; get; }
        public Int16 Cir_Cardiaca_Previa { set; get; }
        public Int16 Cir_Combinada { set; get; }
        public Int16 Cir_Urgencia { set; get; }
        public Int16 CEC { set; get; }
        public Double Hb_pre { set; get; }
        public Double Crea_pre { set; get; }
        public Int16 Congenito { set; get; }
        public Int16 Revascularizacao { set; get; }
        public Int16 Transplante { set; get; }
        public Int16 Valvular { set; get; }
        public Boolean? Result { get; set; }

        public String? ResultDesc {
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
                        return null;
                }
            }
        }
        
    }
}
