﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

namespace Control_VMSA
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string appName = Environment.CurrentDirectory;
            string configFile = string.Concat(appName, @"\Adquisicion.config");
            Config.IniciarConfiguracion(configFile);
            Log.LeerConfiguracion();
            UnicaInstancia.Run(new Control_VMSA());
            // Application.Run(new Control_VMSA());
            //UnicaInstancia.Run(new Form1()); // para lanzar una sola vez
        }
    }
}