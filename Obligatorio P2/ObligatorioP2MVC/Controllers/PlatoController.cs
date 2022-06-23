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
        public IActionResult Like(int id) //cuando se hace like en un plato, se llama a la funcion de sistema de Likear y se redirige
        {
            s.Likear(id);
            return RedirectToAction("Biblioteca");
        }
        public IActionResult Biblioteca()
        {
                List<Plato> listaPlatos = s.GetPlatosOrdenadosPorNombre();
                return View(listaPlatos);
            

        }
    }

    
}
