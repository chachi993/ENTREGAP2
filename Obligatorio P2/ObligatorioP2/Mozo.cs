using System;
namespace ObligatorioP2
{
    public class Mozo : Persona // la clase Mozo hereda instancias y métodos de su clase padre (Persona).
    {

        //creamos las instancias unicas de las clase Mozo
        public int NumeroFuncionario { get; set; }


        public Mozo()
        {
        }

        public Mozo(string nombre, string apellido, int numeroFuncionario)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
            NumeroFuncionario = numeroFuncionario;
        }

        public override bool EsValido() //la clase Mozo hereda el método EsValido de su clase padre (Persona)
                                        //sobreescribe el método realizando sus propias validaciones
        {
            bool esValido = false;

            if (Nombre != "" && Apellido != "") //valida que nombre y apellido no estén vacíos
            {
                esValido = true;

                for (int i = 0; i < Nombre.Length; i++)//valida que nombre no contenga números.
                                                       
                {
                    char caracter = Nombre[i];      //busca en los caracteres del nombre si existe o no un número.
                    if (Char.IsNumber(caracter))
                    {
                        esValido = false;
                    }
                }

                for (int i = 0; i < Apellido.Length; i++)//valida que apellido no contenga números.
                                                         
                {
                    char caracter2 = Apellido[i]; //busca en los caracteres del apellido si existe o no un número.
                    if (Char.IsNumber(caracter2))
                    {
                        esValido = false;
                    }
                }


            }
            return esValido;
        }

        public override string ToString()  //función que retorna un objeto en formato string, y lo representa como una cadena de caracteres.
        {

            return $"{Apellido}, {Nombre}. Su número de funcionario es: {NumeroFuncionario}";   //muestra en la consola el Apellido, el Nombre y el Número de Funcionario.

        }
    }
}
