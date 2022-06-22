namespace ObligatorioP2
{
    public class Usuario
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public int IdPersona { get; set; }

        public Usuario(string userName, string password, string rol, int idPersona)
        {
            UserName = userName;
            Password = password;
            Rol = rol;
            IdPersona = idPersona;
        }

        public Usuario()
        {
            
        }
        //los siguientes datos nos permiten que se pueda visualizar los contenidos que se van a desplegar en otras vistas
         //acuerdo al rol de cada una de las usuarios, asi como que se pueda hacer el bindeo entre cada uno de los parametros
         //y su representacion en el usuario
        public Usuario (Cliente c, string user, string password) {
            UserName = user;
            Password = password;
            Rol = "Cliente";
            IdPersona = c.Id;
        }
        public Usuario(Mozo m, string user, string password)
        {
            UserName = user;
            Password = password;
            Rol = "Mozo";
            IdPersona = m.Id;
        }
        public Usuario(Repartidor r, string user, string password)
        {
            UserName = user;
            Password = password;
            Rol = "Repartidor";
            IdPersona = r.Id;
        } 
    }
}