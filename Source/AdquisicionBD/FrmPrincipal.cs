﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Aplicacion.DB;
using Util;

namespace Aplicacion
{   
    public partial class FrmPrincipal : Form
    {
        private Adquision _adquisicion;
        private long MemoriaAntesCerrar = 0;
        // private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public FrmPrincipal()
        {                      
            InitializeComponent();            
        }

        // la declaramos fuera de la función, para que mantenga su valor
        Boolean PrimeraVez = true;
        Boolean PuedeCerrar = false;
        //
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ////' Asignar los submenús del ContextMenu
            ////'
            ////' Añadimos la opción Restaurar, que será el elemento predeterminado
            ////          MenuItem tMenu = new MenuItem("&Restaurar", new EventHandler(this.Restaurar_Click));
            ////          tMenu.DefaultItem = true;
            ////          ContextMenu1.MenuItems.Add(tMenu);
            ////
            ////' Esto también se puede hacer así:
            //ContextMenu1.MenuItems.Add("&Restaurar", new EventHandler(this.Restaurar_Click));
            //ContextMenu1.MenuItems[0].DefaultItem = true;
            ////'
            ////' Añadimos un separador
            //ContextMenu1.MenuItems.Add("-");
            ////' Añadimos el elemento Acerca de...
            //ContextMenu1.MenuItems.Add("&Acerca de...", new EventHandler(this.AcercaDe_Click));
            ////' Añadimos otro separador
            //ContextMenu1.MenuItems.Add("-");
            ////' Añadimos la opción de salir
            //ContextMenu1.MenuItems.Add("&Salir", new EventHandler(this.Salir_Click));
            ////'
            ////' Asignar los valores para el NotifyIcon
            //NotifyIcon1.Icon = this.Icon;
            //NotifyIcon1.ContextMenu = this.ContextMenu1;
            //NotifyIcon1.Text = Application.ProductName;
            //NotifyIcon1.Visible = true;
            ////
            //// Asignamos los otros eventos al formulario
            //this.Resize += new EventHandler(this.Form1_Resize);
            //this.Activated += new EventHandler(this.Form1_Activated);
            //this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            //// Asignamos el evento DoubleClick del NotifyIcon
            //this.NotifyIcon1.DoubleClick += new EventHandler(this.Restaurar_Click);
            Log.InsertaLogApp("-------- Iniciando Servidor SAIH", true);

            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Iniciando Servidor SAIH";
            notifyIcon1.ShowBalloonTip(20);

            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void FrmPrincipal_Shown(object sender, EventArgs e)
        {
            //
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.InsertaLogApp("-------- Finalizado Servidor SAIH", true);
        }        

        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                this.Visible = false;
        }

       
        private void FrmPrincipal_Activated(object sender, EventArgs e)
        {
            // En C# no se puede usar static para hacer que una variable mantenga su valor
            // en C/C++ sí que se puede
            //static Boolean PrimeraVez = true;
            //
            //' La primera vez que se active, ocultar el form,
            //' es una chapuza, pero el formulario no permite que se oculte en el Form_Load
            if (PrimeraVez)
            {
                PrimeraVez = false;
                Visible = false;
            }
        }

        private void SalirAplicacion()
        {
            DialogResult result1 = MessageBox.Show("Si cierra el Servidor dejara de importar datos.\n ¿Desea continuar?",
                    "Cerrar Servidor", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
                if (result1 == DialogResult.OK)
                {
                    MemoriaAntesCerrar = Log.AppMemoryUsage();
                    notifyIcon1.BalloonTipText = "Finalizando Servidor SAIH";
                    _adquisicion.Terminado = true;
                    backgroundWorker1.CancelAsync();

                    // WaitForWorkerToFinish(backgroundWorker1);
                    // _resetEvent.WaitOne();

                    while (backgroundWorker1.IsBusy)
                    {
                        System.Threading.Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    backgroundWorker1.Dispose();
                    //notifyIcon1.BalloonTipText = "Terminado Servidor SAIH";
                    //// backgroundWorker1.Dispose();
                    //// Cuando se va a cerrar el formulario...
                    //// eliminar el objeto de la barra de tareas
                    //notifyIcon1.Visible = false;
                    //// esto es necesario, para que no se quede el icono en la barra de tareas
                    //// (el cual se quita al pasar el ratón por encima)
                    //notifyIcon1 = null;
                    //// de paso eliminamos el menú contextual
                    //contextMenuStrip1 = null;
                    PuedeCerrar = true;
                    this.Close();
                }
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PuedeCerrar)
            {
                if (this.Visible)
                    this.Visible = false;
                e.Cancel = true;
            }
            else
            {
                notifyIcon1.BalloonTipText = "Terminado Servidor SAIH";
                // backgroundWorker1.Dispose();
                // Cuando se va a cerrar el formulario...
                // eliminar el objeto de la barra de tareas
                notifyIcon1.Visible = false;
                // esto es necesario, para que no se quede el icono en la barra de tareas
                // (el cual se quita al pasar el ratón por encima)
                notifyIcon1 = null;
                // de paso eliminamos el menú contextual
                contextMenuStrip1 = null;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            _adquisicion = new Adquision();
            _adquisicion.ControlAdquisionWait(worker, e);
            _adquisicion = null;

        }

        private DataTable _oTableStatus = null;
        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");

            lblStatus.Text = (string)e.UserState;

            if (_oTableStatus == null)
            {
                _oTableStatus = new DataTable();
                _oTableStatus.Columns.Add("FechaHora");
                _oTableStatus.Columns.Add("Status");
            }

            DataRow oRow = _oTableStatus.NewRow();
            oRow[0] = DateTime.Now.ToString();
            oRow[1] = (string)e.UserState;

            if (_oTableStatus.Rows.Count > 25)
            {
                _oTableStatus.Rows.RemoveAt(0);
            }

            _oTableStatus.Rows.Add(oRow);
            _oTableStatus.DefaultView.Sort = "FechaHora desc";//ordenación de la tabla por la fecha

            dgvStatus.DataSource = _oTableStatus;
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                lblResult.Text = "Cancelado!";
            }
            else if (e.Error != null)
            {
                lblResult.Text = "Error: " + e.Error.Message;
            }
            else
            {
                lblResult.Text = "Hecho!";
                PuedeCerrar = true;
                this.Close();
            }
            //_resetEvent.Set(); // signal that worker is done  
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalirAplicacion();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
        }
                                 
    }
}