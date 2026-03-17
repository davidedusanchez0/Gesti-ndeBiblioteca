using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gestión_de_Biblioteca.Models;

namespace Gestión_de_Biblioteca.Services
{
    public class PrestamoService
    {
        private readonly string _filePath;

        public PrestamoService()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "prestamos.txt");
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close(); // Crea el archivo vacío si no existe
            }
        }

        public List<Prestamo> ObtenerPrestamos()
        {
            var prestamos = new List<Prestamo>();
            
            if (File.Exists(_filePath))
            {
                var lineas = File.ReadAllLines(_filePath);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    if (datos.Length >= 6) // Ahora esperamos 6 datos mínimos
                    {
                        var prestamo = new Prestamo(
                            datos[0], // ID
                            datos[1], // Username
                            datos[2], // ISBN
                            datos[3], // Título Libro
                            DateTime.Parse(datos[4]), // Fecha
                            datos[5]  // Estado
                        );
                        prestamos.Add(prestamo);
                    }
                }
            }
            return prestamos;
        }

        public void RegistrarPrestamo(string username, string isbn, string tituloLibro)
        {
            var prestamos = ObtenerPrestamos();

            // Generar un ID simple P + un número
            string nuevoId = "P" + (prestamos.Count + 1).ToString("D3"); 

            var nuevoPrestamo = new Prestamo(nuevoId, username, isbn, tituloLibro, DateTime.Now, "Activo");

            using (StreamWriter sw = File.AppendText(_filePath))
            {
                // Agregamos TituloLibro al formato
                sw.WriteLine($"{nuevoPrestamo.IdPrestamo},{nuevoPrestamo.UsernameUsuario},{nuevoPrestamo.ISBNLibro},{nuevoPrestamo.TituloLibro},{nuevoPrestamo.FechaPrestamo:o},{nuevoPrestamo.Estado}");
            }
        }

        public void DevolverPrestamo(string idPrestamo)
        {
            var prestamos = ObtenerPrestamos();
            var prestamo = prestamos.FirstOrDefault(p => p.IdPrestamo == idPrestamo);

            if (prestamo != null)
            {
                prestamo.Estado = "Devuelto";

                // Sobrescribimos el archivo con el estado modificado y el nuevo formato
                using (StreamWriter sw = new StreamWriter(_filePath, false))
                {
                    foreach (var p in prestamos)
                    {
                        sw.WriteLine($"{p.IdPrestamo},{p.UsernameUsuario},{p.ISBNLibro},{p.TituloLibro},{p.FechaPrestamo:o},{p.Estado}");
                    }
                }
            }
        }
    }
}