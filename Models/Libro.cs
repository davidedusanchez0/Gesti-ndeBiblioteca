using System;

namespace Gestión_de_Biblioteca.Models
{
    public class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        public int CopiasDisponibles { get; set; }

        // Propiedad que no se guarda en el archivo txt, solo se calcula en memoria para la UI del Admin
        public int CopiasEnPrestamo { get; set; }

        public Libro(string isbn, string titulo, string autor, int anioPublicacion, int copiasDisponibles)
        {
            ISBN = isbn;
            Titulo = titulo;
            Autor = autor;
            AnioPublicacion = anioPublicacion;
            CopiasDisponibles = copiasDisponibles;
        }
    }
}