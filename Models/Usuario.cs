using System;

namespace Gestión_de_Biblioteca.Models
{
    public abstract class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        protected Usuario(string username, string password, string rol)
        {
            Username = username;
            Password = password;
            Rol = rol;
        }

        // Ejemplo de polimorfismo
        public abstract string ObtenerTipoUsuario();
    }
}