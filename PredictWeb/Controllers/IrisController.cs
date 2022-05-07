using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PredictWeb.Models;
using PredictWeb.Service;

namespace PredictWeb.Controllers
{
    public class IrisController : Controller
    {
        private readonly IrisService _irisService;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Result(IrisViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _irisService.GetPredictAsync(model);
                return View(model);
            }
            else return View(model);
        }
    }
}
