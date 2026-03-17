using System;
using System.Collections.Generic;
using System.IO;
using Gestión_de_Biblioteca.Models;

namespace Gestión_de_Biblioteca.Services
{
    public class UsuarioService
    {
        private readonly string _filePath;

        public UsuarioService()
        {
            // Ubicamos el archivo en la base al correr de la app
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuarios.txt");
            InicializarArchivo();
        }

        // Se encarga de crear el archivo con usuarios por defecto si no existe.
        private void InicializarArchivo()
        {
            if (!File.Exists(_filePath))
            {
                using (StreamWriter sw = File.CreateText(_filePath))
                {
                    // Formato: Username,Password,Rol
                    sw.WriteLine("admin,admin123,Admin");
                    sw.WriteLine("user,user123,Cliente");
                }
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            var usuarios = new List<Usuario>();

            if (File.Exists(_filePath))
            {
                var lineas = File.ReadAllLines(_filePath);
                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');
                    if (datos.Length >= 3)
                    {
                        var username = datos[0];
                        var password = datos[1];
                        var rol = datos[2];

                        if (rol == "Admin")
                        {
                            usuarios.Add(new Administrador(username, password));
                        }
                        else
                        {
                            usuarios.Add(new Cliente(username, password));
                        }
                    }
                }
            }
            return usuarios;
        }

        public Usuario Login(string username, string password)
        {
            var usuarios = ObtenerUsuarios();
            
            foreach (var usuario in usuarios)
            {
                // Validación sin ser case-sensitive para el usuario, 
                // pero password exacto por seguridad.
                if (usuario.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                    usuario.Password == password)
                {
                    return usuario; // Retorna el usuario autenticado (Polimorfismo: puede ser Admin o Cliente)
                }
            }

            return null; // Significa que no se encontró coincidencia o credenciales erróneas
        }

        public void RegistrarUsuario(Usuario nuevoUsuario)
        {
            var usuarios = ObtenerUsuarios();
            
            // Validación de duplicados
            if (usuarios.Exists(u => u.Username.Equals(nuevoUsuario.Username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("El nombre de usuario ya existe en el sistema.");
            }

            // Sobreescritura (agregar al txt)
            using (StreamWriter sw = File.AppendText(_filePath))
            {
                sw.WriteLine($"{nuevoUsuario.Username},{nuevoUsuario.Password},{nuevoUsuario.Rol}");
            }
        }

        public void EliminarUsuario(string username)
        {
            var usuarios = ObtenerUsuarios();
            var usuario = usuarios.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (usuario != null)
            {
                // Un admin por seguridad no debería borrarse a sí mismo, pero lo dejaremos simple por ahora
                usuarios.Remove(usuario);

                GuardarListaDeUsuarios(usuarios);
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
        }

        public void ActualizarUsuario(Usuario usuarioActualizado)
        {
            var usuarios = ObtenerUsuarios();

            // Buscar por nombre de usuario viejo (ya que ese no se puede cambiar, actúa como ID)
            int index = usuarios.FindIndex(u => u.Username.Equals(usuarioActualizado.Username, StringComparison.OrdinalIgnoreCase));

            if (index != -1)
            {
                usuarios[index] = usuarioActualizado;
                GuardarListaDeUsuarios(usuarios);
            }
            else
            {
                throw new Exception("El usuario no existe, no se pudo actualizar.");
            }
        }

        private void GuardarListaDeUsuarios(List<Usuario> usuarios)
        {
            using (StreamWriter sw = new StreamWriter(_filePath, false))
            {
                foreach (var u in usuarios)
                {
                    sw.WriteLine($"{u.Username},{u.Password},{u.Rol}");
                }
            }
        }
    }
}