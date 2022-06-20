using System;
namespace ObligatorioP2
{
    public class Delivery : Servicio // la clase Delivery hereda instancias y métodos de su clase padre (Servicio).
    {
        //creamos las instancias únicas de la clase Delivery
        public string Direccion { get; set; }

        public Repartidor Repartidor { get; set; }

        public double DistanciaKM { get; set; }


        public Delivery()
        {
        }

        public Delivery(Cliente cliente, DateTime fecha, string direccion, Repartidor repartidor, int distanciaKM)
        {
            Id = UltimoId;
            UltimoId++;
            Cliente = cliente;
            Fecha = fecha;
            Direccion = direccion;
            Repartidor = repartidor;
            DistanciaKM = distanciaKM;
        }

        public override double CalcularPrecio() //redefine la función CalcularPrecio traída de su clase padre y utiliza el resto. 
        {
            double sumaMontos = base.CalcularPrecio(); //base.CalcularPrecio() es la parte de la función que trae desde la clase padre Servicio.
                                                       //precio base.

            double costoEnvio; //redefine la función.

            if (DistanciaKM < 2) //en distancias menores a 2km tiene un costo de envío de 50 uy.
            {
                costoEnvio = 50;
            }
            else
            {
                double distancia = Math.Round(DistanciaKM); //distancias que sean de 2km o más.
                                                            //utilizamos Math.Round para redondear un valor doble al valor entero más cercano, ya que va aumentando 10 pesos por km.

                double costoExtraDistancia = distancia * 10 - 10; //calculamos el costo extra según aumenten los kms.

                costoEnvio = costoExtraDistancia;
            }

            if (sumaMontos > 100) //aumenta hasta un máximo de 100 pesos.
                                  //una vez que supere los 100 pesos, se mantiene el precio(100).
            {
                costoEnvio = 100;
            }

            return sumaMontos + costoEnvio;
        }

        public override string ToString() //función que retorna un objeto en formato string, y lo representa como una cadena de caracteres.
        {
            return $"Servicio para: {Cliente.Nombre}, en la fecha: {Fecha}, el repartidor fue: {Repartidor.Nombre}."; //muestra en la consola el Nombre del cliente y la Fecha del envío.
        }
    }
}
