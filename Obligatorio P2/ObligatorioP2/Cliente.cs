using System;
using System.Diagnostics.CodeAnalysis;

namespace ObligatorioP2
{
    public class Cliente : Persona // hereda de la clase Persona
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Cliente()
        {
        }

        public Cliente(string nombre, string apellido, string email, string password)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Password = password;
        }

        public override bool EsValido() //Validamos si el formulario para alta de  cliente tiene en el nombre un numero,
                                        //si hay mayuscula, minuscula y arroba
        {
            bool stringTieneNumero = false;
            bool esValidoCliente = false;
            bool hayArroba = false;
            bool hayNum = false;
            bool hayMayus = false;
            bool hayMinus = false;

            if (Nombre != "" && Apellido != "") //si tiene nombre y apellido no son vacios, entonces devuelve true el stringTieneNumero
            {

                for (int i = 0; i < Nombre.Length; i++)
                {
                    char caracter = Nombre[i];
                    if (Char.IsNumber(caracter))
                    {
                        stringTieneNumero = true;
                    }
                }

                for (int i = 0; i < Apellido.Length; i++) //recorremos el apellido en toda su longitud, y
                                                          //si tiene numero, stringTieneNumero es true
                {
                    char caracter2 = Apellido[i];
                    if (Char.IsNumber(caracter2))
                    {
                        stringTieneNumero = true;
                    }
                }

            }
    
            for (int i = 1; i < Email.Length - 1; i++) //recorremos el email en toda su longitud, y si tiene el caracter
                                                       //de arroba, hayArroba es true
            {
                char caracter = Email[i];
                if (Char.ToString(caracter) == "@")
                {
                    hayArroba = true;
                }
            }

            if (Password.Length >= 6) //la contraseña debe de tener 6 o mas caracteres
            {
                for (int i = 0; i < Password.Length; i++) //recorremos el password en toda su longitud, y si tiene numero,
                                                          //hayNum es true y si tiene mayus y minuscula, hayMayyus y hayMinus son true
                {
                    char caracter = Password[i];
                    if (Char.IsNumber(caracter))
                    {
                        hayNum = true;
                    }
                    else
                    {
                        if (Char.ToUpper(caracter) == caracter)
                        {
                            hayMayus = true;
                        }
                        if (Char.ToLower(caracter) == caracter)
                        {
                            hayMinus = true;
                        }

                    }

                }

            }

            if (hayArroba && hayNum && hayMinus && hayMayus && !stringTieneNumero) //si incluye todas las validaciones,
                                                                                   //entonces el cliente es valido
            {
                esValidoCliente = true;
            }

            return esValidoCliente; //se retorna el booleano true
        }

        public override int CompareTo([AllowNull] Persona other) //se usa para poder comparar y luego ordenar
        {
            return base.CompareTo(other);
        }

        public override string ToString() //permite mostrar los campos apellido y nombre y
                                          //el valor del atributo correspondiente a estos
        {
            return $"{Apellido},{Nombre}";
        }
    }
}

        //Validaciones en IValidacion

        // - nombre y apellido
        // - email
        // - password
