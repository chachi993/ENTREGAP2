using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP2MVC.Controllers
{
    public class PlatoController : Controller
    {
        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Like(int id)
        {
            s.Likear(id);
            return RedirectToAction("Biblioteca");
        }
        public IActionResult Biblioteca()
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                List<Plato> listaPlatos = s.GetPlatosOrdenadosPorNombre();
                return View(listaPlatos);
            }

            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Plato> listaPlatos = s.GetPlatosOrdenadosPorNombre(idLogueado);
                return View(listaPlatos);
                
            }
            return RedirectToAction("Index", "Home");
        }
    }

    
}
