using System;

namespace Gestión_de_Biblioteca.Models
{
    public class Cliente : Usuario
    {
        public Cliente(string username, string password) 
            : base(username, password, "Cliente")
        {
        }

        public override string ObtenerTipoUsuario()
        {
            return "Cliente con permisos limitados (consultar, prestar y devolver libros).";
        }
    }
}