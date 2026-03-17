using System;
using System.Windows.Forms;
using Gestión_de_Biblioteca.Services;
using Gestión_de_Biblioteca.Models;

namespace Gestión_de_Biblioteca
{
    public partial class Form1 : Form
    {
        private readonly UsuarioService _usuarioService;

        public Form1()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingresa el usuario y la contraseña.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Polimorfismo: devuelve un Usuario que puede ser Administrador o Cliente
            Usuario usuarioAutenticado = _usuarioService.Login(username, password);

            if (usuarioAutenticado != null)
            {
                MessageBox.Show($"¡Bienvenido {usuarioAutenticado.Username}!\n{usuarioAutenticado.ObtenerTipoUsuario()}", 
                                "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiamos los campos antes de ocultar el formulario
                txtUser.Clear();
                txtPassword.Clear();

                // Ocultamos el Form1 e iniciamos el Formulario Principal pasándole el usuario
                this.Hide();
                FormBiblioteca fBiblioteca = new FormBiblioteca(usuarioAutenticado);
                fBiblioteca.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingresa un usuario y una contraseña validos para registrar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Por defecto, los usuarios que se auto-registran serán "Clientes" (Usuario normal)
                Cliente nuevoCliente = new Cliente(username, password);
                _usuarioService.RegistrarUsuario(nuevoCliente);

                MessageBox.Show("¡Usuario creado exitosamente! Ahora puedes iniciar sesión.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUser.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
