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
                return RedirectToAction("MisServicios", "Servicio");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult AgregarPlato(int Id)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                Servicio ser = s.GetServicioPorId(Id);
                List <Plato> lp = s.GetPlatos();

                ViewBag.Servicio = ser;
                ViewBag.Platos = lp;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult AgregarPlato(int id, int cantidad, int slcPlato)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {

                s.AgregarPlato(id, slcPlato, cantidad );
                return RedirectToAction("MisServicios", "Servicio" );
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult VerPlatos(int Id)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {

                List<PlatoCantidad> misPlatos = s.GetPlatosCantidadPrServicio(Id);
                return View(misPlatos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public IActionResult CerrarServicio(int id)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                s.CerrarServicio(id);
                return RedirectToAction("MisServicios", "Servicio");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult AltaLocal() // funcion para pedir un servicio de tipo Local.
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                List<Mozo> lm = s.GetMozos();

                ViewBag.Mozos = lm;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult AltaLocal(int numeroMesa, int slcMozo, int cantidadComensales)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                s.AltaLocal(idLogueado, numeroMesa, slcMozo, cantidadComensales);
                return RedirectToAction("MisServicios", "Servicio");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ServiciosMasCaros()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {

                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> servicios = s.GetServiciosMasCarosPorIdCliente(idLogueado);
                return View(servicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
