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
        //instancia de sitema (singleton)
        Sistema s = Sistema.GetInstancia();

        public IActionResult Index()
        {
            return View();
        }

        //lista de los servicios del cliente
        public IActionResult MisServicios()
        {
            //solo entra cliente
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                //obtenemos id del session
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                //creamos lista de sus servicios por id del cliente
                List<Servicio> misServicios = s.GetServiciosPorCliente(idLogueado);
                //si la lista está vacía envía un mensaje de error
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
        //al llenar formulario de fechas se hace método post
        [HttpPost]
        public IActionResult MisServicios(DateTime f1, DateTime f2)
        {
            if (HttpContext.Session.GetInt32("LogueadoId") == null)
            {
                return RedirectToAction("Index", "Home");
            } //solo entra siendo cliente
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                //usa el id logeuado y las fechas para filtrar la lista
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> misServicios = s.GetServiciosPorClienteEntreFechas(idLogueado, f1, f2);
                if (misServicios.Count().Equals(0)) //si la lista está vacía tira un error
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

        //metodo para subir un delivery
        public IActionResult AltaDelivery()
        {//solo para cliente
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                List<Repartidor> lr = s.GetRepartidores();
                //pasamos la lista de repartidores para seleccionar uno para hacer el alta
                ViewBag.Repartidores = lr;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }   
        }
        //si el servicio no se puede hacer se vuelve a cargar la vista con un mensaje de error
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
        //metodo para agregar plato a un servicio

        public IActionResult AgregarPlato(int Id)
        {//solo para rol cliente
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            { //obtenemos el servicio del que estamos hablando por id
                Servicio ser = s.GetServicioPorId(Id);
                List <Plato> lp = s.GetPlatos(); //lista de platos para mostrar en el select

                ViewBag.Servicio = ser;
                ViewBag.Platos = lp;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //cuando se no se agrega el plato se hacce un http post para mostrar el error
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


        //ver los platos de un servicio
        public IActionResult VerPlatos(int Id)
        {
            //solo para cliente
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                //get los platos por el id del servicio en cuestión
                List<PlatoCantidad> misPlatos = s.GetPlatosCantidadPrServicio(Id);
                //si no hay platos tirá error
                if(misPlatos.Count().Equals(0))
                {
                    ViewBag.msg = "No hay platos";
                }
                return View(misPlatos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //Cerrar un servicio abierto
        public IActionResult CerrarServicio(int id)
        {
            //lama la función y vuelve a la vista que muestra los servicios
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
            //pasa por mensaje los mozos para mostrar el select
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
        //cuando hay error se repostea la vista con el error
        [HttpPost]
        public IActionResult AltaLocal(int numeroMesa, int slcMozo, int cantidadComensales)
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                if(s.AltaLocal(idLogueado, numeroMesa, slcMozo, cantidadComensales))
                {
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
        // lista con servicios más caros - cliente
        public IActionResult ServiciosMasCaros()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                //obtiene la lista con los servicios y la pasa a la vista
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
        //Sericios filtrados por plato -- cliente 
        public IActionResult ServiciosPorPlato()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Cliente")
            {
                List<Plato> lp = s.GetPlatos();
                List<Servicio> servicios = new List<Servicio>();
                //pasa por viewbag platos para select de filtrpo
                ViewBag.Platos = lp;
                return View(servicios);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        //pasa la lista filtrada a la lista por post - tambien el viewbag de platos para el select
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
        //vista para ver que servicios atendio el mozo logueado
        public IActionResult ServiciosLocalesAtendioMozo()
        {
            if (HttpContext.Session.GetString("LogueadoRol") == "Mozo")
            {
                //busca los servicios por id
                int? idLogueado = HttpContext.Session.GetInt32("LogueadoId");
                List<Servicio> misServicios = s.GetServiciosPorMozo(idLogueado);
                if (misServicios.Count().Equals(0)) //si la lista de servicios esta vacia manda un mensaje
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
        // con los filtros del formulario envia la lista filtrada para mostrar en la vista - si hay error muestra mensjae
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
        // vista repartidor - muestra los servicios ordenados por fecha
        //si no hay servicios muestra mensaje de error
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
