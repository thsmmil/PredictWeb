using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PredictWeb.Models;
using PredictWeb.Service;
using System;

namespace PredictWeb.Controllers
{
    public class CovidController : Controller
    {
        private readonly CovidService covidService = new CovidService();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Result(CovidViewModel model)
        {
            model.ASC_text = model.ASC_text.Replace(".", ",");
            model.Hb_pre_text = model.Hb_pre_text.Replace(".", ",");
            model.Crea_pre_text = model.Crea_pre_text.Replace(".", ",");
            model.Valvular_text = model.Valvular_text.Replace(".", ",");
            if (ModelState.IsValid)
            {
                model.ASC = Convert.ToDouble(model.ASC_text);
                model.Hb_pre = Convert.ToDouble(model.Hb_pre_text);
                model.Crea_pre = Convert.ToDouble(model.Crea_pre_text);
                model.Valvular = Convert.ToDouble(model.Valvular_text);
                var result = await covidService.GetPredictAsync(model);
                return View(model);
            }
            else return View(model);
        }
    }
}