using System;
using System.Diagnostics.CodeAnalysis;

namespace ObligatorioP2
{
    public abstract class Persona : IValidacion, IComparable<Persona> //utilizamos dos interfaces, una de validacion
                                                                      //para utilizar el EsValido() y
                                                                      //la otra para poder utilizar el metodo CompareTo()
    {
        // creamos las instancias que tienen en común todas las clases hijas de clase Persona
        public int Id { get; set; }
        public static int UltimoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }


        public Persona() //creamos un constructor vacio para poder utilizarlo en MVC
        {
        }

        public Persona(string nombre, string apellido)
        {
            Id = UltimoId;
            UltimoId++;
            Nombre = nombre;
            Apellido = apellido;
        }

        public virtual int CompareTo([AllowNull] Persona other) // le damos un criterio para saber como es un apellido respecto de otro.
                                                               // ese criterio retorna un numero: 1 si es mayor a 0, -1 si es menor a 0.
                                                               // luego el Sort se encarga de ordenar a los objetos por apellido
                                                               
        {
            if(Apellido.CompareTo(other.Apellido) > 0)        // el criterio se aplica primero segun los apellidos y luego por nombres
            {
                return 1;
            }
            else if(Apellido.CompareTo(other.Apellido) < 0)
            {
                return -1;
            }
            else
            {
                if(Nombre.CompareTo(other.Nombre) > 0)         
                {
                    return 1;
                }
                else if(Nombre.CompareTo(other.Nombre) < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public abstract bool EsValido(); //declaramos el método EsValido (abtracto en una clase abstracta), no lleva porción de código.
                                         //esta clase padre(Persona) no sabe como la clase hija lo va a resolver.
                                         //luego es definida y utilizado por las clases hijas



    }
}
