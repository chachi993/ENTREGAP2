using System;
using System.Collections.Generic;

namespace ObligatorioP2
{
    public abstract class Servicio // si existe una instancia de servicio, ejemplo take away, no seria abstract.
    {

        // creamos las instancias que tienen en común todas las clases hijas de clase Servicio.
        public int Id { get; set; }

        public static int UltimoId { get; set; }

        public Cliente Cliente { get; set; }

        private List<PlatoCantidad> platos = new List<PlatoCantidad>();

        public DateTime Fecha { get; set; }

        public double PrecioFinal { get; set; } = 0;
        public bool Estado { get; set; } //darle true al darle alta 


        public Servicio()
        {
        }

        public Servicio(Cliente cliente, DateTime fecha)
        {
            Id = UltimoId;
            UltimoId++;
            Cliente = cliente;
            Fecha = fecha;
            PrecioFinal = CalcularPrecio();
            Estado = false;
        }


        public virtual double CalcularPrecio() //la clase padre Servicio emplea CalcularPrecio a su manera.
                                               //las clases hijas la utilizan o no, y lo resuelven como lo hizo su clase padre.
        {
            double sumaMontos = 0;

            foreach (PlatoCantidad pc in platos) //para cada plato de la lista que contiene la cantidad de los distintos plato, me quedo con su precio y la cantidad.
                                                 

            {
                sumaMontos += pc.Plato.Precio * pc.Cantidad; //calculamos el precio total (sumamos todos los platos).
            }
            this.PrecioFinal = sumaMontos;
            return sumaMontos;
        }

        

    }
}
