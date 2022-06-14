using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PredictWeb.Models;
using PredictWeb.Service;

namespace PredictWeb.Controllers
{
    public class CovidController : Controller
    {
        private readonly CovidService covidService;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Result(CovidViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await covidService.GetPredictAsync(model);
                return View(model);
            }
            else return View(model);
        }
    }
}