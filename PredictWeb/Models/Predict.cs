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
                    case 1: return "Versicolor";
                    case 2: return "Virginica";
                }
                return null;
            }
        }

    }
}
