using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System;
using PredictWeb.Models;
using System.Threading.Tasks;

namespace PredictWeb.Service
{
    public class IrisService
    {
        private readonly string url = "http://localhost/8080/api/machine/measures/";
        private readonly HttpClient _httpClient = new HttpClient();


        public async Task<IrisViewModel> GetPredictAsync(IrisViewModel model)
        {
            try
            {
                var measure = new Dictionary<string, string>
                {
                    {"sepalLength", model.SepalLength.ToString()},
                    {"sepalWidth", model.SepalWidth.ToString()},
                    {"petalLength", model.PetalLength.ToString()},
                    {"petalWidth", model.PetalWidth.ToString()}
                };
                var content = new FormUrlEncodedContent(measure);

                var response = await _httpClient.PostAsync(url, content);
                var responseString = String.Empty;
                if (response.IsSuccessStatusCode)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseString))
                    {
                        IrisViewModel deserializedClass = JsonConvert.DeserializeObject<IrisViewModel>(responseString);
                        model.Result = deserializedClass.Result;
                        return model;
                    }
                    return model;
                }
                return model;

            }
            catch (Exception ex)
            {
                return model;
            }
        }
    }
}
