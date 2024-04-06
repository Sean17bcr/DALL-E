using DALL_E.Models;
using DALL_E.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace DALL_E.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConsulta _consulta;

        public HomeController(IConsulta consultaModel)
        {
            _consulta = consultaModel;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Input input)
        {
            ResponseModel responseModel = null;

            responseModel = await _consulta.GenerateImage(input);
            ViewBag.Imagen = responseModel;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}