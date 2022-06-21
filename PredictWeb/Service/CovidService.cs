using Newtonsoft.Json;
using PredictWeb.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PredictWeb.Service
{
    public class CovidService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly Uri uri = new Uri("http://localhost:8000/api/covid");

        public async Task<CovidViewModel> GetPredictAsync(CovidViewModel model)
        {
            try
            {
                
                var dataRequest = new
                {
                    Genero = model.Genero,
                    Idade = model.Idade,
                    ASC = model.ASC,
                    DM = model.DM,
                    HAS = model.HAS,
                    Cir_Cardiaca_Previa = model.Cir_Cardiaca_Previa,
                    Cir_Combinada = model.Cir_Combinada,
                    Cir_Urgencia = model.Cir_Urgencia,
                    CEC = model.CEC,
                    Hb_pre = model.Hb_pre,
                    Crea_pre = model.Crea_pre,
                    Congenito = model.Congenito,
                    Revascularizacao = model.Revascularizacao,
                    Transplante = model.Transplante,
                    Valvular = model.Valvular
                };
                
                var dataJson = new StringContent(JsonConvert.SerializeObject(dataRequest), Encoding.UTF8, "application/json");
                
                var response = await httpClient.PostAsync(uri, dataJson);

                string content = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        var result = JsonConvert.DeserializeObject<CovidViewModel>(content);
                        if (result != null) model.Result = result.Result;
                    }
                }

                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return model;
            }
        }
    }
}
