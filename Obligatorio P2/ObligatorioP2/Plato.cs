using System;
namespace ObligatorioP2
{
    public class Plato: IValidacion
    {
        public int Id { get; set; }

        public static int UltimoId { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public static double PrecioMinimo { get; set; } = 100;


        public Plato()
        {
        }

        public Plato(string nombre, double precio)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Precio = precio;
        }

        

        public bool EsValido()
        {
            bool nombreEsValido = false;
            bool precioEsValido = false;
            bool esValido = false;

            if (Nombre != "")
            {
                nombreEsValido = true;

                for (int i = 0; i < Nombre.Length; i++)
                {
                    char caracter = Nombre[i];
                    if (Char.IsNumber(caracter))
                    {
                        nombreEsValido = false;
                    }
                }

            }
            if (Precio >= PrecioMinimo)
            {
                precioEsValido = true;
            }

            if(precioEsValido && nombreEsValido)
            {
                esValido = true;
            }
            return esValido;
        }
        public static bool ModificarPrecioMinimoPlato(double precioNuevo)
        {
            if (precioNuevo != PrecioMinimo && precioNuevo >= 0) //si el precio nuevo es validado, se cambia el precio minimo de la clase Plato.
            {
                PrecioMinimo = precioNuevo;
                return true;

            }
            return false;
        }

        public override string ToString()
        {
            return $"{Nombre}: {Precio}.   ";
        }

    }

}

