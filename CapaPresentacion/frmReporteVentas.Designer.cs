namespace CapaPresentacion
{
    partial class frmReporteVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FontAwesome.Sharp.IconButton btnExportar;
            FontAwesome.Sharp.IconButton btnDescargarExcel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLimpiarBuscador = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.textBusqueda = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBusqueda = new System.Windows.Forms.ComboBox();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.FechaRegistro = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioRegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocumentoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarRegistro = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimeFin = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimeInicio = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            btnExportar = new FontAwesome.Sharp.IconButton();
            btnDescargarExcel = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExportar
            // 
            btnExportar.BackColor = System.Drawing.Color.WhiteSmoke;
            btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnExportar.ForeColor = System.Drawing.Color.Black;
            btnExportar.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnExportar.IconColor = System.Drawing.Color.LimeGreen;
            btnExportar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExportar.IconSize = 21;
            btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnExportar.Location = new System.Drawing.Point(101, 144);
            btnExportar.Name = "btnExportar";
            btnExportar.Size = new System.Drawing.Size(119, 23);
            btnExportar.TabIndex = 81;
            btnExportar.Text = "Descargar Excel";
            btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnExportar.UseVisualStyleBackColor = false;
            // 
            // btnDescargarExcel
            // 
            btnDescargarExcel.BackColor = System.Drawing.Color.WhiteSmoke;
            btnDescargarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            btnDescargarExcel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            btnDescargarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDescargarExcel.ForeColor = System.Drawing.Color.Black;
            btnDescargarExcel.IconChar = FontAwesome.Sharp.IconChar.FileExcel;
            btnDescargarExcel.IconColor = System.Drawing.Color.LimeGreen;
            btnDescargarExcel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDescargarExcel.IconSize = 21;
            btnDescargarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnDescargarExcel.Location = new System.Drawing.Point(101, 141);
            btnDescargarExcel.Name = "btnDescargarExcel";
            btnDescargarExcel.Size = new System.Drawing.Size(119, 23);
            btnDescargarExcel.TabIndex = 82;
            btnDescargarExcel.Text = "Descargar Excel";
            btnDescargarExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnDescargarExcel.UseVisualStyleBackColor = false;
            btnDescargarExcel.Click += new System.EventHandler(this.btnDescargarExcel_Click);
            // 
            // btnLimpiarBuscador
            // 
            this.btnLimpiarBuscador.BackColor = System.Drawing.Color.White;
            this.btnLimpiarBuscador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarBuscador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiarBuscador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarBuscador.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarBuscador.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnLimpiarBuscador.IconColor = System.Drawing.Color.Black;
            this.btnLimpiarBuscador.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLimpiarBuscador.IconSize = 18;
            this.btnLimpiarBuscador.Location = new System.Drawing.Point(1030, 141);
            this.btnLimpiarBuscador.Name = "btnLimpiarBuscador";
            this.btnLimpiarBuscador.Size = new System.Drawing.Size(39, 23);
            this.btnLimpiarBuscador.TabIndex = 80;
            this.btnLimpiarBuscador.UseVisualStyleBackColor = false;
            this.btnLimpiarBuscador.Click += new System.EventHandler(this.btnLimpiarBuscador_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.White;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 18;
            this.btnBuscar.Location = new System.Drawing.Point(985, 141);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(39, 23);
            this.btnBuscar.TabIndex = 79;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // textBusqueda
            // 
            this.textBusqueda.Location = new System.Drawing.Point(812, 144);
            this.textBusqueda.Name = "textBusqueda";
            this.textBusqueda.Size = new System.Drawing.Size(167, 20);
            this.textBusqueda.TabIndex = 78;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(618, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 76;
            this.label6.Text = "Buscar por:";
            // 
            // comboBusqueda
            // 
            this.comboBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBusqueda.FormattingEnabled = true;
            this.comboBusqueda.Location = new System.Drawing.Point(685, 144);
            this.comboBusqueda.Name = "comboBusqueda";
            this.comboBusqueda.Size = new System.Drawing.Size(121, 21);
            this.comboBusqueda.TabIndex = 77;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaRegistro,
            this.TipoDocumento,
            this.NumeroDocumento,
            this.MontoTotal,
            this.UsuarioRegistro,
            this.DocumentoCliente,
            this.NombreCliente,
            this.CodigoProducto,
            this.NombreProducto,
            this.Categoria,
            this.PrecioVenta,
            this.Cantidad,
            this.SubTotal});
            this.dgvData.Location = new System.Drawing.Point(89, 189);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(989, 343);
            this.dgvData.TabIndex = 75;
            // 
            // FechaRegistro
            // 
            this.FechaRegistro.HeaderText = "Fecha Registro";
            this.FechaRegistro.Name = "FechaRegistro";
            this.FechaRegistro.ReadOnly = true;
            this.FechaRegistro.Width = 80;
            // 
            // TipoDocumento
            // 
            this.TipoDocumento.HeaderText = "Tipo Documento";
            this.TipoDocumento.Name = "TipoDocumento";
            this.TipoDocumento.ReadOnly = true;
            this.TipoDocumento.Width = 80;
            // 
            // NumeroDocumento
            // 
            this.NumeroDocumento.HeaderText = "Número Documento";
            this.NumeroDocumento.Name = "NumeroDocumento";
            this.NumeroDocumento.ReadOnly = true;
            this.NumeroDocumento.Width = 80;
            // 
            // MontoTotal
            // 
            this.MontoTotal.HeaderText = "Monto Total";
            this.MontoTotal.Name = "MontoTotal";
            this.MontoTotal.ReadOnly = true;
            this.MontoTotal.Width = 80;
            // 
            // UsuarioRegistro
            // 
            this.UsuarioRegistro.HeaderText = "Usuario Registro";
            this.UsuarioRegistro.Name = "UsuarioRegistro";
            this.UsuarioRegistro.ReadOnly = true;
            this.UsuarioRegistro.Width = 80;
            // 
            // DocumentoCliente
            // 
            this.DocumentoCliente.HeaderText = "Documento Cliente";
            this.DocumentoCliente.Name = "DocumentoCliente";
            this.DocumentoCliente.ReadOnly = true;
            this.DocumentoCliente.Width = 80;
            // 
            // NombreCliente
            // 
            this.NombreCliente.HeaderText = "Nombre Cliente";
            this.NombreCliente.Name = "NombreCliente";
            this.NombreCliente.ReadOnly = true;
            this.NombreCliente.Width = 80;
            // 
            // CodigoProducto
            // 
            this.CodigoProducto.HeaderText = "Código Producto";
            this.CodigoProducto.Name = "CodigoProducto";
            this.CodigoProducto.ReadOnly = true;
            this.CodigoProducto.Width = 80;
            // 
            // NombreProducto
            // 
            this.NombreProducto.HeaderText = "Nombre Producto";
            this.NombreProducto.Name = "NombreProducto";
            this.NombreProducto.ReadOnly = true;
            this.NombreProducto.Width = 80;
            // 
            // Categoria
            // 
            this.Categoria.HeaderText = "Categoría";
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            this.Categoria.Width = 80;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            this.PrecioVenta.Width = 80;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 80;
            // 
            // SubTotal
            // 
            this.SubTotal.HeaderText = "SubTotal";
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.ReadOnly = true;
            this.SubTotal.Width = 80;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(79, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1010, 428);
            this.label5.TabIndex = 74;
            // 
            // btnBuscarRegistro
            // 
            this.btnBuscarRegistro.BackColor = System.Drawing.SystemColors.Control;
            this.btnBuscarRegistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarRegistro.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscarRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarRegistro.ForeColor = System.Drawing.Color.Black;
            this.btnBuscarRegistro.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarRegistro.IconColor = System.Drawing.Color.Black;
            this.btnBuscarRegistro.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarRegistro.IconSize = 18;
            this.btnBuscarRegistro.Location = new System.Drawing.Point(459, 67);
            this.btnBuscarRegistro.Name = "btnBuscarRegistro";
            this.btnBuscarRegistro.Size = new System.Drawing.Size(82, 23);
            this.btnBuscarRegistro.TabIndex = 73;
            this.btnBuscarRegistro.Text = "Buscar";
            this.btnBuscarRegistro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscarRegistro.UseVisualStyleBackColor = false;
            this.btnBuscarRegistro.Click += new System.EventHandler(this.btnBuscarRegistro_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(290, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Fecha Fin:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(98, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Fecha Inicio:";
            // 
            // dateTimeFin
            // 
            this.dateTimeFin.CustomFormat = "dd/MM/yyyy";
            this.dateTimeFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeFin.Location = new System.Drawing.Point(353, 69);
            this.dateTimeFin.Name = "dateTimeFin";
            this.dateTimeFin.Size = new System.Drawing.Size(100, 20);
            this.dateTimeFin.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(96, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 25);
            this.label2.TabIndex = 67;
            this.label2.Text = "Reporte Ventas";
            // 
            // dateTimeInicio
            // 
            this.dateTimeInicio.CustomFormat = "dd/MM/yyyy";
            this.dateTimeInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimeInicio.Location = new System.Drawing.Point(172, 70);
            this.dateTimeInicio.Name = "dateTimeInicio";
            this.dateTimeInicio.Size = new System.Drawing.Size(100, 20);
            this.dateTimeInicio.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(79, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1010, 103);
            this.label1.TabIndex = 65;
            // 
            // frmReporteVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 616);
            this.Controls.Add(btnDescargarExcel);
            this.Controls.Add(this.btnLimpiarBuscador);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.textBusqueda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBusqueda);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnBuscarRegistro);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimeFin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeInicio);
            this.Controls.Add(this.label1);
            this.Controls.Add(btnExportar);
            this.Name = "frmReporteVentas";
            this.Text = "frmReporteVentas";
            this.Load += new System.EventHandler(this.frmReporteVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnLimpiarBuscador;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.TextBox textBusqueda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBusqueda;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnBuscarRegistro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimeFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimeInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn FechaRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontoTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioRegistro;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocumentoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
    }
}