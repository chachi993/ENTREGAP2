using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObligatorioP2;
using ObligatorioP2MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP2MVC.Controllers
{
    public class HomeController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? logueadoId = HttpContext.Session.GetInt32("LogueadoId");
            
            if(logueadoId != null)
            {
                string rol = HttpContext.Session.GetString("LogueadoRol");
                ViewBag.msg = $"Bienvenido/a, usted inició sesión como {rol}";
            }
            else {
                ViewBag.msg = "Inicie sesión";
            }
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Persona login = s.ValidarDatosLogin(username, password);
            string rol; 
            if(login != null)
            {

                if(login is Cliente)
                {
                    rol = "Cliente";
                }
                else if(login is Repartidor)
                {
                    rol = "Repartidor";
                }
                else
                {
                    rol = "Mozo";
                }

                HttpContext.Session.SetInt32("LogueadoId", login.Id);
                HttpContext.Session.SetString("LogueadoRol", rol);

                ViewBag.msg = "Bienvenido";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.msg = "Datos no válidos";
                return View();
            }
        }
         public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
