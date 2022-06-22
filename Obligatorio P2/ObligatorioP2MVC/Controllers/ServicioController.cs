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
                if (misServicios.Count().Equals(0))
                {
                    ViewBag.msgServicios = "No hay servicios";
                }
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

                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> misServicios = s.GetServiciosPorClienteEntreFechas(idLogueado, f1, f2);
                if (misServicios.Count().Equals(0))
                {
                    ViewBag.msgServicios = "No hay servicios";
                }
                return View(misServicios);

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
                if(s.AltaDelivery(idLogueado, direccion, distancia, slcRepartidor))
                {
                    return RedirectToAction("MisServicios", "Servicio");

                }
                else
                {
                    List<Repartidor> lr = s.GetRepartidores();
                    ViewBag.Repartidores = lr;
                    ViewBag.msg = "No se crea el delivery";
                    return View();
                }               
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

                if(s.AgregarPlato(id, slcPlato, cantidad))
                {
                    return RedirectToAction("MisServicios", "Servicio" );
                }
                else
                {
                    Servicio ser = s.GetServicioPorId(id);
                    List<Plato> lp = s.GetPlatos();

                    ViewBag.Servicio = ser;
                    ViewBag.Platos = lp;
                    ViewBag.msg = "Error - no se agrega plato";
                    return View();
                }

            }
            
            return RedirectToAction("Index", "Home");
            

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
                if(s.AltaLocal(idLogueado, numeroMesa, slcMozo, cantidadComensales))
                {
                    s.AltaLocal(idLogueado, numeroMesa, slcMozo, cantidadComensales);
                    return RedirectToAction("MisServicios", "Servicio");

                } else
                {
                    List<Mozo> lm = s.GetMozos();
                    ViewBag.Mozos = lm;
                    ViewBag.msg = "No se crea el servicio local";
                    return View();
                }
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
                if (servicios.Count().Equals(0))
                {
                    ViewBag.msg = "No hay servicios";
                }
                      
                return View(servicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ServiciosPorPlato()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                List<Plato> lp = s.GetPlatos();
                List<Servicio> servicios = new List<Servicio>();

                ViewBag.Platos = lp;
                return View(servicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult ServiciosPorPlato(string slcPlato)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> servicios= s.GetServiciosSegunNombreDePlato(slcPlato, idLogueado);
                List<Plato> lp = s.GetPlatos();
                ViewBag.Platos = lp;
                if (servicios.Count().Equals(0))
                {
                    ViewBag.msg = "No hay servicios";
                }
                return View(servicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ServiciosLocalesAtendioMozo()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Mozo")
            {

                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> misServicios = s.GetServiciosPorMozo(idLogueado);
                if (misServicios.Count().Equals(0))
                {
                    ViewBag.msgMozo = "No hay servicios";

                }
                
                return View(misServicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ServiciosLocalesAtendioMozo(DateTime f1, DateTime f2)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (HttpContext.Session.GetString("LogueadoRol") == "Mozo")
            {
                if (f1 != null && f2 != null)
                {
                    int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                    List<Servicio> misServicios = s.GetServiciosLocalesPorMozoEntreFechas(idLogueado, f1, f2);
                    if (misServicios.Count().Equals(0))
                    {
                        ViewBag.msgMozo = "No hay servicios";
                    }
                    return View(misServicios);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult ServiciosRepartidor()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Repartidor")
            {
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> servicios = s.GetServiciosPorRepartidorOrdenadosPorFecha(idLogueado);
                if (servicios.Count().Equals(0))
                {
                    ViewBag.msgRepartidor = "No hay servicios";
                }
                return View(servicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
