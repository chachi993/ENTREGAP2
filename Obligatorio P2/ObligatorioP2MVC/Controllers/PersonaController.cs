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
        //se crea instancia de sistema (singleton)
        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            return View();
        }
        //http get para la vista del formulario de registro de persona
        public IActionResult AltaCliente()
        {
            return View();
        }
        //metodo httpPost - redirecciona a login en caso de buen alta y pasa msg de error en caso de que no
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
