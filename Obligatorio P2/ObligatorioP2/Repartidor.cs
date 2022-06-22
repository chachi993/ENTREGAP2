using System;
namespace ObligatorioP2
{
    public class Repartidor : Persona // la clase Repartidor hereda instancias y métodos de su clase padre (Persona)
    {
        // creamos las instancias unicas de las clase Repartidor
        public Vehiculos Vehiculo { get; set; }

        public enum Vehiculos //utilizamos la función Enum para definir los distintos tipos de vehículos
        {
            Moto,
            Bicicleta,
            APie,
        }
        public Repartidor()
        {
        }

        public Repartidor(string nombre, string apellido, Vehiculos vehiculo)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
            Vehiculo = vehiculo;
        }

        public override bool EsValido() //la clase Repartidor hereda la función EsValido de su clase padre (Persona)
                                        //sobreescribe la función realizando sus propias validaciones
        {
            bool esValido = false;
            if (Nombre != "" && Apellido != "") //valida que nombre y apellido no estén vacíos
            {
                esValido = true;

                for (int i = 0; i < Nombre.Length; i++) //valida que nombre no contenga números                                    
                {
                    char caracter = Nombre[i];      //busca en los caracteres del nombre si existe o no un número.
                    if (Char.IsNumber(caracter))
                    {
                        esValido = false;
                    }
                }
                for (int i = 0; i < Apellido.Length; i++) //valida que apellido no contenga números                                                        
                {
                    char caracter2 = Apellido[i];       //busca en los caracteres del apellido si existe o no un número
                    if (Char.IsNumber(caracter2))
                    {
                        esValido = false;
                    }
                }
            }
            return esValido;
        }

        public override string ToString() //función que retorna un objeto en formato string, y lo representa como una cadena de caracteres
        {
            return $"{Apellido},{Nombre}"; //muestra en la consola el Apellido y el Nombre
        }
    }
}
