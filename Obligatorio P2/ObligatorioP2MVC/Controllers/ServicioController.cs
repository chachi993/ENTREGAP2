using ObligatorioP2;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ObligatorioP2MVC.Controllers
{
    public class ServicioController : Controller
    {
        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MisServicios()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {

                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> misServicios = s.GetServiciosPorCliente(idLogueado);
                return View(misServicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult MisServicios(DateTime f1, DateTime f2)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                if(f1 != null && f2 != null) { 

                    int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                    List<Servicio> misServicios = s.GetServiciosPorClienteEntreFechas(idLogueado, f1, f2);
                    return View(misServicios);

                }
                else
                {
                    int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                    List<Servicio> misServicios = s.GetServiciosPorClienteEntreFechas(idLogueado, new DateTime (01-01-1900), new DateTime (01-01-2023));
                    return View(misServicios);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AltaDelivery()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                List<Repartidor> lr = s.GetRepartidores();

                ViewBag.Repartidores = lr;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpPost]
        public IActionResult AltaDelivery(string direccion, int distancia, int slcRepartidor)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {

                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                s.AltaDelivery(idLogueado, direccion, distancia, slcRepartidor);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


    }
}
