namespace Gestión_de_Biblioteca
{
    partial class FormBiblioteca
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.TabControl tabControlPrincipal;
        private System.Windows.Forms.TabPage tabPanelLibros;
        private System.Windows.Forms.TabPage tabPanelPrestamos;
        private System.Windows.Forms.TabPage tabPanelUsuarios;
        private System.Windows.Forms.TabPage tabPanelEstadisticas;

        // Controles para el Catálogo de Libros
        private System.Windows.Forms.DataGridView dgvLibros;
        private System.Windows.Forms.Button btnAgregarLibro;
        private System.Windows.Forms.Button btnEditarLibro;
        private System.Windows.Forms.Button btnEliminarLibro;
        private System.Windows.Forms.Button btnSolicitarPrestamo;
        private System.Windows.Forms.TextBox txtBuscarLibro;
        private System.Windows.Forms.Button btnBuscarLibro;

        // Controles para Préstamos
        private System.Windows.Forms.DataGridView dgvPrestamos;
        private System.Windows.Forms.Button btnDevolverPrestamo;
        private System.Windows.Forms.Label lblTituloPrestamos;

        // Controles para Usuarios
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btnCrearUsuarioAdmin;
        private System.Windows.Forms.Button btnEditarUsuario;
        private System.Windows.Forms.Button btnEliminarUsuario;

        // Controles para Estadísticas (OxyPlot)
        private OxyPlot.WindowsForms.PlotView plotLibros;
        private OxyPlot.WindowsForms.PlotView plotUsuarios;

        // Control para Cerrar Sesión
        private System.Windows.Forms.Button btnCerrarSesion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblBienvenida = new Label();
            tabControlPrincipal = new TabControl();
            tabPanelLibros = new TabPage();
            btnBuscarLibro = new Button();
            txtBuscarLibro = new TextBox();
            btnSolicitarPrestamo = new Button();
            btnEliminarLibro = new Button();
            btnEditarLibro = new Button();
            btnAgregarLibro = new Button();
            dgvLibros = new DataGridView();
            tabPanelPrestamos = new TabPage();
            lblTituloPrestamos = new Label();
            btnDevolverPrestamo = new Button();
            dgvPrestamos = new DataGridView();
            tabPanelUsuarios = new TabPage();
            btnCrearUsuarioAdmin = new Button();
            btnEditarUsuario = new Button();
            btnEliminarUsuario = new Button();
            dgvUsuarios = new DataGridView();
            tabPanelEstadisticas = new TabPage();
            plotUsuarios = new OxyPlot.WindowsForms.PlotView();
            plotLibros = new OxyPlot.WindowsForms.PlotView();
            btnCerrarSesion = new Button();

            tabControlPrincipal.SuspendLayout();
            tabPanelLibros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLibros).BeginInit();
            tabPanelPrestamos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrestamos).BeginInit();
            tabPanelUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            tabPanelEstadisticas.SuspendLayout();
            SuspendLayout();
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBienvenida.Location = new Point(17, 25);
            lblBienvenida.Margin = new Padding(4, 0, 4, 0);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(142, 32);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "Bienvenido";
            // 
            // tabControlPrincipal
            // 
            tabControlPrincipal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlPrincipal.Controls.Add(tabPanelLibros);
            tabControlPrincipal.Controls.Add(tabPanelPrestamos);
            tabControlPrincipal.Controls.Add(tabPanelUsuarios);
            tabControlPrincipal.Controls.Add(tabPanelEstadisticas);
            tabControlPrincipal.Location = new Point(17, 83);
            tabControlPrincipal.Margin = new Padding(4, 5, 4, 5);
            tabControlPrincipal.Name = "tabControlPrincipal";
            tabControlPrincipal.SelectedIndex = 0;
            tabControlPrincipal.Size = new Size(1304, 750);
            tabControlPrincipal.TabIndex = 1;
            tabControlPrincipal.SelectedIndexChanged += tabControlPrincipal_SelectedIndexChanged;
            // 
            // tabPanelLibros
            // 
            tabPanelLibros.Controls.Add(btnBuscarLibro);
            tabPanelLibros.Controls.Add(txtBuscarLibro);
            tabPanelLibros.Controls.Add(btnSolicitarPrestamo);
            tabPanelLibros.Controls.Add(btnEliminarLibro);
            tabPanelLibros.Controls.Add(btnEditarLibro);
            tabPanelLibros.Controls.Add(btnAgregarLibro);
            tabPanelLibros.Controls.Add(dgvLibros);
            tabPanelLibros.Location = new Point(4, 34);
            tabPanelLibros.Margin = new Padding(4, 5, 4, 5);
            tabPanelLibros.Name = "tabPanelLibros";
            tabPanelLibros.Padding = new Padding(4, 5, 4, 5);
            tabPanelLibros.Size = new Size(1296, 712);
            tabPanelLibros.TabIndex = 0;
            tabPanelLibros.Text = "Catálogo de Libros";
            tabPanelLibros.UseVisualStyleBackColor = true;
            // 
            // btnBuscarLibro
            // 
            btnBuscarLibro.Location = new Point(471, 33);
            btnBuscarLibro.Margin = new Padding(4, 5, 4, 5);
            btnBuscarLibro.Name = "btnBuscarLibro";
            btnBuscarLibro.Size = new Size(143, 38);
            btnBuscarLibro.TabIndex = 2;
            btnBuscarLibro.Text = "Buscar";
            btnBuscarLibro.UseVisualStyleBackColor = true;
            btnBuscarLibro.Click += btnBuscarLibro_Click;
            // 
            // txtBuscarLibro
            // 
            txtBuscarLibro.Location = new Point(21, 33);
            txtBuscarLibro.Margin = new Padding(4, 5, 4, 5);
            txtBuscarLibro.Name = "txtBuscarLibro";
            txtBuscarLibro.PlaceholderText = "Buscar por Título o Autor...";
            txtBuscarLibro.Size = new Size(427, 31);
            txtBuscarLibro.TabIndex = 1;
            // 
            // btnSolicitarPrestamo
            // 
            btnSolicitarPrestamo.BackColor = Color.LightGreen;
            btnSolicitarPrestamo.Location = new Point(1059, 625);
            btnSolicitarPrestamo.Margin = new Padding(4, 5, 4, 5);
            btnSolicitarPrestamo.Name = "btnSolicitarPrestamo";
            btnSolicitarPrestamo.Size = new Size(214, 50);
            btnSolicitarPrestamo.TabIndex = 5;
            btnSolicitarPrestamo.Text = "Solicitar Préstamo";
            btnSolicitarPrestamo.UseVisualStyleBackColor = false;
            btnSolicitarPrestamo.Click += btnSolicitarPrestamo_Click;
            // 
            // btnEliminarLibro
            // 
            btnEliminarLibro.ForeColor = Color.Red;
            btnEliminarLibro.Location = new Point(480, 625);
            btnEliminarLibro.Margin = new Padding(4, 5, 4, 5);
            btnEliminarLibro.Name = "btnEliminarLibro";
            btnEliminarLibro.Size = new Size(214, 50);
            btnEliminarLibro.TabIndex = 4;
            btnEliminarLibro.Text = "Eliminar Seleccionado";
            btnEliminarLibro.UseVisualStyleBackColor = true;
            btnEliminarLibro.Click += btnEliminarLibro_Click;
            // 
            // btnEditarLibro
            // 
            btnEditarLibro.Location = new Point(250, 625);
            btnEditarLibro.Margin = new Padding(4, 5, 4, 5);
            btnEditarLibro.Name = "btnEditarLibro";
            btnEditarLibro.Size = new Size(214, 50);
            btnEditarLibro.TabIndex = 6;
            btnEditarLibro.Text = "Editar Libro";
            btnEditarLibro.UseVisualStyleBackColor = true;
            btnEditarLibro.Click += btnEditarLibro_Click;
            // 
            // btnAgregarLibro
            // 
            btnAgregarLibro.Location = new Point(21, 625);
            btnAgregarLibro.Margin = new Padding(4, 5, 4, 5);
            btnAgregarLibro.Name = "btnAgregarLibro";
            btnAgregarLibro.Size = new Size(214, 50);
            btnAgregarLibro.TabIndex = 3;
            btnAgregarLibro.Text = "Añadir Nuevo Libro";
            btnAgregarLibro.UseVisualStyleBackColor = true;
            btnAgregarLibro.Click += btnAgregarLibro_Click;
            // 
            // dgvLibros
            // 
            dgvLibros.AllowUserToAddRows = false;
            dgvLibros.AllowUserToDeleteRows = false;
            dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLibros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLibros.Location = new Point(21, 100);
            dgvLibros.Margin = new Padding(4, 5, 4, 5);
            dgvLibros.MultiSelect = false;
            dgvLibros.Name = "dgvLibros";
            dgvLibros.ReadOnly = true;
            dgvLibros.RowHeadersWidth = 62;
            dgvLibros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibros.Size = new Size(1252, 500);
            dgvLibros.TabIndex = 0;
            // 
            // tabPanelPrestamos
            // 
            tabPanelPrestamos.Controls.Add(lblTituloPrestamos);
            tabPanelPrestamos.Controls.Add(btnDevolverPrestamo);
            tabPanelPrestamos.Controls.Add(dgvPrestamos);
            tabPanelPrestamos.Location = new Point(4, 34);
            tabPanelPrestamos.Margin = new Padding(4, 5, 4, 5);
            tabPanelPrestamos.Name = "tabPanelPrestamos";
            tabPanelPrestamos.Padding = new Padding(4, 5, 4, 5);
            tabPanelPrestamos.Size = new Size(1296, 712);
            tabPanelPrestamos.TabIndex = 1;
            tabPanelPrestamos.Text = "Préstamos";
            tabPanelPrestamos.UseVisualStyleBackColor = true;
            // 
            // lblTituloPrestamos
            // 
            lblTituloPrestamos.AutoSize = true;
            lblTituloPrestamos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTituloPrestamos.Location = new Point(21, 33);
            lblTituloPrestamos.Margin = new Padding(4, 0, 4, 0);
            lblTituloPrestamos.Name = "lblTituloPrestamos";
            lblTituloPrestamos.Size = new Size(246, 30);
            lblTituloPrestamos.TabIndex = 0;
            lblTituloPrestamos.Text = "Registro de Préstamos";
            // 
            // btnDevolverPrestamo
            // 
            btnDevolverPrestamo.BackColor = Color.LightSkyBlue;
            btnDevolverPrestamo.Location = new Point(1063, 623);
            btnDevolverPrestamo.Margin = new Padding(4, 5, 4, 5);
            btnDevolverPrestamo.Name = "btnDevolverPrestamo";
            btnDevolverPrestamo.Size = new Size(214, 50);
            btnDevolverPrestamo.TabIndex = 2;
            btnDevolverPrestamo.Text = "Devolver Libro";
            btnDevolverPrestamo.UseVisualStyleBackColor = false;
            btnDevolverPrestamo.Click += btnDevolverPrestamo_Click;
            // 
            // dgvPrestamos
            // 
            dgvPrestamos.AllowUserToAddRows = false;
            dgvPrestamos.AllowUserToDeleteRows = false;
            dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrestamos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPrestamos.Location = new Point(21, 100);
            dgvPrestamos.Margin = new Padding(4, 5, 4, 5);
            dgvPrestamos.MultiSelect = false;
            dgvPrestamos.Name = "dgvPrestamos";
            dgvPrestamos.ReadOnly = true;
            dgvPrestamos.RowHeadersWidth = 62;
            dgvPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrestamos.Size = new Size(1256, 500);
            dgvPrestamos.TabIndex = 1;
            // 
            // tabPanelUsuarios
            // 
            tabPanelUsuarios.Controls.Add(btnCrearUsuarioAdmin);
            tabPanelUsuarios.Controls.Add(btnEditarUsuario);
            tabPanelUsuarios.Controls.Add(btnEliminarUsuario);
            tabPanelUsuarios.Controls.Add(dgvUsuarios);
            tabPanelUsuarios.Location = new Point(4, 34);
            tabPanelUsuarios.Margin = new Padding(4, 5, 4, 5);
            tabPanelUsuarios.Name = "tabPanelUsuarios";
            tabPanelUsuarios.Padding = new Padding(4, 5, 4, 5);
            tabPanelUsuarios.Size = new Size(1296, 712);
            tabPanelUsuarios.TabIndex = 2;
            tabPanelUsuarios.Text = "Gestión de Usuarios (Admin)";
            tabPanelUsuarios.UseVisualStyleBackColor = true;
            // 
            // btnCrearUsuarioAdmin
            // 
            btnCrearUsuarioAdmin.Location = new Point(21, 625);
            btnCrearUsuarioAdmin.Margin = new Padding(4, 5, 4, 5);
            btnCrearUsuarioAdmin.Name = "btnCrearUsuarioAdmin";
            btnCrearUsuarioAdmin.Size = new Size(214, 50);
            btnCrearUsuarioAdmin.TabIndex = 1;
            btnCrearUsuarioAdmin.Text = "Crear Nuevo Usuario";
            btnCrearUsuarioAdmin.UseVisualStyleBackColor = true;
            btnCrearUsuarioAdmin.Click += btnCrearUsuarioAdmin_Click;
            // 
            // btnEditarUsuario
            // 
            btnEditarUsuario.Location = new Point(257, 625);
            btnEditarUsuario.Margin = new Padding(4, 5, 4, 5);
            btnEditarUsuario.Name = "btnEditarUsuario";
            btnEditarUsuario.Size = new Size(214, 50);
            btnEditarUsuario.TabIndex = 3;
            btnEditarUsuario.Text = "Editar Usuario";
            btnEditarUsuario.UseVisualStyleBackColor = true;
            btnEditarUsuario.Click += btnEditarUsuario_Click;
            // 
            // btnEliminarUsuario
            // 
            btnEliminarUsuario.ForeColor = Color.Red;
            btnEliminarUsuario.Location = new Point(493, 625);
            btnEliminarUsuario.Margin = new Padding(4, 5, 4, 5);
            btnEliminarUsuario.Name = "btnEliminarUsuario";
            btnEliminarUsuario.Size = new Size(214, 50);
            btnEliminarUsuario.TabIndex = 2;
            btnEliminarUsuario.Text = "Eliminar Seleccionado";
            btnEliminarUsuario.UseVisualStyleBackColor = true;
            btnEliminarUsuario.Click += btnEliminarUsuario_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(21, 33);
            dgvUsuarios.Margin = new Padding(4, 5, 4, 5);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersWidth = 62;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(1256, 567);
            dgvUsuarios.TabIndex = 0;
            // 
            // tabPanelEstadisticas
            // 
            tabPanelEstadisticas.Controls.Add(plotUsuarios);
            tabPanelEstadisticas.Controls.Add(plotLibros);
            tabPanelEstadisticas.Location = new Point(4, 34);
            tabPanelEstadisticas.Margin = new Padding(4, 5, 4, 5);
            tabPanelEstadisticas.Name = "tabPanelEstadisticas";
            tabPanelEstadisticas.Padding = new Padding(4, 5, 4, 5);
            tabPanelEstadisticas.Size = new Size(1296, 712);
            tabPanelEstadisticas.TabIndex = 3;
            tabPanelEstadisticas.Text = "Estadísticas (Admin)";
            tabPanelEstadisticas.UseVisualStyleBackColor = true;
            // 
            // plotUsuarios
            // 
            plotUsuarios.Location = new Point(650, 33);
            plotUsuarios.Margin = new Padding(4, 5, 4, 5);
            plotUsuarios.Name = "plotUsuarios";
            plotUsuarios.PanCursor = Cursors.Hand;
            plotUsuarios.Size = new Size(629, 567);
            plotUsuarios.TabIndex = 1;
            plotUsuarios.Text = "a";
            plotUsuarios.ZoomHorizontalCursor = Cursors.SizeWE;
            plotUsuarios.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotUsuarios.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // plotLibros
            // 
            plotLibros.Location = new Point(21, 33);
            plotLibros.Margin = new Padding(4, 5, 4, 5);
            plotLibros.Name = "plotLibros";
            plotLibros.PanCursor = Cursors.Hand;
            plotLibros.Size = new Size(621, 567);
            plotLibros.TabIndex = 0;
            plotLibros.ZoomHorizontalCursor = Cursors.SizeWE;
            plotLibros.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotLibros.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrarSesion.BackColor = Color.LightCoral;
            btnCerrarSesion.Location = new Point(1150, 20);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(150, 40);
            btnCerrarSesion.TabIndex = 2;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // FormBiblioteca
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1334, 872);
            Controls.Add(btnCerrarSesion);
            Controls.Add(tabControlPrincipal);
            Controls.Add(lblBienvenida);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormBiblioteca";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Biblioteca";
            FormClosed += FormBiblioteca_FormClosed;
            tabControlPrincipal.ResumeLayout(false);
            tabPanelLibros.ResumeLayout(false);
            tabPanelLibros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLibros).EndInit();
            tabPanelPrestamos.ResumeLayout(false);
            tabPanelPrestamos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrestamos).EndInit();
            tabPanelUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            tabPanelEstadisticas.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}