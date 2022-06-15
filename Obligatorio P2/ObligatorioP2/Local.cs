using System;
namespace ObligatorioP2
{
    public class Local : Servicio // la clase Local hereda instancias y métodos de su clase padre (Servicio).
    {
        //creamos las instancias únicas de la clase Local.
        public int NumeroMesa { get; set; }

        public Mozo Mozo { get; set; }

        public int CantidadComensales { get; set; }

        public static double PrecioCubierto { get; set; }

        public Local()
        {
        }

        public Local(Cliente cliente, DateTime fecha, int numeroMesa, Mozo mozo, int cantidadComensales)
        {
            Id = UltimoId;
            UltimoId++;
            Cliente = cliente;
            Fecha = fecha;
            NumeroMesa = numeroMesa;
            Mozo = mozo;
            CantidadComensales = cantidadComensales;
        }

        public override double CalcularPrecio() //redefine la función CalcularPrecio traída de su clase padre y utiliza el resto. 
        {
            double sumaMontos = base.CalcularPrecio(); //base.CalcularPrecio() es la parte de la función que trae desde la clase padre Servicio.

            double propina = sumaMontos * 0.1; //redefine la función.
                                              //se agrega un 10% de propina

            sumaMontos += propina + PrecioCubierto * CantidadComensales; //se agrega el precio del cubierto

            return sumaMontos;
        }
    }
}
