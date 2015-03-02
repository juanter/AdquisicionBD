using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
// using System.Configuration;
// using System.Security;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

// using System.Runtime.Remoting.Messaging;

namespace Util
{    
    /*********************************  HISTORICO ARCHIVO   *************************
     * 2012-04 (Juan Míguez): CREACIÓN   
     * 2012-05-24 (Javier Calvo): Reconfiguración de funciones: cambios de propiedades públicas, descripción de funcionamiento
     * 
     * 
     * **********************************************************************************/

    /// <summary>
    /// Establece los métodos necesarios para crear un archivo  de texto de Log de funcionamiento y otro de errores en la aplicación
    /// </summary>
    public class Log
    {
        const int cMaxFilesize = 1024 * 1024 * 5; // Mayor 5 MBytes
        public static bool LogApp = false;
        public static bool LogError = true;
        public static bool LogFicherosXML = false;
        public static string FicheroApplog = @"\Logs\{0}.log";
        public static string FicheroErroreslog = @"\Logs\Errores.log";
        public static string CarpetaFicherosXML = @"\Logs\XML\";
        private static int contlog = 20;
        private static int contErrlog = 20;

        /// <summary>
        /// Lee la configuración
        /// </summary>
        public static void LeerConfiguracion()
        {
            System.Configuration.Configuration config;
            try
            {
                string straux;
                config = Config.GetConfiguracion();
                straux = Config.leerSetting("LogApp", "false", config);
                if (straux.ToLower() == "true")
                    LogApp = true;
                else
                    LogApp = false;

                straux = Config.leerSetting("LogError", "true", config);
                if (straux.ToLower() == "true")
                    LogError = true;
                else
                    LogError = false;

                straux = Config.leerSetting("LogFicherosXML", "true", config);
                if (straux.ToLower() == "true")
                    LogFicherosXML = true;
                else
                    LogFicherosXML = false;
            }
            finally
            {
                config = null;
            }
        }


        /// <summary>
        /// Controla el tamaño del archivo. Al superar el tamaño especificado se archiva con la fecha hora actual y se crea uno nuevo
        /// </summary>
        /// <param name="RutaArchivo">Archivo a controlar</param>
        /// <param name="cont">Nº de escrituras a partir de las cuales se controla el tamaño</param>
        /// <returns>Tamaño del archivo</returns>
        private static bool ControlSize(string RutaArchivo,ref int cont)
        {
            bool resultado = false;
            if (cont >= 20)
            {
                if (File.Exists(RutaArchivo))
                {
                    FileInfo f = new FileInfo(RutaArchivo);
                    if (f.Length >= cMaxFilesize) // Mayor 5 MBytes
                    {
                        string Directorio = Path.GetDirectoryName(RutaArchivo);
                        string FileName = Path.GetFileNameWithoutExtension(RutaArchivo);
                        string Ext = Path.GetExtension(RutaArchivo);
                        File.Move(RutaArchivo, Directorio + "\\" + FileName + DateTime.Now.ToString("_dd-MM-yy_HHmmss") + Ext);
                    }
                    f = null;
                    cont = 0;
                    resultado = true;
                }
                else cont = 0;
            }
            else
                cont++;
            return resultado;
        }

        /// <summary>
        /// Añade una línea al archivo de Log del sistema
        /// </summary>
        /// <param name="Mensaje">Texto a escribir</param>
        /// <param name="showMemoryUsage">True para mostrar tamaño memoria sistema, por defecto es false</param>
        public static void InsertaLogApp(string Mensaje, bool showMemoryUsage = false)
        {
            if (LogApp)
            {
                try
                {
                    string RutaArchivo = Environment.CurrentDirectory + String.Format(FicheroApplog, Path.GetFileNameWithoutExtension(Application.ExecutablePath));
                    if (ControlSize(RutaArchivo, ref contlog))
                        contlog = 0;
                    InsertaLog(Mensaje, RutaArchivo, showMemoryUsage);
                }
                finally
                { 
                }

            }
        }

        /// <summary>
        /// Añade una línea al archivo Log de errores
        /// </summary>
        /// <param name="Mensaje">Texto a escribir</param>
        /// /// <param name="showMemoryUsage">True para mostrar tamaño memoria sistema, por defecto es false</param>
        public static void InsertaLogErrores(string Mensaje, bool showMemoryUsage = false)
        {
            if (LogError)
            {
                try
                {
                    string RutaArchivo = Environment.CurrentDirectory + FicheroErroreslog;
                    if (ControlSize(RutaArchivo, ref contErrlog))
                        contErrlog = 0;
                    InsertaLog(Mensaje, RutaArchivo, showMemoryUsage);
                }
                finally
                { 
                }
            }
        }


        /// <summary>
        /// Inserta una línea de registro en el archivo especificado
        /// </summary>
        /// <param name="Mensaje">Texto a archivar</param>
        /// <param name="Fichero">Fichero en el que se almacena</param>
        /// <param name="showMemoryUsage">True= se guarda el tamaño en memoria de la aplicación</param>
        private static void InsertaLog(string Mensaje, string Fichero = "", bool showMemoryUsage = false)
        {
            string RutaArchivo = "";
            string val;
            
            if (showMemoryUsage)
                val = DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " - " + Mensaje + string.Format(" |Mem {0:0}KB|", Log.AppMemoryUsage() / 1024);
            else
                val = DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " - " + Mensaje;                  

            DirectoryInfo directoryInfo = null;
            FileStream fileStream = null;
            StreamWriter streamWriter = null;

            if (Fichero == "")
                RutaArchivo = Environment.CurrentDirectory + FicheroApplog;
            else
                RutaArchivo = Fichero;

            try
            {
                if (File.Exists(RutaArchivo))
                {                    
                    fileStream = new FileStream(RutaArchivo, FileMode.Append, FileAccess.Write);
                    streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(val);
                }
                else
                {
                    //If file doesn't exist create one
                    // Path.GetDirectoryName(yourPath).Split(@"\/", StringSplitOptions.RemoveEmptyEntries);RutaArchivo)
                    string Directorio = Path.GetDirectoryName(RutaArchivo);
                    directoryInfo = new DirectoryInfo(Directorio);
                    directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);
                    fileStream = File.Create(RutaArchivo);                    
                    streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(val);
                }
            }
            finally
            {
                if (directoryInfo != null)
                    directoryInfo = null;

                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                    streamWriter = null;
                }
                if (fileStream != null)
                {
                    fileStream.Dispose();
                    fileStream = null;
                }

            }
        }

        public static void InsertaXML(XDocument xmlObservacion, string Fichero = "")
        {
            string RutaArchivo = "";           
            DirectoryInfo directoryInfo = null;

            RutaArchivo = Environment.CurrentDirectory + Log.CarpetaFicherosXML + Fichero;
            string Directorio = Path.GetDirectoryName(RutaArchivo);

            try
            {
                if (!Directory.Exists(Directorio))
                {
                    directoryInfo = new DirectoryInfo(Directorio);
                    directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);
                }
                xmlObservacion.Save(RutaArchivo);

            }
            finally
            {
                if (directoryInfo != null)
                    directoryInfo = null;
            }
        }
		
        /// <summary>
        /// Calcula el tamaño en memoria de la aplicación
        /// </summary>
        /// <returns>Memoria ocupada por la aplicación</returns>
        public static long AppMemoryUsage()
        {
            Process currentProc = Process.GetCurrentProcess();
            long memoryUsed = currentProc.PrivateMemorySize64;
            currentProc = null; 
            return memoryUsed;
        }
    }
}
