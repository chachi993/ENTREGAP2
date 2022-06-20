using System;
using System.Collections.Generic;



namespace ObligatorioP2
{
    public class Sistema 
    {

        //creamos las variables que contienen los diferentes tipos de listas
        //inicializamos las listas.
        private List<Plato> platos = new List<Plato>(); 
        private List<Persona> personas = new List<Persona>();
        private List<Servicio> servicios = new List<Servicio>();
        private List<Usuario> usuarios = new List<Usuario>();

        private Sistema()
        {
            PreCarga(); //colocamos PreCarga en constructor de System.
        }
        private static Sistema instancia = null;

        public static Sistema GetInstancia()
        {
            if(instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }

        public List<Servicio> GetServiciosPorClienteEntreFechas(int? idLogueado)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> GetClientesOrdenados() //métod que retorna una lista de clientes ordenados por un criterio.
        {
            List<Cliente> clientesOrdenados = new List<Cliente>(); //creamos variable de tipo lista para los clientes ordenados.
            foreach (Persona p in personas)//recorremos la lista de personas. 
            {
                if (p is Cliente)//toma aquellas personas que sean de tipo cliente.
                {
                    Cliente aux = p as Cliente; //creamos una variable auxiliar Cliente(de la persona por el cual se encuentra el recorrido).
                    clientesOrdenados.Add(aux); //agregamos el cliente auxiliar a una lista.
                }
            }
            clientesOrdenados.Sort(); //se ordena la lista mediante el uso del Sort.
            return clientesOrdenados; //devolvemos la lista ordenada.
        }

        public List<Mozo> GetMozos()
        {
            //el métod (al igual que GetClientesOrdenados) devuelve una lista de mozos, obtenida de la lista personas.
            List<Mozo> ret = new List<Mozo>();
            foreach (Persona p in personas)
            {
                if (p is Mozo)
                {
                    Mozo aux = p as Mozo;
                    ret.Add(aux);
                }
            }
            
            return ret;
        }

        public List<Repartidor> GetRepartidores()
        {
            //el métod (al igual que GetClientesOrdenados) devuelve una lista de repartidores, obtenida de la lista personas.
            List<Repartidor> ret = new List<Repartidor>();
            foreach (Persona p in personas)
            {
                if (p is Repartidor)
                {
                    Repartidor aux = p as Repartidor;
                    ret.Add(aux);
                }
            }

            return ret;
        }
        public Cliente GetClientePorId(int? idCliente)
        {
            foreach (Persona p in personas)
            {
                if (p.Id.Equals(idCliente))
                {
                    if (p is Cliente)
                    {
                        Cliente aux = p as Cliente;
                        return aux;
                    }
                }
            }
            
            return null;
        }
        public Cliente GetCliente(int? idCliente)
        {
            foreach (Persona p in personas)
            {
                if (p.Id.Equals(idCliente))
                {
                    if (p is Cliente)
                    {
                        Cliente aux = p as Cliente;
                        return aux;
                    }
                }
            }

            return null;
        }
        public void CerrarServicio(int idServicio)
        {
            foreach (Servicio s in servicios)
            {
                if (s.Id.Equals(idServicio))
                {
                    if (s.Estado.Equals("Abierto")){
                        s.CambiarEstado();
                        s.CalcularPrecio();
                    }
                }
            }
        }

        public void AltaDelivery(int? idCliente, string direccion, int distancia, int slcRepartidor)
        {
            Delivery nuevo = new Delivery (GetClientePorId(idCliente), DateTime.Now, direccion, GetRepartidorPorId(slcRepartidor), distancia);
            servicios.Add(nuevo);
        }

        public List<Delivery> GetDeliveries()
        //el métod (similar que GetClientesOrdenados) devuelve una lista de deliveries, obtenida de la lista servicios.
        {
            List<Delivery> ret = new List<Delivery>();
            foreach (Servicio s in servicios)
            {
                if (s is Delivery)
                {
                    Delivery aux = s as Delivery;
                    ret.Add(aux);
                }
            }

            return ret;
        }

        //retornamos la lista de platos.
        public List<Plato> GetPlatos()
        {
            return platos;
        }
        public List<Plato> GetPlatosOrdenadosPorNombre(int? id)
        {
            platos.Sort();
            return platos;
        }
        public List<Plato> GetPlatosOrdenadosPorNombre()
        {
            platos.Sort();
            return platos;
        }
        public Repartidor GetRepartidorPorId(int num) 
        {
            Repartidor ret = null; 
            List<Repartidor> repartidores = GetRepartidores(); //creamos una lista de repartidores con GetRepartidores.

            foreach (Repartidor r in repartidores) 
            {
                if(r.Id.Equals(num)) //si el repartidor tiene el ID igual al argumento num, se retorna el objwto repartidor.
                {
                    ret = r;
                }
            }

            return ret;
        }
        public void Likear(int id)
        {
            foreach (Plato p in platos)
            {

                if (p.Id.Equals(id))
                {
                    p.Likes++;
                }
            }
        }


        public List<Delivery> GetServiciosPorRepartidorEntreFechas(Repartidor repartidor, DateTime fecha1, DateTime fecha2)

        {
            List<Delivery> ret = new List<Delivery>();

            List<Delivery> deliverys = GetDeliveries(); //creamos lista de deliveries con GetDeliveries.

            //se recorre la lista de deliveries. Si el repartidor es el mismo que el argumento dado, y la fecha está en el rango
            //brindado: se agrega el objeto delivery a la lista de retorno.

            foreach (Delivery d in deliverys)
            {
                if (repartidor == d.Repartidor) 
                {
                    if (d.Fecha >= fecha1 && d.Fecha <= fecha2)
                    {
                        ret.Add(d);
                    }
                }
            }

            return ret;
        }
        public List<Servicio> GetServiciosPorClienteEntreFechas(int? id, DateTime fecha1, DateTime fecha2)
        {
            List<Servicio> ret = new List<Servicio>();
            foreach (Servicio s in servicios)
            {
                if (id == s.Cliente.Id)
                {
                    if (s.Fecha >= fecha1 && s.Fecha <= fecha2)
                    {
                        ret.Add(s);
                    }
                }
            }
            return ret;
        }
        public List<Servicio> GetServiciosPorCliente(int? id)
        {
            List<Servicio> ret = new List<Servicio>();
            foreach (Servicio s in servicios)
            {
                if (id == s.Cliente.Id)
                {
                   
                    ret.Add(s);
                    
                }
            }
            return ret;
        }



        //public bool ModificarPrecioMinimoPlato(double precioNuevo) 
        //{
        //    if (precioNuevo != Plato.PrecioMinimo && precioNuevo>=0) //si el precio nuevo es validado, se cambia el precio minimo de la clase Plato.
        //    {
        //    Plato.PrecioMinimo = precioNuevo;
        //    return true;

        //    }
        //    return false;
        //}

        public Mozo AltaMozo(Mozo m) 
        {
            Mozo nuevo = null;
            if (m.EsValido() && !NumeroFuncionarioExiste(m.NumeroFuncionario))
            //si los datos del funcionario son válidos y el número de funcionario no existe: se da de alta el nuevo Mozo.
            //agrega a Mozo a la lista de personas.
            {
                nuevo = m;
                personas.Add(nuevo);

            }
            return nuevo;

        }
        public Repartidor AltaRepartidor(Repartidor r)
        {
            Repartidor nuevo = null;
            if (r.EsValido())
            //si los datos del repartidor son válidos: se da de alta el nuevo Repartidor.
            //agrega a Repartidor a la lista de personas.
            {
                nuevo = r;
                personas.Add(nuevo);

            }
            return nuevo;

        }
        public Plato AltaPlato(Plato p)
        {
            if (p.EsValido())
            //si los datos del plato son válidos: se da de alta el nuevo Plato.
            //agrega a Plato a la lista de platos.
            {
                platos.Add(p);
                return p;
            }
            return null;
        }

        //ALTA CLIENTE NUEVA
        public Cliente AltaCliente(Cliente c, string username, string password)
        {
            personas.Add(c);
            Usuario nuevo = new Usuario(c, username, password);
            usuarios.Add(nuevo);
            return c;
        }

        public Cliente AltaCliente(Cliente c)
        {
            if (c.EsValido())
            //si los datos del cliente son válidos: se da de alta el nuevo Cliente.
            //agrega a Cliente a la lista de personas.
            {
                personas.Add(c);
                return c;
            }
            return null;
        }

        public Persona ValidarDatosLogin(string user, string pass)
        {
            Persona p = null;
            foreach(Usuario u in usuarios)
            {
                if(u.UserName.Equals(user) && u.Password.Equals(pass))
                {
                    p = GetPersona(u.IdPersona);
                }
            }
            return p;
        }

        public Persona GetPersona(int idPersona)
        {
            foreach(Persona p in personas)
            {
                if (p.Id.Equals(idPersona))
                {
                    return p;
                }
            }
            return null;
        }


        public bool NumeroFuncionarioExiste(int numero) 
        {
            bool numExiste = false;
            foreach (Persona m in personas) //se recorren las personas, si la persona es Mozo se hace una variable auxiliar de tipo Mozo.
                                             
            {
                if (m is Mozo)
                {
                    Mozo aux = m as Mozo;

                    if (aux.NumeroFuncionario == numero) //se verifica si el numero de funcionario del mozo es igual al argumento.
                    {
                        numExiste = true;
                    }

                }
            }

            return numExiste;
        }


            private void PreCarga()
            {
                
                Cliente c1 = new Cliente("Juan", "Gonzalez", "juan19@gmail.com", "Juan3456");
                AltaCliente(c1,"Juan1","Juan1"); //llamamos al métod AltaCliente para validr y agregar los clientes al sistema.

                Cliente c2 = new Cliente("Romina", "Lopez", "romina19@gmail.com", "Romina3456");
                AltaCliente(c2);

                Cliente c3 = new Cliente("Claudia", "Pereira", "claudia19@gmail.com", "Claudia3456");
                AltaCliente(c3);

                Cliente c4 = new Cliente("Facundo", "Moreira", "facundo19@gmail.com", "Facundo3456");
                AltaCliente(c4);

                Cliente c5 = new Cliente("Florencia", "Martinez", "florencia19@gmail.com", "Florencia3456");
                AltaCliente(c5);

                Mozo m1 = new Mozo("Pedro", "Fagundez", 1);
                AltaMozo(m1); //llamamos al métod AltaMozo para validr y agregar los mozos al sistema.
                Mozo m2 = new Mozo("Leandro", "Sanchez", 2);
                AltaMozo(m2);
                Mozo m3 = new Mozo("Lorena", "Varela", 3);
                AltaMozo(m3);
                Mozo m4 = new Mozo("Paola", "Pacheco", 4);
                AltaMozo(m4);
                Mozo m5 = new Mozo("Santiago", "Benitez", 5);
                AltaMozo(m5);

                Repartidor r1 = new Repartidor("Javier", "Perez", Repartidor.Vehiculos.APie);
                AltaRepartidor(r1); //llamamos al métod AltaRepartidor para validr y agregar los repartidores al sistema.
                Repartidor r2 = new Repartidor("Gonzalo", "Ramirez", Repartidor.Vehiculos.Bicicleta);
                AltaRepartidor(r2);

                Repartidor r3 = new Repartidor("Maria", "Gutierrez", Repartidor.Vehiculos.Moto);
                AltaRepartidor(r3);

                Repartidor r4 = new Repartidor("Rossana", "Villar", Repartidor.Vehiculos.Moto);
                AltaRepartidor(r4);

                Repartidor r5 = new Repartidor("Ana", "Cubero", Repartidor.Vehiculos.APie);
                AltaRepartidor(r5);


                Delivery d1 = new Delivery(c1, DateTime.Parse("2022-03-03"), "Ramirez 123", r1, 12);
                servicios.Add(d1); //no es relevante la validación de los datos, se agregan directamente al sistema.
                
                Delivery d2 = new Delivery(c2, DateTime.Parse("2021-03-03"), "Bolivar 456", r2, 5);
                servicios.Add(d2);
                Delivery d3 = new Delivery(c3, DateTime.Parse("2021-04-03"), "Bolivia 789", r3, 9);
                servicios.Add(d3);
                Delivery d4 = new Delivery(c4, DateTime.Parse("2021-09-23"), "Nin y Silva 123", r4, 22);
                servicios.Add(d4);
                Delivery d5 = new Delivery(c5, DateTime.Parse("2022-01-30"), "Rivera 456", r5, 11);
                servicios.Add(d5);
                Local l1 = new Local(c1, DateTime.Parse("2021-03-03"), 1, m4, 7);
                servicios.Add(l1);
                Local l2 = new Local(c3, DateTime.Parse("2022-07-15"), 3, m5, 2);
                servicios.Add(l2);
                Local l3 = new Local(c2, DateTime.Parse("2020-10-19"), 4, m1, 1);
                servicios.Add(l3);
                Local l4 = new Local(c5, DateTime.Parse("2021-12-01"), 2, m3, 5);
                servicios.Add(l4);
                Local l5 = new Local(c5, DateTime.Parse("2021-08-23"), 4, m2, 4);
                servicios.Add(l5);
                CerrarServicio(d1.Id);
                CerrarServicio(d2.Id);
                CerrarServicio(d3.Id);
                CerrarServicio(d4.Id);
                CerrarServicio(d5.Id);
                CerrarServicio(l1.Id);
                CerrarServicio(l2.Id);
                CerrarServicio(l3.Id);
                CerrarServicio(l4.Id);
                CerrarServicio(l5.Id);


            Plato p1 = new Plato("Milanesa", 345);
                AltaPlato(p1); //llamamos al métod AltaPlato para validr y agregar los platos al sistema.

                Plato p2 = new Plato("Pollo", 405);
                AltaPlato(p2);

                Plato p3 = new Plato("Ensalada", 250);
                AltaPlato(p3);

                Plato p4 = new Plato("Hamburguesa al plato", 180);
                AltaPlato(p4);

                Plato p5 = new Plato("Nuggets", 250);
                AltaPlato(p5);

                Plato p6 = new Plato("Guiso", 450);
                AltaPlato(p6);

                Plato p7 = new Plato("Nioqui", 370);
                AltaPlato(p7);

                Plato p8 = new Plato("Pascualina", 180);
                AltaPlato(p8);

                Plato p9 = new Plato("Canelones de carne", 270);
                AltaPlato(p9);

                Plato p10 = new Plato("Lasagna", 410);
                AltaPlato(p10);
            }
        }
    }

 