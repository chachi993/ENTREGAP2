using System;
using System.Collections.Generic;



namespace ObligatorioP2
{
    public class Sistema 
    {
        //creamos las variables que contienen los diferentes tipos de listas
        //inicializamos las listas
        private List<Plato> platos = new List<Plato>(); 
        private List<Persona> personas = new List<Persona>();
        private List<Servicio> servicios = new List<Servicio>();
        private List<Usuario> usuarios = new List<Usuario>();

        private Sistema()
        {
            PreCarga(); //colocamos PreCarga en constructor de System
        }
        //metodo Singleton para no tener mas de una instancioa de una clase
        private static Sistema instancia = null;


        public static Sistema GetInstancia()
        {
            if(instancia == null)
            {
                instancia = new Sistema();
            }
            return instancia;
        }

        //-----------GET LISTAS---------------

        //método que retorna una lista de clientes ordenados por un criterio
        public List<Cliente> GetClientesOrdenados()
        {
            List<Cliente> clientesOrdenados = new List<Cliente>(); //creamos variable de tipo lista para los clientes ordenados
            foreach (Persona p in personas)//recorremos la lista de personas
            {
                if (p is Cliente)//toma aquellas personas que sean de tipo cliente
                {
                    Cliente aux = p as Cliente; //creamos una variable auxiliar Cliente(de la persona por el cual se encuentra el recorrido)
                    clientesOrdenados.Add(aux); //agregamos el cliente auxiliar a una lista
                }
            }
            clientesOrdenados.Sort(); //se ordena la lista mediante el uso del Sort
            return clientesOrdenados; //devolvemos la lista ordenada
        }
        //el método (al igual que GetClientesOrdenados) devuelve una lista de mozos, obtenida de la lista personas
        public List<Mozo> GetMozos()
        {
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
        //el método (al igual que GetClientesOrdenados) devuelve una lista de repartidores, obtenida de la lista personas
        public List<Repartidor> GetRepartidores()
        {
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
        public List<Delivery> GetDeliveries()
        //el método (similar que GetClientesOrdenados) devuelve una lista de deliveries, obtenida de la lista servicios
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

        //-------------GET INSTANCIA POR ID------------

        //Buscamos y obtenemos recorriendo la lista de personas un Cliente en especifico a traves de su id
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
        //Buscamos y obtenemos un Servicio recorriendo la lista Servicio a traves de su id.
        public Servicio GetServicioPorId(int id)
        {
            foreach (Servicio s in servicios)
            {
                if (s.Id.Equals(id))
                {
                    return s;
                }
            }
            return null;
        }
        //Buscamos y obtenemos un Cliente a traves del id ingresado por el usuario.
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
        //Buscamos y obtenemos un Mozo a partir de su id
        public Mozo GetMozoPorId(int num)
        {
            Mozo ret = null;
            List<Mozo> mozos = GetMozos();

            foreach (Mozo m in mozos)
            {
                if (m.Id.Equals(num))
                {
                    ret = m;
                }
            }
            return ret;
        }
        //metodo que retorna un Repartidos a partir de su id
        public Repartidor GetRepartidorPorId(int num)
        {
            Repartidor ret = null;
            List<Repartidor> repartidores = GetRepartidores(); //creamos una lista de repartidores con GetRepartidores

            foreach (Repartidor r in repartidores)
            {
                if (r.Id.Equals(num)) //si el repartidor tiene el ID igual al argumento num, se retorna el objwto repartidor
                {
                    ret = r;
                }
            }
            return ret;
        }
        //obtenemos las personas por su id
        public Persona GetPersona(int idPersona)
        {
            foreach (Persona p in personas)
            {
                if (p.Id.Equals(idPersona))
                {
                    return p;
                }
            }
            return null;
        }
        //obtenemos los servicios por id
        private Servicio GetServicioPorId(int? idServicio)
        {
            foreach (Servicio s in servicios)

            {
                if (s.Id.Equals(idServicio))
                {
                    return s;
                }
            }
            return null;
        }
        //obtenemos los platos por id
        private Plato GetPlatoPorId(int? idPlato)
        {
            foreach (Plato p in platos)
            {
                if (p.Id.Equals(idPlato))
                {
                    return p;
                }
            }
            return null;
        }

        //--------------OPERACIONES------------
        //Damos por cerrado un Servicio siempre que este en estado "Abierto" 
        //Al cerrarlo cambia su estado a "Cerrado" y muestra el Precio Final de la compra
        public void CerrarServicio(int? idServicio)
        {
            foreach (Servicio s in servicios)
            {
                if (s.Id.Equals(idServicio))
                {
                    if (s.Estado.Equals("Abierto")){
                        s.CerrarServicio();
                    }
                }
            }
        }
        //metodo que permite al Cliente darle "like" a un plato una cantidad indefinida.
        public void Likear(int id)
        {
            foreach (Plato p in platos)
            {
                if (p.Id.Equals(id))
                {
                    p.SumarLike();
                }
            }
        }
        public bool NumeroFuncionarioExiste(int numero) //se busca y se devuelve true si el numero del mozo existe
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
        // verifica si nombre usuario ya existe entre los usuarios
        public bool UsuarioExiste(string user)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.UserName == user)
                {
                    return true;
                }
            }
            return false;
        }
        public bool AgregarPlato(int? idServicio, int? idPlato, int cantidad) //agrega platos aun servicio
        {
            if (idPlato != null && idServicio != null && cantidad > 0)
            {
                Plato p = GetPlatoPorId(idPlato);
                PlatoCantidad pc = new PlatoCantidad(p, cantidad);
                Servicio s = GetServicioPorId(idServicio);
                s.AgregarPlato(pc);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Persona ValidarDatosLogin(string user, string pass) //valida si los datos ingresados en el login son validos(user y password)
        {
            Persona p = null;
            foreach (Usuario u in usuarios)
            {
                if (u.UserName.Equals(user) && u.Password.Equals(pass))
                {
                    p = GetPersona(u.IdPersona);
                }
            }
            return p;
        }

        //----------------GET LISTA SERVICIO FILTRADA----------
        //metodo que devuelve los servicios mas caros que consumio el Cliente
        //si hay empatados, muestra todos los empatados
        public List<Servicio> GetServiciosMasCarosPorIdCliente(int? idCliente)
        {
            List<Servicio> ret = new List<Servicio>();
            double sMasCaro = 0;
            //utilizamos la funcion ya creada para obtener los servicios del Cliente
            List<Servicio> serviciosCliente = GetServiciosPorCliente(idCliente); //de cliente logueado
            foreach (Servicio s in serviciosCliente)
            {
                if (s.PrecioFinal > sMasCaro)
                {
                    sMasCaro = s.PrecioFinal;
                    ret.Clear();
                    ret.Add(s);
                }
                else if (s.PrecioFinal.Equals(sMasCaro))
                {
                    ret.Add(s);
                }
            }
            return ret;
        }
        //metodo que devuelve dado el nombre de un plato, todos los servicios de ese Cliente que 
        //pidio ese plato al menos una vez
        public List<Servicio> GetServiciosSegunNombreDePlato(string nombre, int? idlogueado)
        {
            List<Servicio> ret = new List<Servicio>();
            foreach (Servicio s in GetServiciosPorCliente(idlogueado)) //usamos la funcion donde obtenemos sus servicios
            {
                foreach (PlatoCantidad pc in GetPlatosCantidadPrServicio(s.Id)) //necesitamos la cantidad de cada plato para que haga el conteo de una sola vez.
                    if (pc.Plato.Nombre.Equals(nombre))
                    {
                        ret.Add(s);
                    }
            }
            return ret;
        }

        //retorna una lista de servicios segun el Repartidor dado dos fechas
        public List<Delivery> GetServiciosPorRepartidorEntreFechas(Repartidor repartidor, DateTime fecha1, DateTime fecha2)
        {
            List<Delivery> ret = new List<Delivery>();
            List<Delivery> deliverys = GetDeliveries(); //creamos lista de deliveries con GetDeliveries
            //se recorre la lista de deliveries. Si el repartidor es el mismo que el argumento dado, y la fecha está en el rango
            //brindado: se agrega el objeto delivery a la lista de retorno
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
        //metodo que retorna los servicios del Cliente que fueron realizados entre dos fechas
        public List<Servicio> GetServiciosPorClienteEntreFechas(int? id, DateTime fecha1, DateTime fecha2)
        {
            List<Servicio> ret = new List<Servicio>();
            if(fecha1 != null && fecha2 != null)
            {
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
            }
            return ret;
        }
        //metodo que retorna los servicios del Cliente
        //Agrega ese servicio a la lista de servicios del Cliente.
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
        public List<Servicio> GetServiciosPorMozo(int? id) //obtener los servicios dado un numero de mozo
        {
            List<Servicio> ret = new List<Servicio>();
            foreach (Servicio s in servicios)
            {
                if (s is Local)
                {
                    Local aux = s as Local;
                    if (id == aux.Mozo.Id)
                    {
                        ret.Add(aux);
                    }
                }
            }
            return ret;
        }
        //obtener los servicios locales de los mozos dadas dos fechas
        public List<Servicio> GetServiciosLocalesPorMozoEntreFechas(int? id, DateTime fecha1, DateTime fecha2)
        {
            List<Servicio> ret = new List<Servicio>();
            foreach (Persona p in personas)
            {
                if (p is Mozo && p.Id == id) //si el id de mozo es igual al id que le pasamos por argumento...
                {
                    Mozo aux = p as Mozo;
                    foreach (Servicio s in servicios)
                    {
                        if (s is Local)
                        {
                            Local aux2 = s as Local;
                            if (aux2.Fecha >= fecha1 && aux2.Fecha <= fecha2 && aux2.Mozo == aux)
                            {
                                ret.Add(aux2);
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public List<Servicio> GetServiciosPorRepartidorOrdenadosPorFecha(int? id)//obtenemos los servicios por repartidor ya ordenados por fecha
        {
            List<Servicio> serviciosOrdenados = new List<Servicio>();
            foreach (Persona p in personas)
            {
                if (p is Repartidor && p.Id == id) //si la persona es repartidor y su id es el mismo del que le pasamos por argumento
                {
                    Repartidor aux = p as Repartidor;
                    foreach (Servicio s in servicios)
                    {
                        if (s is Delivery)
                        {
                            Delivery d = s as Delivery;
                            if (d.Repartidor.Equals(aux))
                            {
                                serviciosOrdenados.Add(s);
                            }
                        }
                    }
                }
            }
            serviciosOrdenados.Sort();
            return serviciosOrdenados;
        }

        //----------------GET LISTA FILTRADA----------

        public List<PlatoCantidad> GetPlatosCantidadPrServicio(int id) //obtenemos los platos cantidad de cada servicio
        {
            Servicio s = GetServicioPorId(id);
            List<PlatoCantidad> pc = s.Platos;
            return pc;
        }

        //retornamos la lista de platos ordenanos por nombre
        public List<Plato> GetPlatosOrdenadosPorNombre()
        {
            platos.Sort();
            return platos;
        }

        //---------------ALTA-------------
        //funcion que permite adquirir un servicio de tipo Local.
        //agrega a Mozo a la lista de personas.
        public bool AltaLocal(int? idCliente, int numeroMesa, int slcMozo, int cantidadComensales)
        {
            //si los datos son válidos, coincide el id de Cliente y se selecciona un Mozo que este disponible,
            //se da de alta el nuevo Servicio.
            if (GetClientePorId(idCliente) is Cliente && GetMozoPorId(slcMozo) is Mozo && cantidadComensales>0)
            {
                Local nuevo = new Local(GetClientePorId(idCliente), DateTime.Now, numeroMesa, GetMozoPorId(slcMozo), cantidadComensales);
                servicios.Add(nuevo);
                return true;
            }
            return false;
        }
        
        //funcion que permite adquirir un servicio de tipo Delivery.
        public bool AltaDelivery(int? idCliente, string direccion, int distancia, int slcRepartidor)
        {
            if (GetClientePorId(idCliente) is Cliente && GetRepartidorPorId(slcRepartidor) is Repartidor && distancia > 0 && direccion!= null)
            {
                               Delivery nuevo = new Delivery(GetClientePorId(idCliente), DateTime.Now, direccion, GetRepartidorPorId(slcRepartidor), distancia);
                servicios.Add(nuevo);
                return true;
            }
            return false;
            //si los datos son válidos, coincide el id de Cliente y se selecciona un Repartidor que este disponible,
            //se da de alta el nuevo Servicio.
        }
        
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
        //ALTA CLIENTE como usuario
        public Cliente AltaCliente(Cliente c, string username, string password)
        {
            if (c.EsValido() && !UsuarioExiste(username))
            {
                personas.Add(c);
                Usuario nuevo = new Usuario(c, username, password);
                usuarios.Add(nuevo);
                return c;
            }
            else
            {
                return null;
            }
        }
        public Cliente AltaCliente(string nombre, string apellido, string email, string username, string password)
        {
            Cliente c = new Cliente(nombre, apellido, email, password);
            if (c.EsValido() && !UsuarioExiste(username)) //si el cliente es valido y el usuario existe se puede hacer el alta
            {
                personas.Add(c);
                Usuario nuevo = new Usuario(c, username, password);
                usuarios.Add(nuevo);
                return c;
            }
            else
            {
                return null;
            }
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
        public Mozo AltaMozoComoUsuario(Mozo m, string username, string password) //damos de alta el mozo como un usuario mas
        {
            if (m.EsValido() && !UsuarioExiste(username))
            {
                personas.Add(m);
                Usuario nuevo = new Usuario(m, username, password);
                usuarios.Add(nuevo);
                return m;
            }
            else
            {
                return null;
            }   
        }
        public Repartidor AltaRepartidorComoUsuario(Repartidor r, string username, string password)
        {
            if (r.EsValido() && !UsuarioExiste(username))
            {
                personas.Add(r);
                Usuario nuevo = new Usuario(r, username, password);
                usuarios.Add(nuevo);
                return r;
            }
            else
            {
                return null;
            }
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

        private void PreCarga() //datos precargados de usuario, cliente, mozo , repartidor y delivery
        {  
            Cliente c1 = new Cliente("Juan", "Gonzalez", "juan19@gmail.com", "Juan3456");
            AltaCliente(c1,"Juan1","Juan1"); //llamamos al métod AltaCliente para validr y agregar los clientes al sistema.
            Cliente c2 = new Cliente("Romina", "Lopez", "romina19@gmail.com", "Romina3456");
            AltaCliente(c2, "Romina1", "Romina1");
            Cliente c3 = new Cliente("Claudia", "Pereira", "claudia19@gmail.com", "Claudia3456");
            AltaCliente(c3, "Claudia1", "Claudia1");

            Cliente c4 = new Cliente("Facundo", "Moreira", "facundo19@gmail.com", "Facundo3456");
            AltaCliente(c4, "Facundo1", "Facundo1" );

            Cliente c5 = new Cliente("Florencia", "Martinez", "florencia19@gmail.com", "Florencia3456");
            AltaCliente(c5, "Florencia1", "Florencia");

            Mozo m1 = new Mozo("Pedro", "Fagundez", 1);
            AltaMozoComoUsuario(m1, "Pedro1", "Pedro1" ); //llamamos al métod AltaMozo para validr y agregar los mozos al sistema.
            Mozo m2 = new Mozo("Leandro", "Sanchez", 2);
            AltaMozoComoUsuario(m2, "Leandro1", "Lenadro1");
            Mozo m3 = new Mozo("Lorena", "Varela", 3);
            AltaMozoComoUsuario(m3, "Lorena1", "Lorena1" );
            Mozo m4 = new Mozo("Paola", "Pacheco", 4);
            AltaMozoComoUsuario(m4, "Paola1", "Paola1" );
            Mozo m5 = new Mozo("Santiago", "Benitez", 5);
            AltaMozoComoUsuario(m5, "Santiago1", "Santiago1");
            Mozo m6 = new Mozo("Ramiro", "Gucci", 6);
            AltaMozoComoUsuario(m6, "Ramiro1", "Ramiro1");

            Repartidor r1 = new Repartidor("Javier", "Perez", Repartidor.Vehiculos.APie);
            AltaRepartidorComoUsuario(r1, "Javier1", "Javier1"); //llamamos al método AltaRepartidor para validar y agregar los repartidores al sistema.
            Repartidor r2 = new Repartidor("Gonzalo", "Ramirez", Repartidor.Vehiculos.Bicicleta);
            AltaRepartidorComoUsuario(r2, "Gonzalo1", "Gonzalo1");
            Repartidor r3 = new Repartidor("Maria", "Gutierrez", Repartidor.Vehiculos.Moto);
            AltaRepartidorComoUsuario(r3, "Maria1", "Maria1");
            Repartidor r4 = new Repartidor("Rossana", "Villar", Repartidor.Vehiculos.Moto);
            AltaRepartidorComoUsuario(r4, "Rossana1", "Rossana1");
            Repartidor r5 = new Repartidor("Ana", "Cubero", Repartidor.Vehiculos.APie);
            AltaRepartidorComoUsuario(r5, "AnaAna1", "AnaAna1");
            Repartidor r6 = new Repartidor("Juana", "Alcubero", Repartidor.Vehiculos.Bicicleta);
            AltaRepartidorComoUsuario(r6, "Juana1", "Juana1");


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

            AgregarPlato(1, 1, 3);
            AgregarPlato(2, 2, 2);
            AgregarPlato(3, 3, 1);
            AgregarPlato(4, 4, 2);
            AgregarPlato(5, 5, 3);
            AgregarPlato(6, 6, 1);
            AgregarPlato(7, 7, 2);
            AgregarPlato(8, 8, 3);
            AgregarPlato(9, 9, 4);
            AgregarPlato(0, 0, 3);
            AgregarPlato(1, 2, 3);
            AgregarPlato(2, 4, 2);
            AgregarPlato(3, 7, 1);
            AgregarPlato(4, 4, 2);
            AgregarPlato(5, 0, 3);
            AgregarPlato(6, 5, 1);
            AgregarPlato(7, 2, 2);
            AgregarPlato(8, 1, 3);
            AgregarPlato(9, 0, 4);
            AgregarPlato(0, 9, 3);

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
        }
    }
}

 