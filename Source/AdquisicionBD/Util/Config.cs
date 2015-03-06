using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace Util
{
    public class Config
    {        
        public static string RutaConfig;
        // Access a configuration file using mapping.
        // This function uses the OpenMappedExeConfiguration 
        // method to access a new configuration file.   
        // It also gets the custom ConsoleSection and 
        // sets its ConsoleEment BackgroundColor and
        // ForegroundColor properties to green and red
        // respectively. Then it uses these properties to
        // set the console colors.  
        public static void IniciarConfiguracion(string vRutaConfig)
        {
            try
            {
                RutaConfig = vRutaConfig;
                //// Get the application configuration file.
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (!File.Exists(RutaConfig))
                {
                    // send("Se requiere fichero configuración:" + configFile, LogLevel.Error);
                    // return;
                    //// Create a new configuration file by saving 
                    //// the application configuration to a new file.                       
                    config.SaveAs(RutaConfig, ConfigurationSaveMode.Full);
                }


                config = GetConfiguracion();

                // Make changes to the new configuration file. 
                // This is to show that this file is the 
                // one that is used.
                //AppSettingsSection section = (AppSettingsSection)config.GetSection("appSettings");
                //if (section == null)
                if (config.AppSettings.Settings.Count == 0)
                {
                    config.AppSettings.Settings.Add("Fb_Database", string.Concat(Environment.CurrentDirectory, @"\BaseDato\2904.fdb"));
                    config.AppSettings.Settings.Add("Fb_DataSource", "LOCALHOST");
                    config.AppSettings.Settings.Add("Fb_User", "SYSDBA");
                    config.AppSettings.Settings.Add("Fb_Pass", "masterkey");
                    //config.AppSettings.Settings.Add("MSA_Database", "");
                    //config.AppSettings.Settings.Add("LogApp", "false");
                    //config.AppSettings.Settings.Add("LogError", "true");
                }

                if (config.ConnectionStrings.ConnectionStrings.Count == 0)
                {
                    // Create a connection string element.
                    ConnectionStringSettings csSettings;
                    csSettings = new ConnectionStringSettings("SQLite", @"DataSource=localhost; Database=" +
                            string.Concat(Environment.CurrentDirectory, @"\BaseDato\2904.fdb") + "; UserId=SYSDBA; Pwd=masterkey; Port=3050;Dialect=3; Charset=NONE;Role=;");
                    config.ConnectionStrings.ConnectionStrings.Add(csSettings);
                    //csSettings = new ConnectionStringSettings("MSAccess", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Geonica\Geonica Suite 3K\DataBase\MeteoStation.mdb");
                    //config.ConnectionStrings.ConnectionStrings.Add(csSettings);
                }

                //  ClientSettingsSection section = (ClientSettingsSection) config.GetSection("applicationSettings/SG.Server.Properties.Settings");

                //string servername = section.Settings.Get("server").Value.ValueXml.InnerText;
                //string port = section.Settings.Get("port").Value.ValueXml.InnerText;
                //string file = section.Settings.Get("projectionfilelocation").Value.ValueXml.InnerText;

                // Save the configuration file.
                config.Save(ConfigurationSaveMode.Modified);
                // Force a reload of the changed section. This 
                // makes the new values available for reading.      
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.RefreshSection("ConnectionStrings");

            }
            catch (ConfigurationErrorsException e)
            {
                Log.InsertaLogErrores("Excepcion IniciarConfiguracion: " + e.Message);
                //Console.WriteLine("[CreateAppSettings: {0}]",
                //    e.ToString());
            }

        }

        public static System.Configuration.Configuration GetConfiguracion()
        {

            //string appName = Environment.CurrentDirectory;
            //string configFile = string.Concat(appName, @"\SAIH.config");
            // Map the new configuration file.
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = RutaConfig;
            // Get the mapped configuration file
            return ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }

        public static string leerSetting(string stName, string vDefecto = "", System.Configuration.Configuration config = null)
        {
            // Para leer un parámetro de configuración
            string Result = "";
            try
            {
                if (config == null)
                    Result = ConfigurationManager.AppSettings[stName];
                else
                    Result = config.AppSettings.Settings[stName].Value;
            }
            catch
            {
                //
            }
            if ((Result == null) || (Result == ""))
                Result = vDefecto;
            return Result;
        }

        public static string leerConnectionString(string stName, string vDefecto, System.Configuration.Configuration config = null)
        {
            string Result = "";
            // Para leer un parámetro de configuración
            try
            {
                if (config == null)
                    Result = ConfigurationManager.ConnectionStrings[stName].ConnectionString;
                else
                    Result = config.ConnectionStrings.ConnectionStrings[stName].ConnectionString;
            }
            catch
            {
                // 
            }
            if (Result == "")
                Result = vDefecto;
            return Result;
        }

        /// <summary>
        /// Lee el modo de configuración REVSA, si está correcto devuelve true
        /// </summary>
        /// <param name="modo"></param>
        public static void leerModoRevsa(ref string modo)
        {
            // Get the mapped configuration file
            System.Configuration.Configuration config = GetConfiguracion();
            modo = "false";

            // Para leer un parámetro de configuración
            // baseDatos = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
            foreach(string key in config.AppSettings.Settings.AllKeys)
            {
                if(key=="ModoRV")
                {
                    modo = config.AppSettings.Settings["ModoRV"].Value;
                }
            }

            
        
            //MSA_Database = config.AppSettings.Settings["MSA_Database"].Value;



        }

        public static void leerConfiguracionFb(ref string Fb_DataSource, ref string Fb_Database, ref string Fb_User, ref string Fb_Pass, ref string MSA_Database)
        {
            // Get the mapped configuration file
            System.Configuration.Configuration config = GetConfiguracion();

            // Para leer un parámetro de configuración
            // baseDatos = ConfigurationManager.ConnectionStrings["SQLite"].ConnectionString;
            Fb_Database = config.AppSettings.Settings["Fb_Database"].Value;
            Fb_DataSource = config.AppSettings.Settings["Fb_DataSource"].Value;
            Fb_User = config.AppSettings.Settings["Fb_User"].Value;
            Fb_Pass = config.AppSettings.Settings["Fb_Pass"].Value;
            //MSA_Database = config.AppSettings.Settings["MSA_Database"].Value;



        }

        public static void EscrbirConfiguracion(string Fb_DataSource, string Fb_Database, string Fb_User, string Fb_Pass, string MSA_Database)
        {
            System.Configuration.Configuration config = GetConfiguracion();
            // Para modificar y persistir un parámetro de configuración            
            config.AppSettings.Settings["Fb_Database"].Value = Fb_Database;
            config.AppSettings.Settings["Fb_DataSource"].Value = Fb_DataSource;
            config.AppSettings.Settings["Fb_User"].Value = Fb_User;
            config.AppSettings.Settings["Fb_Pass"].Value = Fb_Pass;
            //config.AppSettings.Settings["MSA_Database"].Value = MSA_Database;
            config.Save(ConfigurationSaveMode.Modified);

            //ConfigurationManager.RefreshSection("appSettings");
            //ConfigurationManager.RefreshSection("ConnectionStrings");
        } //fin inicializa sesion
    }//end class util
}
