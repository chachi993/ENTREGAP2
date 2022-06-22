using System;
using System.Collections.Generic;



namespace ObligatorioP2
{
    class Program
    {
        static void Main(string[] args)
        {

            
            Sistema s = Sistema.GetInstancia(); //garantiza la existencia de una sola instancia de una clase Sistema

            int op = -1; //variable op = -1 para que entre en el while
            while (op != 0) //si presionamos 0, sale del programa
            {
                MostrarMenu(); //método que muestra en consola las opciones de Menu
                op = Int32.Parse(Console.ReadLine()); //lee la opcion del menu que selecciona el usuario

                switch (op) 
                {
                    case 1:
                        Console.WriteLine("Listar platos");

                        List<Plato> listaPlatos = s.GetPlatos(); //creamos la lista de platos llamando al métod en System

                        if (listaPlatos.Count > 0) //si la lista es mayor a cero, entonces hay lista de platos
                        {

                            foreach (Plato p in listaPlatos)//se recorren tods los platos para mostrarlos en consola
                            {
                                Console.WriteLine(p);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay registros");
                        }
                        Console.ReadKey();
                        break;

                    case 2:
                        List<Cliente> ListaOrdenada = s.GetClientesOrdenados(); //creamos la lista ordenada de clientes llamando al método en System

                        if (ListaOrdenada.Count > 0)
                        {

                            foreach (Cliente c in ListaOrdenada)
                            {
                                Console.WriteLine(c);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay registros");
                        }

                        Console.ReadKey();
                        break;

                    case 3: 
                        //almacenamos en variables el ID del Repartidor y dos fechas entre las que se buscan servicios de delivery
                        Console.WriteLine("Ingrese ID del Repartidor"); 
                        int num = Int32.Parse(Console.ReadLine()); 

                        Console.WriteLine("Ingrese fecha 1(AAAA-MM-DD)");
                        DateTime fecha1 = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Ingrese fecha 2(AAAA-MM-DD)");
                        DateTime fecha2 = DateTime.Parse(Console.ReadLine());


                        List<Delivery> listaDeliverys = s.GetServiciosPorRepartidorEntreFechas(s.GetRepartidorPorId(num), fecha1, fecha2);
                        //creamos la lista de deliverys llamando al método en System

                        if (listaDeliverys.Count > 0)
                        {
                            foreach (Delivery d in listaDeliverys)
                            {
                                Console.WriteLine(d.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay registros");
                        }
                        Console.ReadKey();
                        break;

                    case 4:
                        double precioMinimoActual = Plato.PrecioMinimo; //mostramos al usuario cual es el precio minimo actual del plato.
                        Console.WriteLine($"El precio mínimo actual es: {precioMinimoActual}");

                        Console.WriteLine("Ingrese nuevo precio mínimo del plato");
                        double precioNuevo = Double.Parse(Console.ReadLine());
                        bool precioCambiado = Plato.ModificarPrecioMinimoPlato(precioNuevo); //se modifica el precio del plato llamando al métod en el System.


                        if (precioCambiado) 
                        {
                            Console.WriteLine($"El nuevo precio mínimo es : {precioNuevo}");

                        }
                        else
                        {
                            Console.WriteLine("No se cambia el precio");
                        }
                        Console.ReadKey();


                        break;

                    case 5:
                        //pedimos datos para registrar (dar de alta) a un mozo.
                        Console.WriteLine("Ingrese el nombre del Mozo");
                        string nombre = Console.ReadLine();

                        Console.WriteLine("Ingrese el apellido del Mozo");
                        string apellido = Console.ReadLine();

                        Console.WriteLine("Ingrese número de funcionario");
                        int nroFuncionario = Int32.Parse(Console.ReadLine());

                        Mozo m = new Mozo(nombre, apellido, nroFuncionario);

                        if (s.AltaMozo(m) != null) //si se cumple el AltaMozo, este se muestra en la pantalla.
                        {
                            Console.WriteLine($"El nuevo mozo es: {m}");
                        }
                        else
                        {
                            Console.WriteLine("El mozo no se puede registrar - verifique que los campos se hayan completado o cambie el número de funcionario");
                        }
                        Console.ReadKey();
                        break;
                }
            }
            if (op == 0)
            {
                Console.Clear(); //cerramos la consola presionando 0.   
            }
        }
        private static void MostrarMenu()//métod para mostrar Menu en pantalla.
        {
            Console.Clear();
            Console.WriteLine("### SISTEMA RESTAURANTE ###");
            Console.WriteLine("1 - Listar Platos");
            Console.WriteLine("2 - Listar Clientes Ordenados por apellido/nombre");
            Console.WriteLine("3 - Listar servicios dado un repartidor y un rango de fechas");
            Console.WriteLine("4 - Cambiar precio mínimo del plato");
            Console.WriteLine("5 - Dar de alta un mozo");
            Console.WriteLine("0 - Salir");

        }

    }
}

            