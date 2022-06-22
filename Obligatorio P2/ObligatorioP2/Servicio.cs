using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ObligatorioP2
{
    public abstract class Servicio: IComparable<Servicio>  // si existe una instancia de servicio, ejemplo take away, no seria abstract
    {
        // creamos las instancias que tienen en común todas las clases hijas de clase Servicio
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public Cliente Cliente { get; set; }
        public List<PlatoCantidad> Platos = new List<PlatoCantidad>();
        public DateTime Fecha { get; set; }
        public double PrecioFinal { get; set; } = 0;
        public string Estado { get; set; } //darle true al darle alta

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
        }
        


        public virtual double CalcularPrecio() //la clase padre Servicio emplea CalcularPrecio a su manera.
                                               //las clases hijas la utilizan o no, y lo resuelven como lo hizo su clase padre.
        {
            if (Estado == "Cerrado")
            {
                return -1;
            }
            double sumaMontos = 0;

            foreach (PlatoCantidad pc in Platos) //para cada plato de la lista que contiene la cantidad de los distintos plato, me quedo con su precio y la cantidad.
                                                 

            {
                sumaMontos += pc.Plato.Precio * pc.Cantidad; //calculamos el precio total (sumamos todos los platos).
            }
            PrecioFinal = sumaMontos;
            return sumaMontos;
        }

        public void CambiarEstado()
        {
            if(Estado == "Abierto")
            {

            Estado = "Cerrado";

            } 
        }
        public void CerrarServicio()
        {
            if (Estado == "Abierto")
            {
                PrecioFinal = CalcularPrecio();
                Estado = "Cerrado";
            }
        }
        public void AgregarPlato(PlatoCantidad pc)
        {
            Platos.Add(pc);
        }
        public virtual int CompareTo([AllowNull] Servicio other)
        {
            if (Fecha.CompareTo(other.Fecha) > 0)
            {
                return -1;
            }
            else if (Fecha.CompareTo(other.Fecha) < 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
