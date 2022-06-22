using Microsoft.AspNetCore.Mvc;
using ObligatorioP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP2MVC.Controllers
{
    public class PersonaController : Controller
    {
        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AltaCliente()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AltaCliente(string nombre, string apellido, string email, string user, string pass )
        {
            if(s.AltaCliente(nombre, apellido, email, user, pass) != null){

                ViewBag.msg = "Alta Correcta";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.msg = "Alta Incorrecta";
            }
            return View();
        }
       
    }
}
