using System;

namespace Gestión_de_Biblioteca.Models
{
    public class Prestamo
    {
        public string IdPrestamo { get; set; }
        public string UsernameUsuario { get; set; }
        public string ISBNLibro { get; set; }
        public string TituloLibro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public string Estado { get; set; } // Puede ser "Activo" o "Devuelto"

        public Prestamo(string idPrestamo, string usernameUsuario, string isbnLibro, string tituloLibro, DateTime fechaPrestamo, string estado)
        {
            IdPrestamo = idPrestamo;
            UsernameUsuario = usernameUsuario;
            ISBNLibro = isbnLibro;
            TituloLibro = tituloLibro;
            FechaPrestamo = fechaPrestamo;
            Estado = estado;
        }
    }
}