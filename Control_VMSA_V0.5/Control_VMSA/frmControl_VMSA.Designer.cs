namespace Control_VMSA
{
    partial class Control_VMSA
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Control_VMSA));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConectar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDesconectar = new System.Windows.Forms.ToolStripMenuItem();
            this.controlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBorrar = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sta = new System.Windows.Forms.StatusStrip();
            this.lblEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btoIcoConectar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.Conectar = new System.Windows.Forms.TabPage();
            this.btoDesconectar = new System.Windows.Forms.Button();
            this.btoConectar = new System.Windows.Forms.Button();
            this.cboPuerto = new System.Windows.Forms.ComboBox();
            this.cboBaudios = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Logger = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.bt_PeticionLoggerOrientacion = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bto_ConfirmacionRecepcion = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btoSaveLog = new System.Windows.Forms.Button();
            this.btoDesactivarLog_01 = new System.Windows.Forms.Button();
            this.btoActivarLog_01 = new System.Windows.Forms.Button();
            this.Grafica = new System.Windows.Forms.TabPage();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.chart_consumo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bt_Send_ParamPID = new System.Windows.Forms.Button();
            this.tb_consKD = new System.Windows.Forms.TextBox();
            this.tb_consKI = new System.Windows.Forms.TextBox();
            this.tb_consKP = new System.Windows.Forms.TextBox();
            this.bt_ParoMI = new System.Windows.Forms.Button();
            this.bt_MarchaMI = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bto_Stop = new System.Windows.Forms.Button();
            this.bto_Iniciar = new System.Windows.Forms.Button();
            this.grbox_Grafica = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.chart_Datos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Brujula = new System.Windows.Forms.TabPage();
            this.tB_Longitud = new System.Windows.Forms.TextBox();
            this.tB_Latitud = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tB_Brujula = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.gB_Brujula = new System.Windows.Forms.GroupBox();
            this.bto_DetenerLoggerBrujula = new System.Windows.Forms.Button();
            this.bto_IniciarLoggerBrujula = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CtoMotor = new System.Windows.Forms.TabPage();
            this.tb_BasculaMotores = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_ComandoServo = new System.Windows.Forms.Button();
            this.bt_Comando_Rx = new System.Windows.Forms.Button();
            this.pB_LedMotorIzq = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tB_PotenciaMotores = new System.Windows.Forms.TextBox();
            this.trk_PotenciaMotores = new System.Windows.Forms.TrackBar();
            this.bto_ParoMotores = new System.Windows.Forms.Button();
            this.bto_MarchaMotores = new System.Windows.Forms.Button();
            this.trk_BasculaMotores = new System.Windows.Forms.TrackBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.sta.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.Conectar.SuspendLayout();
            this.Logger.SuspendLayout();
            this.Grafica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_consumo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grbox_Grafica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Datos)).BeginInit();
            this.Brujula.SuspendLayout();
            this.gB_Brujula.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.CtoMotor.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_LedMotorIzq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trk_PotenciaMotores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trk_BasculaMotores)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.controlToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConectar,
            this.mnuDesconectar});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // mnuConectar
            // 
            this.mnuConectar.Image = ((System.Drawing.Image)(resources.GetObject("mnuConectar.Image")));
            this.mnuConectar.Name = "mnuConectar";
            this.mnuConectar.Size = new System.Drawing.Size(139, 22);
            this.mnuConectar.Text = "Conectar";
            this.mnuConectar.Click += new System.EventHandler(this.mnuConectar_Click);
            // 
            // mnuDesconectar
            // 
            this.mnuDesconectar.Image = ((System.Drawing.Image)(resources.GetObject("mnuDesconectar.Image")));
            this.mnuDesconectar.Name = "mnuDesconectar";
            this.mnuDesconectar.Size = new System.Drawing.Size(139, 22);
            this.mnuDesconectar.Text = "Desconectar";
            this.mnuDesconectar.Click += new System.EventHandler(this.mnuDesconectar_Click);
            // 
            // controlToolStripMenuItem
            // 
            this.controlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBorrar});
            this.controlToolStripMenuItem.Name = "controlToolStripMenuItem";
            this.controlToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.controlToolStripMenuItem.Text = "Control";
            // 
            // mnuBorrar
            // 
            this.mnuBorrar.Image = ((System.Drawing.Image)(resources.GetObject("mnuBorrar.Image")));
            this.mnuBorrar.Name = "mnuBorrar";
            this.mnuBorrar.Size = new System.Drawing.Size(106, 22);
            this.mnuBorrar.Text = "Borrar";
            this.mnuBorrar.Click += new System.EventHandler(this.mnuBorrar_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // sta
            // 
            this.sta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstado});
            this.sta.Location = new System.Drawing.Point(0, 817);
            this.sta.Name = "sta";
            this.sta.Size = new System.Drawing.Size(760, 22);
            this.sta.TabIndex = 1;
            this.sta.Text = "statusStrip1";
            // 
            // lblEstado
            // 
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(42, 17);
            this.lblEstado.Text = "Estado";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btoIcoConectar,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(760, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btoIcoConectar
            // 
            this.btoIcoConectar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btoIcoConectar.Image = ((System.Drawing.Image)(resources.GetObject("btoIcoConectar.Image")));
            this.btoIcoConectar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btoIcoConectar.Name = "btoIcoConectar";
            this.btoIcoConectar.Size = new System.Drawing.Size(23, 22);
            this.btoIcoConectar.Text = "toolStripButton1";
            this.btoIcoConectar.Click += new System.EventHandler(this.BTO_Conectar_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.mnuDesconectar_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.mnuBorrar_Click);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.Conectar);
            this.tabMain.Controls.Add(this.Logger);
            this.tabMain.Controls.Add(this.Grafica);
            this.tabMain.Controls.Add(this.Brujula);
            this.tabMain.Controls.Add(this.CtoMotor);
            this.tabMain.Location = new System.Drawing.Point(0, 52);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(760, 667);
            this.tabMain.TabIndex = 4;
            // 
            // Conectar
            // 
            this.Conectar.Controls.Add(this.btoDesconectar);
            this.Conectar.Controls.Add(this.btoConectar);
            this.Conectar.Controls.Add(this.cboPuerto);
            this.Conectar.Controls.Add(this.cboBaudios);
            this.Conectar.Controls.Add(this.label2);
            this.Conectar.Controls.Add(this.label1);
            this.Conectar.Location = new System.Drawing.Point(4, 22);
            this.Conectar.Name = "Conectar";
            this.Conectar.Padding = new System.Windows.Forms.Padding(3);
            this.Conectar.Size = new System.Drawing.Size(752, 641);
            this.Conectar.TabIndex = 0;
            this.Conectar.Text = "Conectar";
            this.Conectar.UseVisualStyleBackColor = true;
            // 
            // btoDesconectar
            // 
            this.btoDesconectar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btoDesconectar.Location = new System.Drawing.Point(315, 172);
            this.btoDesconectar.Name = "btoDesconectar";
            this.btoDesconectar.Size = new System.Drawing.Size(206, 47);
            this.btoDesconectar.TabIndex = 6;
            this.btoDesconectar.Text = "DESCONECTAR";
            this.btoDesconectar.UseVisualStyleBackColor = false;
            this.btoDesconectar.Click += new System.EventHandler(this.mnuDesconectar_Click);
            // 
            // btoConectar
            // 
            this.btoConectar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btoConectar.Location = new System.Drawing.Point(61, 172);
            this.btoConectar.Name = "btoConectar";
            this.btoConectar.Size = new System.Drawing.Size(206, 47);
            this.btoConectar.TabIndex = 6;
            this.btoConectar.Text = "CONECTAR";
            this.btoConectar.UseVisualStyleBackColor = false;
            this.btoConectar.Click += new System.EventHandler(this.btoConectar_Click);
            // 
            // cboPuerto
            // 
            this.cboPuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPuerto.FormattingEnabled = true;
            this.cboPuerto.Location = new System.Drawing.Point(306, 84);
            this.cboPuerto.Name = "cboPuerto";
            this.cboPuerto.Size = new System.Drawing.Size(215, 28);
            this.cboPuerto.TabIndex = 5;
            // 
            // cboBaudios
            // 
            this.cboBaudios.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBaudios.FormattingEnabled = true;
            this.cboBaudios.Location = new System.Drawing.Point(306, 20);
            this.cboBaudios.Name = "cboBaudios";
            this.cboBaudios.Size = new System.Drawing.Size(215, 28);
            this.cboBaudios.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(198, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Puerto:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Velocidad de Transmisión:";
            // 
            // Logger
            // 
            this.Logger.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.Logger.Controls.Add(this.button2);
            this.Logger.Controls.Add(this.bt_PeticionLoggerOrientacion);
            this.Logger.Controls.Add(this.textBox1);
            this.Logger.Controls.Add(this.button1);
            this.Logger.Controls.Add(this.bto_ConfirmacionRecepcion);
            this.Logger.Controls.Add(this.label3);
            this.Logger.Controls.Add(this.btoSaveLog);
            this.Logger.Controls.Add(this.btoDesactivarLog_01);
            this.Logger.Controls.Add(this.btoActivarLog_01);
            this.Logger.Location = new System.Drawing.Point(4, 22);
            this.Logger.Name = "Logger";
            this.Logger.Padding = new System.Windows.Forms.Padding(3);
            this.Logger.Size = new System.Drawing.Size(752, 641);
            this.Logger.TabIndex = 1;
            this.Logger.Text = "Logger";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(137, 108);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 41);
            this.button2.TabIndex = 17;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bt_PeticionLoggerOrientacion
            // 
            this.bt_PeticionLoggerOrientacion.Location = new System.Drawing.Point(408, 255);
            this.bt_PeticionLoggerOrientacion.Name = "bt_PeticionLoggerOrientacion";
            this.bt_PeticionLoggerOrientacion.Size = new System.Drawing.Size(103, 41);
            this.bt_PeticionLoggerOrientacion.TabIndex = 16;
            this.bt_PeticionLoggerOrientacion.Text = "Petición logger orientación";
            this.bt_PeticionLoggerOrientacion.UseVisualStyleBackColor = true;
            this.bt_PeticionLoggerOrientacion.Click += new System.EventHandler(this.bt_PeticionLoggerOrientacion_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(533, 208);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 20);
            this.textBox1.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(408, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 41);
            this.button1.TabIndex = 14;
            this.button1.Text = "Peticion logger velo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // bto_ConfirmacionRecepcion
            // 
            this.bto_ConfirmacionRecepcion.Location = new System.Drawing.Point(408, 132);
            this.bto_ConfirmacionRecepcion.Name = "bto_ConfirmacionRecepcion";
            this.bto_ConfirmacionRecepcion.Size = new System.Drawing.Size(104, 41);
            this.bto_ConfirmacionRecepcion.TabIndex = 13;
            this.bto_ConfirmacionRecepcion.Text = "@";
            this.bto_ConfirmacionRecepcion.UseVisualStyleBackColor = false;
            this.bto_ConfirmacionRecepcion.Click += new System.EventHandler(this.bto_ConfirmacionRecepcion_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(254, 60);
            this.label3.TabIndex = 12;
            this.label3.Text = "Log_01:  Logger de prueba\r\nLog_02:  Variables de estado\r\nLog_03:  Comandos de con" +
    "trol";
            // 
            // btoSaveLog
            // 
            this.btoSaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btoSaveLog.Location = new System.Drawing.Point(12, 151);
            this.btoSaveLog.Name = "btoSaveLog";
            this.btoSaveLog.Size = new System.Drawing.Size(81, 32);
            this.btoSaveLog.TabIndex = 11;
            this.btoSaveLog.Text = "Save Log";
            this.btoSaveLog.UseVisualStyleBackColor = true;
            this.btoSaveLog.Click += new System.EventHandler(this.btoSaveLog_Click);
            // 
            // btoDesactivarLog_01
            // 
            this.btoDesactivarLog_01.Location = new System.Drawing.Point(318, 55);
            this.btoDesactivarLog_01.Name = "btoDesactivarLog_01";
            this.btoDesactivarLog_01.Size = new System.Drawing.Size(138, 34);
            this.btoDesactivarLog_01.TabIndex = 10;
            this.btoDesactivarLog_01.Text = "Desactiva Log_01";
            this.btoDesactivarLog_01.UseVisualStyleBackColor = true;
            this.btoDesactivarLog_01.Click += new System.EventHandler(this.btoDesactivarLog_01_Click);
            // 
            // btoActivarLog_01
            // 
            this.btoActivarLog_01.Location = new System.Drawing.Point(318, 15);
            this.btoActivarLog_01.Name = "btoActivarLog_01";
            this.btoActivarLog_01.Size = new System.Drawing.Size(138, 34);
            this.btoActivarLog_01.TabIndex = 9;
            this.btoActivarLog_01.Text = "Activa Log_01";
            this.btoActivarLog_01.UseVisualStyleBackColor = true;
            this.btoActivarLog_01.Click += new System.EventHandler(this.btoActivarLog_01_Click);
            // 
            // Grafica
            // 
            this.Grafica.Controls.Add(this.textBox6);
            this.Grafica.Controls.Add(this.chart_consumo);
            this.Grafica.Controls.Add(this.bt_Send_ParamPID);
            this.Grafica.Controls.Add(this.tb_consKD);
            this.Grafica.Controls.Add(this.tb_consKI);
            this.Grafica.Controls.Add(this.tb_consKP);
            this.Grafica.Controls.Add(this.bt_ParoMI);
            this.Grafica.Controls.Add(this.bt_MarchaMI);
            this.Grafica.Controls.Add(this.groupBox1);
            this.Grafica.Controls.Add(this.grbox_Grafica);
            this.Grafica.Location = new System.Drawing.Point(4, 22);
            this.Grafica.Name = "Grafica";
            this.Grafica.Padding = new System.Windows.Forms.Padding(3);
            this.Grafica.Size = new System.Drawing.Size(752, 641);
            this.Grafica.TabIndex = 2;
            this.Grafica.Text = "Grafica";
            this.Grafica.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(659, 554);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(65, 20);
            this.textBox6.TabIndex = 11;
            this.textBox6.Text = "Consumo";
            // 
            // chart_consumo
            // 
            this.chart_consumo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_consumo.BackColor = System.Drawing.Color.Gainsboro;
            chartArea1.Name = "ChartArea1";
            this.chart_consumo.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            legend1.Title = "Consumo";
            this.chart_consumo.Legends.Add(legend1);
            this.chart_consumo.Location = new System.Drawing.Point(0, 319);
            this.chart_consumo.Name = "chart_consumo";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_consumo.Series.Add(series1);
            this.chart_consumo.Size = new System.Drawing.Size(724, 255);
            this.chart_consumo.TabIndex = 8;
            this.chart_consumo.Text = "Consumo Fadrone";
            // 
            // bt_Send_ParamPID
            // 
            this.bt_Send_ParamPID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.bt_Send_ParamPID.Location = new System.Drawing.Point(614, 605);
            this.bt_Send_ParamPID.Name = "bt_Send_ParamPID";
            this.bt_Send_ParamPID.Size = new System.Drawing.Size(90, 30);
            this.bt_Send_ParamPID.TabIndex = 7;
            this.bt_Send_ParamPID.Text = "SEND";
            this.bt_Send_ParamPID.UseVisualStyleBackColor = false;
            this.bt_Send_ParamPID.Click += new System.EventHandler(this.bt_Send_ParamPID_Click);
            // 
            // tb_consKD
            // 
            this.tb_consKD.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_consKD.Location = new System.Drawing.Point(490, 605);
            this.tb_consKD.Name = "tb_consKD";
            this.tb_consKD.Size = new System.Drawing.Size(90, 31);
            this.tb_consKD.TabIndex = 6;
            this.tb_consKD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_consKI
            // 
            this.tb_consKI.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_consKI.Location = new System.Drawing.Point(375, 605);
            this.tb_consKI.Name = "tb_consKI";
            this.tb_consKI.Size = new System.Drawing.Size(90, 31);
            this.tb_consKI.TabIndex = 5;
            this.tb_consKI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_consKP
            // 
            this.tb_consKP.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_consKP.Location = new System.Drawing.Point(264, 605);
            this.tb_consKP.Name = "tb_consKP";
            this.tb_consKP.Size = new System.Drawing.Size(90, 31);
            this.tb_consKP.TabIndex = 4;
            this.tb_consKP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bt_ParoMI
            // 
            this.bt_ParoMI.Location = new System.Drawing.Point(112, 605);
            this.bt_ParoMI.Name = "bt_ParoMI";
            this.bt_ParoMI.Size = new System.Drawing.Size(90, 30);
            this.bt_ParoMI.TabIndex = 3;
            this.bt_ParoMI.Text = "Paro MI";
            this.bt_ParoMI.UseVisualStyleBackColor = true;
            this.bt_ParoMI.Click += new System.EventHandler(this.bto_ParoMotores_Click);
            // 
            // bt_MarchaMI
            // 
            this.bt_MarchaMI.Location = new System.Drawing.Point(6, 605);
            this.bt_MarchaMI.Name = "bt_MarchaMI";
            this.bt_MarchaMI.Size = new System.Drawing.Size(90, 30);
            this.bt_MarchaMI.TabIndex = 1;
            this.bt_MarchaMI.Text = "Marcha MI";
            this.bt_MarchaMI.UseVisualStyleBackColor = true;
            this.bt_MarchaMI.Click += new System.EventHandler(this.bot_MarchaMotores_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bto_Stop);
            this.groupBox1.Controls.Add(this.bto_Iniciar);
            this.groupBox1.Location = new System.Drawing.Point(461, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 51);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control de la ejecución";
            // 
            // bto_Stop
            // 
            this.bto_Stop.Location = new System.Drawing.Point(144, 19);
            this.bto_Stop.Name = "bto_Stop";
            this.bto_Stop.Size = new System.Drawing.Size(111, 25);
            this.bto_Stop.TabIndex = 1;
            this.bto_Stop.Text = "Stop";
            this.bto_Stop.UseVisualStyleBackColor = true;
            this.bto_Stop.Click += new System.EventHandler(this.bto_Stop_Click);
            // 
            // bto_Iniciar
            // 
            this.bto_Iniciar.Location = new System.Drawing.Point(5, 19);
            this.bto_Iniciar.Name = "bto_Iniciar";
            this.bto_Iniciar.Size = new System.Drawing.Size(117, 26);
            this.bto_Iniciar.TabIndex = 0;
            this.bto_Iniciar.Text = "Iniciar";
            this.bto_Iniciar.UseVisualStyleBackColor = true;
            this.bto_Iniciar.Click += new System.EventHandler(this.bto_Iniciar_Click);
            // 
            // grbox_Grafica
            // 
            this.grbox_Grafica.Controls.Add(this.textBox5);
            this.grbox_Grafica.Controls.Add(this.chart_Datos);
            this.grbox_Grafica.Location = new System.Drawing.Point(-4, 57);
            this.grbox_Grafica.Name = "grbox_Grafica";
            this.grbox_Grafica.Size = new System.Drawing.Size(728, 301);
            this.grbox_Grafica.TabIndex = 1;
            this.grbox_Grafica.TabStop = false;
            this.grbox_Grafica.Text = "Grafica";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(661, 239);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(65, 20);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "Velocidad";
            // 
            // chart_Datos
            // 
            this.chart_Datos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart_Datos.BackColor = System.Drawing.Color.Gainsboro;
            chartArea2.Name = "ChartArea1";
            this.chart_Datos.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            legend2.Title = "Velocidad";
            this.chart_Datos.Legends.Add(legend2);
            this.chart_Datos.Location = new System.Drawing.Point(0, 14);
            this.chart_Datos.Name = "chart_Datos";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_Datos.Series.Add(series2);
            this.chart_Datos.Size = new System.Drawing.Size(728, 255);
            this.chart_Datos.TabIndex = 9;
            this.chart_Datos.Text = "Datos entrada analógica";
            // 
            // Brujula
            // 
            this.Brujula.Controls.Add(this.tB_Longitud);
            this.Brujula.Controls.Add(this.tB_Latitud);
            this.Brujula.Controls.Add(this.textBox4);
            this.Brujula.Controls.Add(this.tB_Brujula);
            this.Brujula.Controls.Add(this.textBox3);
            this.Brujula.Controls.Add(this.textBox2);
            this.Brujula.Controls.Add(this.gB_Brujula);
            this.Brujula.Controls.Add(this.pictureBox1);
            this.Brujula.Location = new System.Drawing.Point(4, 22);
            this.Brujula.Name = "Brujula";
            this.Brujula.Padding = new System.Windows.Forms.Padding(3);
            this.Brujula.Size = new System.Drawing.Size(752, 641);
            this.Brujula.TabIndex = 3;
            this.Brujula.Text = "Brujula";
            this.Brujula.UseVisualStyleBackColor = true;
            // 
            // tB_Longitud
            // 
            this.tB_Longitud.Location = new System.Drawing.Point(601, 128);
            this.tB_Longitud.Name = "tB_Longitud";
            this.tB_Longitud.Size = new System.Drawing.Size(117, 20);
            this.tB_Longitud.TabIndex = 3;
            // 
            // tB_Latitud
            // 
            this.tB_Latitud.Location = new System.Drawing.Point(435, 128);
            this.tB_Latitud.Name = "tB_Latitud";
            this.tB_Latitud.Size = new System.Drawing.Size(117, 20);
            this.tB_Latitud.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(565, 131);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(30, 13);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "LON: ";
            // 
            // tB_Brujula
            // 
            this.tB_Brujula.Location = new System.Drawing.Point(565, 93);
            this.tB_Brujula.Name = "tB_Brujula";
            this.tB_Brujula.Size = new System.Drawing.Size(117, 20);
            this.tB_Brujula.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(403, 131);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(30, 13);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "LAT: ";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(476, 96);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 13);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "ORIENTACIÓN: ";
            // 
            // gB_Brujula
            // 
            this.gB_Brujula.Controls.Add(this.bto_DetenerLoggerBrujula);
            this.gB_Brujula.Controls.Add(this.bto_IniciarLoggerBrujula);
            this.gB_Brujula.Location = new System.Drawing.Point(456, 6);
            this.gB_Brujula.Name = "gB_Brujula";
            this.gB_Brujula.Size = new System.Drawing.Size(260, 57);
            this.gB_Brujula.TabIndex = 2;
            this.gB_Brujula.TabStop = false;
            this.gB_Brujula.Text = "Control ejecución";
            // 
            // bto_DetenerLoggerBrujula
            // 
            this.bto_DetenerLoggerBrujula.Location = new System.Drawing.Point(134, 19);
            this.bto_DetenerLoggerBrujula.Name = "bto_DetenerLoggerBrujula";
            this.bto_DetenerLoggerBrujula.Size = new System.Drawing.Size(120, 30);
            this.bto_DetenerLoggerBrujula.TabIndex = 0;
            this.bto_DetenerLoggerBrujula.Text = "Detener";
            this.bto_DetenerLoggerBrujula.UseVisualStyleBackColor = true;
            this.bto_DetenerLoggerBrujula.Click += new System.EventHandler(this.bto_DetenerLoggerBrujula_Click);
            // 
            // bto_IniciarLoggerBrujula
            // 
            this.bto_IniciarLoggerBrujula.Location = new System.Drawing.Point(6, 19);
            this.bto_IniciarLoggerBrujula.Name = "bto_IniciarLoggerBrujula";
            this.bto_IniciarLoggerBrujula.Size = new System.Drawing.Size(120, 30);
            this.bto_IniciarLoggerBrujula.TabIndex = 0;
            this.bto_IniciarLoggerBrujula.Text = "Iniciar";
            this.bto_IniciarLoggerBrujula.UseVisualStyleBackColor = true;
            this.bto_IniciarLoggerBrujula.Click += new System.EventHandler(this.bto_IniciarLoggerBrujula_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Control_VMSA.Properties.Resources.Brujula;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(186, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 190);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // CtoMotor
            // 
            this.CtoMotor.Controls.Add(this.tb_BasculaMotores);
            this.CtoMotor.Controls.Add(this.groupBox2);
            this.CtoMotor.Controls.Add(this.trk_BasculaMotores);
            this.CtoMotor.Location = new System.Drawing.Point(4, 22);
            this.CtoMotor.Name = "CtoMotor";
            this.CtoMotor.Padding = new System.Windows.Forms.Padding(3);
            this.CtoMotor.Size = new System.Drawing.Size(752, 641);
            this.CtoMotor.TabIndex = 4;
            this.CtoMotor.Text = "Control Motor";
            this.CtoMotor.UseVisualStyleBackColor = true;
            // 
            // tb_BasculaMotores
            // 
            this.tb_BasculaMotores.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_BasculaMotores.Location = new System.Drawing.Point(275, 393);
            this.tb_BasculaMotores.Name = "tb_BasculaMotores";
            this.tb_BasculaMotores.Size = new System.Drawing.Size(138, 31);
            this.tb_BasculaMotores.TabIndex = 5;
            this.tb_BasculaMotores.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_BasculaMotores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tB_BasculaMotores_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_ComandoServo);
            this.groupBox2.Controls.Add(this.bt_Comando_Rx);
            this.groupBox2.Controls.Add(this.pB_LedMotorIzq);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.tB_PotenciaMotores);
            this.groupBox2.Controls.Add(this.trk_PotenciaMotores);
            this.groupBox2.Controls.Add(this.bto_ParoMotores);
            this.groupBox2.Controls.Add(this.bto_MarchaMotores);
            this.groupBox2.Location = new System.Drawing.Point(24, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(670, 284);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control de Potencia";
            // 
            // bt_ComandoServo
            // 
            this.bt_ComandoServo.Location = new System.Drawing.Point(134, 228);
            this.bt_ComandoServo.Name = "bt_ComandoServo";
            this.bt_ComandoServo.Size = new System.Drawing.Size(103, 41);
            this.bt_ComandoServo.TabIndex = 20;
            this.bt_ComandoServo.Text = "SERVO";
            this.bt_ComandoServo.UseVisualStyleBackColor = true;
            this.bt_ComandoServo.Click += new System.EventHandler(this.bt_ComandoServo_Click);
            // 
            // bt_Comando_Rx
            // 
            this.bt_Comando_Rx.Location = new System.Drawing.Point(25, 228);
            this.bt_Comando_Rx.Name = "bt_Comando_Rx";
            this.bt_Comando_Rx.Size = new System.Drawing.Size(103, 41);
            this.bt_Comando_Rx.TabIndex = 19;
            this.bt_Comando_Rx.Text = "RX";
            this.bt_Comando_Rx.UseVisualStyleBackColor = true;
            this.bt_Comando_Rx.Click += new System.EventHandler(this.bt_Comando_Rx_Click);
            // 
            // pB_LedMotorIzq
            // 
            this.pB_LedMotorIzq.Image = global::Control_VMSA.Properties.Resources.Led_verde;
            this.pB_LedMotorIzq.Location = new System.Drawing.Point(301, 83);
            this.pB_LedMotorIzq.Name = "pB_LedMotorIzq";
            this.pB_LedMotorIzq.Size = new System.Drawing.Size(50, 50);
            this.pB_LedMotorIzq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pB_LedMotorIzq.TabIndex = 4;
            this.pB_LedMotorIzq.TabStop = false;
            this.pB_LedMotorIzq.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Control_VMSA.Properties.Resources.Led_rojo;
            this.pictureBox2.Location = new System.Drawing.Point(301, 83);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // tB_PotenciaMotores
            // 
            this.tB_PotenciaMotores.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_PotenciaMotores.Location = new System.Drawing.Point(251, 156);
            this.tB_PotenciaMotores.Name = "tB_PotenciaMotores";
            this.tB_PotenciaMotores.Size = new System.Drawing.Size(138, 31);
            this.tB_PotenciaMotores.TabIndex = 2;
            this.tB_PotenciaMotores.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tB_PotenciaMotores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tB_PotenciaMotores_KeyPress);
            // 
            // trk_PotenciaMotores
            // 
            this.trk_PotenciaMotores.LargeChange = 15;
            this.trk_PotenciaMotores.Location = new System.Drawing.Point(608, 19);
            this.trk_PotenciaMotores.Maximum = 255;
            this.trk_PotenciaMotores.Name = "trk_PotenciaMotores";
            this.trk_PotenciaMotores.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trk_PotenciaMotores.Size = new System.Drawing.Size(45, 250);
            this.trk_PotenciaMotores.TabIndex = 1;
            this.trk_PotenciaMotores.ValueChanged += new System.EventHandler(this.trk_PotenciaMotores_ValueChanged);
            // 
            // bto_ParoMotores
            // 
            this.bto_ParoMotores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.bto_ParoMotores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bto_ParoMotores.Location = new System.Drawing.Point(389, 87);
            this.bto_ParoMotores.Name = "bto_ParoMotores";
            this.bto_ParoMotores.Size = new System.Drawing.Size(138, 46);
            this.bto_ParoMotores.TabIndex = 0;
            this.bto_ParoMotores.Text = "Paro";
            this.bto_ParoMotores.UseVisualStyleBackColor = false;
            this.bto_ParoMotores.Click += new System.EventHandler(this.bto_ParoMotores_Click);
            // 
            // bto_MarchaMotores
            // 
            this.bto_MarchaMotores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.bto_MarchaMotores.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bto_MarchaMotores.Location = new System.Drawing.Point(112, 87);
            this.bto_MarchaMotores.Name = "bto_MarchaMotores";
            this.bto_MarchaMotores.Size = new System.Drawing.Size(138, 46);
            this.bto_MarchaMotores.TabIndex = 0;
            this.bto_MarchaMotores.Text = "Marcha";
            this.bto_MarchaMotores.UseVisualStyleBackColor = false;
            this.bto_MarchaMotores.Click += new System.EventHandler(this.bot_MarchaMotores_Click);
            // 
            // trk_BasculaMotores
            // 
            this.trk_BasculaMotores.LargeChange = 15;
            this.trk_BasculaMotores.Location = new System.Drawing.Point(24, 332);
            this.trk_BasculaMotores.Maximum = 255;
            this.trk_BasculaMotores.Name = "trk_BasculaMotores";
            this.trk_BasculaMotores.Size = new System.Drawing.Size(682, 45);
            this.trk_BasculaMotores.TabIndex = 1;
            this.trk_BasculaMotores.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trk_BasculaMotores.Value = 128;
            this.trk_BasculaMotores.ValueChanged += new System.EventHandler(this.trk_BasculaMotores_ValueChanged);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtLog.Location = new System.Drawing.Point(0, 725);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(760, 92);
            this.txtLog.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Control_VMSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 839);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.sta);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Control_VMSA";
            this.Text = "Controlador VMSA";
            this.Load += new System.EventHandler(this.Control_VMSA_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.sta.ResumeLayout(false);
            this.sta.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.Conectar.ResumeLayout(false);
            this.Conectar.PerformLayout();
            this.Logger.ResumeLayout(false);
            this.Logger.PerformLayout();
            this.Grafica.ResumeLayout(false);
            this.Grafica.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_consumo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grbox_Grafica.ResumeLayout(false);
            this.grbox_Grafica.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Datos)).EndInit();
            this.Brujula.ResumeLayout(false);
            this.Brujula.PerformLayout();
            this.gB_Brujula.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.CtoMotor.ResumeLayout(false);
            this.CtoMotor.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_LedMotorIzq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trk_PotenciaMotores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trk_BasculaMotores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.StatusStrip sta;
        private System.Windows.Forms.ToolStripStatusLabel lblEstado;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btoIcoConectar;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage Conectar;
        private System.Windows.Forms.TabPage Logger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPuerto;
        private System.Windows.Forms.ComboBox cboBaudios;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btoConectar;
        private System.Windows.Forms.Button btoDesconectar;
        private System.Windows.Forms.ToolStripMenuItem mnuConectar;
        private System.Windows.Forms.ToolStripMenuItem mnuDesconectar;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuBorrar;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button btoActivarLog_01;
        private System.Windows.Forms.Button btoDesactivarLog_01;
        private System.Windows.Forms.Button btoSaveLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bto_ConfirmacionRecepcion;
        private System.Windows.Forms.TabPage Grafica;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Datos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bto_Stop;
        private System.Windows.Forms.Button bto_Iniciar;
        private System.Windows.Forms.GroupBox grbox_Grafica;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage Brujula;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gB_Brujula;
        private System.Windows.Forms.Button bto_DetenerLoggerBrujula;
        private System.Windows.Forms.Button bto_IniciarLoggerBrujula;
        private System.Windows.Forms.TextBox tB_Brujula;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tB_Longitud;
        private System.Windows.Forms.TextBox tB_Latitud;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tb_consKD;
        private System.Windows.Forms.TextBox tb_consKI;
        private System.Windows.Forms.TextBox tb_consKP;
        private System.Windows.Forms.Button bt_ParoMI;
        private System.Windows.Forms.Button bt_MarchaMI;
        private System.Windows.Forms.TabPage CtoMotor;
        private System.Windows.Forms.TrackBar trk_BasculaMotores;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pB_LedMotorIzq;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox tB_PotenciaMotores;
        private System.Windows.Forms.TrackBar trk_PotenciaMotores;
        private System.Windows.Forms.Button bto_ParoMotores;
        private System.Windows.Forms.Button bto_MarchaMotores;
        private System.Windows.Forms.Button bt_Send_ParamPID;
        private System.Windows.Forms.Button bt_PeticionLoggerOrientacion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_BasculaMotores;
        private System.Windows.Forms.Button bt_Comando_Rx;
        private System.Windows.Forms.Button bt_ComandoServo;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_consumo;
        private System.Windows.Forms.TextBox textBox5;
    }
}

