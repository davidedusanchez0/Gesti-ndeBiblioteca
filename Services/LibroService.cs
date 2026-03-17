using System;
using System.Collections.Generic;
using System.IO;
using Gestión_de_Biblioteca.Models;

namespace Gestión_de_Biblioteca.Services
{
    public class LibroService
    {
        private readonly string _filePath;

        public LibroService()
        {
            // Ubicamos el archivo libros.txt en la misma carpeta base
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libros.txt");
            InicializarArchivo();
        }

        // Se encarga de crear el archivo con libros por defecto si no existe.
        private void InicializarArchivo()
        {
            if (!File.Exists(_filePath))
            {
                using (StreamWriter sw = File.CreateText(_filePath))
                {
                    // Formato: ISBN,Titulo,Autor,AñoPublicacion,CopiasDisponibles
                    sw.WriteLine("1001,El Quijote,Miguel de Cervantes,1605,5");
                    sw.WriteLine("1002,Cien Años de Soledad,Gabriel Garcia Marquez,1967,3");
                    sw.WriteLine("1003,El Principito,Antoine de Saint-Exupery,1943,10");
                    sw.WriteLine("1004,1984,George Orwell,1949,0"); // 0 copias para simular que no hay prestables
                    sw.WriteLine("1005,Fahrenheit 451,Ray Bradbury,1953,5");
                    sw.WriteLine("1006,Orgullo y Prejuicio,Jane Austen,1813,4");
                    sw.WriteLine("1007,Matar a un Ruiseñor,Harper Lee,1960,6");
                    sw.WriteLine("1008,El Gran Gatsby,Jane Austen,1925,3");
                    sw.WriteLine("1009,Moby Dick,F. Scott Fitzgerald,1925,2");
                    sw.WriteLine("1010,Crimen y Castigo,Harper Lee,1960,7");
                    sw.WriteLine("1011,Don Quijote de la Mancha,Miguel de Cervantes,1605,8");
                    sw.WriteLine("1012,En Busca del Tiempo Perdido,Marcel Proust,1913,3");
                    sw.WriteLine("1013,Ulises,Herman Melville,1851,4");
                    sw.WriteLine("1014,La Odisea,Homero,-800,2");
                    sw.WriteLine("1015,Guerra y Paz,León Tolstói,1869,5");
                    sw.WriteLine("1016,Los Miserables,León Tolstói,1862,6");
                    sw.WriteLine("1017,Madame Bovary,Gustave Flaubert,1857,4");
                    sw.WriteLine("1018,La Divina Comedia,Dante Alighieri,1320,3");
                    sw.WriteLine("1019,El Señor de los Anillos,J. R. R. Tolkien,1954,10");
                    sw.WriteLine("1020,Harry Potter y la Piedra Filosofal,J. K. Rowling,1997,12");
                    sw.WriteLine("1021,Crónica de una Muerte Anunciada,Gabriel García Márquez,1981,6");
                    sw.WriteLine("1022,El Alquimista,Paulo Coelho,1988,8");
                    sw.WriteLine("1023,El Diario de Ana Frank,Ana Frank,1947,5");
                    sw.WriteLine("1024,Los Juegos del Hambre,Suzanne Collins,2008,9");
                }
            }
        }

        public List<Libro> ObtenerLibros()
        {
            var libros = new List<Libro>();

            if (File.Exists(_filePath))
            {
                var lineas = File.ReadAllLines(_filePath);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    if (datos.Length >= 5)
                    {
                        var libro = new Libro(
                            datos[0], // ISBN
                            datos[1], // Titulo
                            datos[2], // Autor
                            int.Parse(datos[3]), // Año
                            int.Parse(datos[4])  // Copias Disponibles
                        );
                        libros.Add(libro);
                    }
                }
            }

            return libros;
        }

        public void GuardarLibro(Libro nuevoLibro)
        {
            var libros = ObtenerLibros();
            
            // Validar que no se duplique por ISBN (su código identificador)
            if (libros.Exists(l => l.ISBN == nuevoLibro.ISBN))
            {
                throw new Exception("Ya existe un libro con este ISBN (Código) en el catálogo.");
            }

            // Guardar al final del archivo
            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine($"{nuevoLibro.ISBN},{nuevoLibro.Titulo},{nuevoLibro.Autor},{nuevoLibro.AnioPublicacion},{nuevoLibro.CopiasDisponibles}");
            }
        }

        public void EliminarLibro(string isbn)
        {
            var libros = ObtenerLibros();
            var libroAEliminar = libros.Find(l => l.ISBN == isbn);

            if (libroAEliminar != null)
            {
                libros.Remove(libroAEliminar);

                // Sobreescribir el archivo con la lista actualizada
                using (StreamWriter sw = new StreamWriter(_filePath, false))
                {
                    foreach (var libro in libros)
                    {
                        sw.WriteLine($"{libro.ISBN},{libro.Titulo},{libro.Autor},{libro.AnioPublicacion},{libro.CopiasDisponibles}");
                    }
                }
            }
            else
            {
                throw new Exception("El libro seleccionado no se encontró en el catálogo.");
            }
        }

        public void ActualizarCopias(string isbn, int cantidadCambio)
        {
            var libros = ObtenerLibros();
            var libro = libros.Find(l => l.ISBN == isbn);

            if (libro != null)
            {
                if (libro.CopiasDisponibles + cantidadCambio < 0)
                {
                    throw new Exception("No hay copias suficientes para realizar el préstamo.");
                }

                libro.CopiasDisponibles += cantidadCambio;

                GuardarListaCompleta(libros);
            }
        }

        public void ActualizarLibro(Libro libroActualizado)
        {
            var libros = ObtenerLibros();
            int index = libros.FindIndex(l => l.ISBN == libroActualizado.ISBN);

            if (index != -1)
            {
                libros[index] = libroActualizado;
                GuardarListaCompleta(libros);
            }
            else
            {
                throw new Exception("El libro no existe.");
            }
        }

        private void GuardarListaCompleta(List<Libro> libros)
        {
            using (StreamWriter sw = new StreamWriter(_filePath, false))
            {
                foreach (var l in libros)
                {
                    sw.WriteLine($"{l.ISBN},{l.Titulo},{l.Autor},{l.AnioPublicacion},{l.CopiasDisponibles}");
                }
            }
        }
    }
}