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

        public string Estado { get; set; } //darle true al darle alta

        public string ListaPlatos { get; set; }


        public Servicio()
        {
            
        }

        public Servicio(Cliente cliente, DateTime fecha)
        {
            Id = UltimoId;
            UltimoId++;
            Cliente = cliente;
            Fecha = fecha;
            Estado = "Abierto";
            ListaPlatos = ListarPlatos();
        }
        


        public virtual double CalcularPrecio() //la clase padre Servicio emplea CalcularPrecio a su manera.
                                               //las clases hijas la utilizan o no, y lo resuelven como lo hizo su clase padre.
        {
            double sumaMontos = 0;

            foreach (PlatoCantidad pc in platos) //para cada plato de la lista que contiene la cantidad de los distintos plato, me quedo con su precio y la cantidad.
                                                 

            {
                sumaMontos += pc.Plato.Precio * pc.Cantidad; //calculamos el precio total (sumamos todos los platos).
            }
            PrecioFinal = sumaMontos;
            return sumaMontos;
        }
        public string ListarPlatos()
        {
            string ret = "";
            foreach(PlatoCantidad pc in platos)
            {
                ret += $"Plato: {pc.Plato.Nombre} , cantidad:  {pc.Cantidad}.  ";
            }
            return ret;
        }


        public void CambiarEstado()
        {
            if(Estado == "Abierto")
            {

            Estado = "Cerrado";

            } else if (Estado == "Cerrado"){
                Estado = "Abierto";

            }
        }

    }
}
