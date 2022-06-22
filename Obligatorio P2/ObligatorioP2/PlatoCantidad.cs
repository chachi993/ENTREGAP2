using System;
namespace ObligatorioP2
{
    public class PlatoCantidad //creamos una clase de asociación (PlatoCantidad) porque un comensal puede llevar muchos platos y
                               //puede repetir un plato esto hace que haya el mismo plato en una lista
    {
        //creamos las instancias únicas de la clase de asociación PlatoCantidad
        public Plato Plato { get; set; }

        public int Cantidad { get; set; }



        public PlatoCantidad()
        {
        }

        public PlatoCantidad(Plato plato, int cantidad)
        {

            Plato = plato;
            Cantidad = cantidad;
        }
    }
}
