using System;

namespace Gestión_de_Biblioteca.Models
{
    public class Administrador : Usuario
    {
        public Administrador(string username, string password) 
            : base(username, password, "Admin")
        {
        }

        public override string ObtenerTipoUsuario()
        {
            return "Administrador con todos los permisos (crear/borrar/modificar).";
        }
    }
}