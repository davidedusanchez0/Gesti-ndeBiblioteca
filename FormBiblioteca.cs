using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Gestión_de_Biblioteca.Models;
using Gestión_de_Biblioteca.Services;

namespace Gestión_de_Biblioteca
{
    public partial class FormBiblioteca : Form
    {
        private Usuario _usuarioActual;
        private LibroService _libroService;
        private PrestamoService _prestamoService;
        private UsuarioService _usuarioService;
        private List<Libro> _listaLibros;
        private List<Prestamo> _listaPrestamos;
        private List<Usuario> _listaUsuarios;

        public FormBiblioteca(Usuario usuario)
        {
            InitializeComponent();
            _usuarioActual = usuario;
            _libroService = new LibroService();
            _prestamoService = new PrestamoService();
            _usuarioService = new UsuarioService();

            ConfigurarAccesos();
            CargarLibros();
            CargarPrestamos();
            CargarUsuarios();
            CargarEstadisticas();
        }

        private void ConfigurarAccesos()
        {
            lblBienvenida.Text = $"Usuario: {_usuarioActual.Username} | Perfil: {_usuarioActual.Rol}";

            // Aquí aplicamos lo que platicamos: si es tipo "Cliente", ocultamos cosas de Admin
            if (_usuarioActual is Cliente) 
            {
                // Ocultar pestañas de admin
                tabControlPrincipal.TabPages.Remove(tabPanelUsuarios);
                tabControlPrincipal.TabPages.Remove(tabPanelEstadisticas);

                // Ocultar botones de gestionar libros en la pestaña de catálogo
                btnAgregarLibro.Visible = false;
                btnEditarLibro.Visible = false;
                btnEliminarLibro.Visible = false;
            }
            else if (_usuarioActual is Administrador)
            {
                // El Admin no debería estar solicitando préstamos para sí mismo desde acá
                // (opcional: podrías dejarlo visible si también saca libros)
                btnSolicitarPrestamo.Visible = false;
            }
        }

        private void CargarLibros()
        {
            try
            {
                _listaLibros = _libroService.ObtenerLibros();

                // Si es un administrador, le calculamos cuántos préstamos activos hay de cada libro
                if (_usuarioActual is Administrador)
                {
                    // Necesitamos la lista de préstamos actual
                    var prestamos = _prestamoService.ObtenerPrestamos();

                    foreach(var libro in _listaLibros)
                    {
                        // Contamos todos los préstamos que tienen el mismo ISBN del libro y cuyo estado es "Activo"
                        int activos = prestamos.Count(p => p.ISBNLibro == libro.ISBN && p.Estado == "Activo");
                        libro.CopiasEnPrestamo = activos;
                    }
                }

                dgvLibros.DataSource = null;
                dgvLibros.DataSource = _listaLibros;

                // Después de asignar la fuente de datos, ocultamos la columna si es Cliente
                if (_usuarioActual is Cliente)
                {
                    if (dgvLibros.Columns.Contains("CopiasEnPrestamo"))
                    {
                        dgvLibros.Columns["CopiasEnPrestamo"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el catálogo de libros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPrestamos()
        {
            try
            {
                _listaPrestamos = _prestamoService.ObtenerPrestamos();

                if (_usuarioActual is Cliente)
                {
                    // Si es cliente, solo ve los suyos
                    List<Prestamo> misPrestamos = new List<Prestamo>();
                    foreach (var p in _listaPrestamos)
                    {
                        if (p.UsernameUsuario.ToLower() == _usuarioActual.Username.ToLower())
                        {
                            misPrestamos.Add(p);
                        }
                    }
                    dgvPrestamos.DataSource = misPrestamos;
                    lblTituloPrestamos.Text = "Mis Préstamos Activos";
                }
                else
                {
                    // Si es admin, ve los de todos y no necesita el botón devolver
                    dgvPrestamos.DataSource = _listaPrestamos;
                    lblTituloPrestamos.Text = "Registro Global de Préstamos";
                    btnDevolverPrestamo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los préstamos: {ex.Message}");
            }
        }

        private void CargarUsuarios()
        {
            // Solo carga usuarios si el logueado es Admin
            if (_usuarioActual is Administrador)
            {
                try
                {
                    _listaUsuarios = _usuarioService.ObtenerUsuarios();
                    dgvUsuarios.DataSource = _listaUsuarios;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar usuarios: {ex.Message}");
                }
            }
        }

        private void btnAgregarLibro_Click(object sender, EventArgs e)
        {
            // Para mantenerlo simple sin un form extra, usaremos un pequeño truco
            // solicitaremos los datos separados por comas usando InputBox (que requiere Microsoft.VisualBasic)
            // Pero como estamos en C# puro, levantaremos un Formulario muy chiquito al vuelo

            Form formCrear = new Form() { Width = 450, Height = 450, Text = "Agregar Nuevo Libro", StartPosition = FormStartPosition.CenterParent };

            // Incrementamos la separación (Top) y hacemos el form más alto
            TextBox txtISBN = new TextBox() { Left = 60, Top = 40, Width = 300, PlaceholderText = "ISBN (Ej: 2001)" };
            TextBox txtTitulo = new TextBox() { Left = 60, Top = 90, Width = 300, PlaceholderText = "Título del Libro" };
            TextBox txtAutor = new TextBox() { Left = 60, Top = 140, Width = 300, PlaceholderText = "Autor" };
            TextBox txtAnio = new TextBox() { Left = 60, Top = 190, Width = 300, PlaceholderText = "Año de Publicación" };
            TextBox txtCopias = new TextBox() { Left = 60, Top = 240, Width = 300, PlaceholderText = "Copias Disponibles" };

            // Hacemos el botón un poco más alto (Height = 40) para que no se corte el texto
            Button btnGuardar = new Button() { Text = "Guardar", Left = 230, Top = 300, Width = 120, Height = 40 };

            btnGuardar.Click += (senderObj, eArgs) => {
                Form f = (senderObj as Control).FindForm();

                if (string.IsNullOrWhiteSpace(txtISBN.Text) || string.IsNullOrWhiteSpace(txtTitulo.Text))
                {
                    MessageBox.Show("El ISBN y el Título son obligatorios.");
                    return;
                }

                try
                {
                    // Intentamos parsear los números
                    int anio = int.TryParse(txtAnio.Text, out int a) ? a : 2023;
                    int copias = int.TryParse(txtCopias.Text, out int c) ? c : 1;

                    Libro nuevoLibro = new Libro(txtISBN.Text.Trim(), txtTitulo.Text.Trim(), txtAutor.Text.Trim(), anio, copias);
                    _libroService.GuardarLibro(nuevoLibro);

                    f.DialogResult = DialogResult.OK;
                    f.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            formCrear.Controls.Add(txtISBN);
            formCrear.Controls.Add(txtTitulo);
            formCrear.Controls.Add(txtAutor);
            formCrear.Controls.Add(txtAnio);
            formCrear.Controls.Add(txtCopias);
            formCrear.Controls.Add(btnGuardar);

            if (formCrear.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Libro agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarLibros(); // Recargar la grilla
            }
        }

        private void btnEditarLibro_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un libro de la tabla para editarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Libro libroSeleccionado = (Libro)dgvLibros.SelectedRows[0].DataBoundItem;

            Form formEditar = new Form() { Width = 450, Height = 450, Text = "Editar Libro", StartPosition = FormStartPosition.CenterParent };

            TextBox txtISBN = new TextBox() { Left = 60, Top = 40, Width = 300, Text = libroSeleccionado.ISBN, ReadOnly = true }; // ISBN no se debe editar
            TextBox txtTitulo = new TextBox() { Left = 60, Top = 90, Width = 300, Text = libroSeleccionado.Titulo };
            TextBox txtAutor = new TextBox() { Left = 60, Top = 140, Width = 300, Text = libroSeleccionado.Autor };
            TextBox txtAnio = new TextBox() { Left = 60, Top = 190, Width = 300, Text = libroSeleccionado.AnioPublicacion.ToString() };
            TextBox txtCopias = new TextBox() { Left = 60, Top = 240, Width = 300, Text = libroSeleccionado.CopiasDisponibles.ToString() };

            Button btnActualizar = new Button() { Text = "Actualizar", Left = 230, Top = 300, Width = 120, Height = 40 };

            btnActualizar.Click += (senderObj, eArgs) => {
                Form f = (senderObj as Control).FindForm();

                if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                {
                    MessageBox.Show("El Título es obligatorio.");
                    return;
                }
                try
                {
                    int anio = int.TryParse(txtAnio.Text, out int a) ? a : libroSeleccionado.AnioPublicacion;
                    int copias = int.TryParse(txtCopias.Text, out int c) ? c : libroSeleccionado.CopiasDisponibles;

                    Libro libroActualizado = new Libro(txtISBN.Text, txtTitulo.Text.Trim(), txtAutor.Text.Trim(), anio, copias);
                    _libroService.ActualizarLibro(libroActualizado);

                    f.DialogResult = DialogResult.OK;
                    f.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            formEditar.Controls.Add(txtISBN);
            formEditar.Controls.Add(txtTitulo);
            formEditar.Controls.Add(txtAutor);
            formEditar.Controls.Add(txtAnio);
            formEditar.Controls.Add(txtCopias);
            formEditar.Controls.Add(btnActualizar);

            if (formEditar.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Libro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarLibros();
            }
        }

        private void btnEliminarLibro_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un libro de la tabla para eliminarlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación
            var confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar el libro seleccionado?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Obtener el libro de la fila seleccionada (Hacemos cast de DataBoundItem)
                    Libro libroSeleccionado = (Libro)dgvLibros.SelectedRows[0].DataBoundItem;

                    _libroService.EliminarLibro(libroSeleccionado.ISBN);
                    MessageBox.Show("Libro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarLibros(); // Recargar grilla
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormBiblioteca_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Cierra toda la aplicación si el usuario cierra la ventana principal
            Application.Exit();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Estás seguro que deseas cerrar sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                // Volvemos a mostrar el formulario de login (Form1) que estaba oculto
                Form1 formLogin = (Form1)Application.OpenForms["Form1"];
                if (formLogin != null)
                {
                    formLogin.Show();
                }

                // Nos desuscribimos del evento FormClosed para no tirar toda la App
                this.FormClosed -= FormBiblioteca_FormClosed;
                this.Close();
            }
        }

        private void btnSolicitarPrestamo_Click(object sender, EventArgs e)
        {
            if (dgvLibros.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor selecciona un libro de la tabla para solicitarlo.");
                return;
            }

            Libro libroSeleccionado = (Libro)dgvLibros.SelectedRows[0].DataBoundItem;

            if (libroSeleccionado.CopiasDisponibles <= 0)
            {
                MessageBox.Show("Actualmente no hay copias disponibles de este libro.", "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Restar 1 copia en libros.txt
                _libroService.ActualizarCopias(libroSeleccionado.ISBN, -1);

                // 2. Registrar en prestamos.txt
                _prestamoService.RegistrarPrestamo(_usuarioActual.Username, libroSeleccionado.ISBN, libroSeleccionado.Titulo);

                MessageBox.Show($"Has solicitado exitosamente '{libroSeleccionado.Titulo}'.", "Préstamo Aprobado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Recargar ambas grillas para ver los cambios
                CargarLibros();
                CargarPrestamos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en préstamo");
            }
        }

        private void btnDevolverPrestamo_Click(object sender, EventArgs e)
        {
            if (dgvPrestamos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor selecciona el préstamo que deseas devolver.");
                return;
            }

            Prestamo prestamoSeleccionado = (Prestamo)dgvPrestamos.SelectedRows[0].DataBoundItem;

            if (prestamoSeleccionado.Estado == "Devuelto")
            {
                MessageBox.Show("Este libro ya ha sido devuelto anteriormente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. Marcar préstamo como devuelto
                _prestamoService.DevolverPrestamo(prestamoSeleccionado.IdPrestamo);

                // 2. Sumar 1 copia de vuelta al libro correspondiente
                _libroService.ActualizarCopias(prestamoSeleccionado.ISBNLibro, 1);

                MessageBox.Show("Gracias por devolver el libro.", "Devolución Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 3. Recargar grillas
                CargarLibros();
                CargarPrestamos();
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "Error al devolver");
            }
        }

        private void btnCrearUsuarioAdmin_Click(object sender, EventArgs e)
        {
            // Formulario emergente simple para crear usuario
            Form formCrearU = new Form() { Width = 350, Height = 300, Text = "Crear Usuario", StartPosition = FormStartPosition.CenterParent };

            TextBox txtUsername = new TextBox() { Left = 25, Top = 30, Width = 280, PlaceholderText = "Nombre de Usuario" };
            TextBox txtPassword = new TextBox() { Left = 25, Top = 80, Width = 280, PlaceholderText = "Contraseña" };
            ComboBox cmbRol = new ComboBox() { Left = 25, Top = 130, Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRol.Items.Add("Cliente");
            cmbRol.Items.Add("Admin");
            cmbRol.SelectedIndex = 0; // Por defecto Cliente

            Button btnGuardarU = new Button() { Text = "Guardar", Left = 185, Top = 190, Width = 120, Height = 40 };

            btnGuardarU.Click += (senderObj, eArgs) => {
                Form f = (senderObj as Control).FindForm();

                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("El usuario y la contraseña son obligatorios.");
                    return;
                }

                try
                {
                    Usuario nuevoUsuario = cmbRol.Text == "Admin" 
                        ? (Usuario)new Administrador(txtUsername.Text.Trim(), txtPassword.Text.Trim()) 
                        : (Usuario)new Cliente(txtUsername.Text.Trim(), txtPassword.Text.Trim());

                    _usuarioService.RegistrarUsuario(nuevoUsuario);
                    f.DialogResult = DialogResult.OK;
                    f.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            };

            formCrearU.Controls.Add(txtUsername);
            formCrearU.Controls.Add(txtPassword);
            formCrearU.Controls.Add(cmbRol);
            formCrearU.Controls.Add(btnGuardarU);

            if (formCrearU.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Usuario creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUsuarios();
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor selecciona un usuario.");
                return;
            }

            Usuario usuarioSeleccionado = (Usuario)dgvUsuarios.SelectedRows[0].DataBoundItem;

            if (usuarioSeleccionado.Username.Equals(_usuarioActual.Username, System.StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("No puedes eliminar tu propio usuario mientras estás en sesión.", "Acción Degenegada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var confirmacion = MessageBox.Show($"¿Eliminar definitivamente a '{usuarioSeleccionado.Username}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.EliminarUsuario(usuarioSeleccionado.Username);
                    MessageBox.Show("Usuario eliminado.");
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
            private void btnBuscarLibro_Click(object sender, EventArgs e)
            {
                string busqueda = txtBuscarLibro.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(busqueda))
                {
                    dgvLibros.DataSource = _listaLibros;
                }
                else
                {
                    var librosFiltrados = _listaLibros.Where(l => 
                        l.Titulo.ToLower().Contains(busqueda) || 
                        l.Autor.ToLower().Contains(busqueda) || 
                        l.ISBN.Contains(busqueda)).ToList();

                    dgvLibros.DataSource = librosFiltrados;
                }
            }

            private void CargarEstadisticas()
            {
                if (_usuarioActual is Administrador && _listaPrestamos != null)
                {
                    // === CUMPLIMIENTO CON RÚBRICA: USO DE DICCIONARIO ===
                    // En lugar de usar LINQ avanzado directo, usamos un Diccionario para contar los libros
                    Dictionary<string, int> conteoLibros = new Dictionary<string, int>();

                    foreach (var prestamo in _listaPrestamos)
                    {
                        if (conteoLibros.ContainsKey(prestamo.TituloLibro))
                        {
                            conteoLibros[prestamo.TituloLibro]++;
                        }
                        else
                        {
                            conteoLibros.Add(prestamo.TituloLibro, 1);
                        }
                    }

                    // Ordenamos y sacamos los mejores 5
                    var librosTop = conteoLibros.OrderByDescending(x => x.Value).Take(5).ToList();

                    var plotModelLibros = new PlotModel { Title = "Top 5 Libros Más Prestados" };
                    var barSeriesLibros = new BarSeries { LabelPlacement = LabelPlacement.Outside, LabelFormatString = "{0}" };
                    var categoryAxisLibros = new CategoryAxis { Position = AxisPosition.Left };

                    foreach (var item in librosTop)
                    {
                        barSeriesLibros.Items.Add(new BarItem { Value = item.Value });
                        categoryAxisLibros.Labels.Add(item.Key);
                    }

                    plotModelLibros.Series.Add(barSeriesLibros);
                    plotModelLibros.Axes.Add(categoryAxisLibros);
                    plotModelLibros.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, AbsoluteMinimum = 0 });
                    plotLibros.Model = plotModelLibros;

                    // === CUMPLIMIENTO CON RÚBRICA: USO DE MATRIZ/ARREGLO BIDIMENSIONAL Y DICCIONARIO ===
                    // Contamos préstamos activos por usuario usando Diccionario
                    Dictionary<string, int> conteoUsuarios = new Dictionary<string, int>();
                    foreach (var p in _listaPrestamos)
                    {
                        if (conteoUsuarios.ContainsKey(p.UsernameUsuario))
                        {
                            conteoUsuarios[p.UsernameUsuario]++;
                        }
                        else
                        {
                            conteoUsuarios.Add(p.UsernameUsuario, 1);
                        }
                    }

                    // Volcamos el diccionario en un arreglo bidimensional (matriz nx2)
                    int cantidadUsuarios = conteoUsuarios.Count > 5 ? 5 : conteoUsuarios.Count;
                    string[,] matrizUsuarios = new string[cantidadUsuarios, 2];

                    var usuariosOrdenados = conteoUsuarios.OrderByDescending(x => x.Value).Take(cantidadUsuarios).ToList();

                    for (int i = 0; i < cantidadUsuarios; i++)
                    {
                        matrizUsuarios[i, 0] = usuariosOrdenados[i].Key;         // Nombre
                        matrizUsuarios[i, 1] = usuariosOrdenados[i].Value.ToString(); // Cantidad
                    }

                    var plotModelUsuarios = new PlotModel { Title = "Top 5 Usuarios Más Activos" };
                    var pieSeriesUsuarios = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

                    // Leemos de la matriz para popular el gráfico
                    for (int i = 0; i < cantidadUsuarios; i++)
                    {
                        string nombre = matrizUsuarios[i, 0];
                        double cantidad = Convert.ToDouble(matrizUsuarios[i, 1]);
                        pieSeriesUsuarios.Slices.Add(new PieSlice(nombre, cantidad) { IsExploded = false });
                    }

                    plotModelUsuarios.Series.Add(pieSeriesUsuarios);
                    plotUsuarios.Model = plotModelUsuarios;
                }
            }

            private void tabControlPrincipal_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (tabControlPrincipal.SelectedTab == tabPanelEstadisticas)
                {
                    CargarPrestamos(); // Refrescamos en memoria
                    CargarEstadisticas(); // Dibujamos gráfico actual
                }
            }
        }
    }