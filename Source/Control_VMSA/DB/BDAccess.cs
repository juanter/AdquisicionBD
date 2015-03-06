using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace REVSA
{
    public class BDAccess
    {
        public OleDbConnection conexion;
        public OleDbConnectionStringBuilder conexionString;

        #region Constructores
        public BDAccess()
        {                   
            CargaDatosConexionBD();
            conexion = new OleDbConnection();
            conexion.ConnectionString = this.conexionString.ConnectionString;
        }

        ~BDAccess()
        {
            if (conexion != null)
            {
                conexion.Dispose();
                conexion = null;
            }
            if (conexionString != null)
                conexionString = null;
        }
        #endregion

        /* Rellena el objeto FBConnectionStringBuilder con los datos de la conexión a la base de datos*/
        private void CargaDatosConexionBD()
        {
            this.conexionString = new OleDbConnectionStringBuilder ();

            //Datos de la conexión
            this.conexionString.Provider = @"Microsoft.Jet.OLEDB.4.0";
            //@"D:\Clientes\Hidroelectricas Cortizo\2904. SAIH Rio Umia\BaseDatos\MeteoStation.mdb";

            this.conexionString.DataSource = Config.leerSetting("MSA_Database", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + 
                    @"\Geonica\Geonica Suite 3K\DataBase\MeteoStation.mdb",Config.GetConfiguracion());
        }

        /// <summary>
        /// Comprueba el estado de conexión a la base de datos. Abre y cierra conexión
        /// </summary>
        /// <returns> True= si existe conexión, false en caso contrario</returns>
        public Exception CompruebaConexion()
        {
            Exception error = null;
            conexion = new OleDbConnection();
                
            conexion.ConnectionString = this.conexionString.ConnectionString;
            try
            {
                conexion.Open();                
            }
            catch(Exception e){
                error = e;
            }
            finally
            {
                conexion.Dispose();    
            }
            return error;
        }

        // Select General
        public DataSet EjecutarSelect(string strQuery)
        {
            OleDbCommand objCommand=null; //Para dar orde SQL al DataAdapter
            OleDbDataAdapter objDataAdapter = null; //Para operar con la BD utilizano SQL
            DataSet objDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            bool cerrar = false;
           
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                    cerrar = true;
                }

                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                objDataSet = new DataSet();
                objDataAdapter = new OleDbDataAdapter();
                objCommand = new OleDbCommand(strQuery, conexion);
                objCommand.CommandType = CommandType.Text;

                objDataAdapter.SelectCommand = objCommand;//ejecuta el Command sobre la BD                                
                objDataAdapter.Fill(objDataSet);//se carga de datos de la BD el DataSet

                conexion.Close();
            }
            catch (Exception e)
            {
                string aux = e.Message;
            }
            finally
            {
                if (cerrar)
                    conexion.Close();
                if (objDataAdapter != null)
                {
                    objDataAdapter.Dispose();
                     objDataAdapter = null;
                }
                if (objCommand != null)
                {
                    objCommand.Dispose();
                    objCommand = null;
                }
                //conexion.Dispose();
            }
            return objDataSet;
        }        
    }
}
