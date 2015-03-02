
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using System.Windows.Forms;
using REVSA;


namespace configurador
{
    public class Funciones
    {
        /// <summary>
        /// Objeto conexión Firebir con los datos de la conexion
        /// </summary>
        private static FbConnectionStringBuilder _conexion=null;

        /// <summary>
        /// Devuelve la cadena de conexión de Firebird configurada en el archivo xml Application.config
        /// </summary>
        private static string cadenaConexion
        {
            get{
                //if (_conexion == null)
                //{
                //    _conexion = new FbConnectionStringBuilder();
                //    System.Configuration.Configuration config;
                //    config = Config.GetConfiguracion();
                //    _conexion.DataSource = Config.leerSetting("Fb_DataSource", "LOCALHOST", config);
                //    _conexion.Database = Config.leerSetting("Fb_Database", Environment.CurrentDirectory + @"\2912.fbb", config);
                //    _conexion.UserID = Config.leerSetting("Fb_User", "SYSDBA", config); ;
                //    _conexion.Password = Config.leerSetting("Fb_Pass", "masterkey", config); ;
                //    _conexion.Charset = "NONE";
                //    _conexion.Pooling = true;
                //}
                //return _conexion.ToString();
                return configurador.Properties.Settings.Default.Firebird;
            }
        }

   
        /// <summary>
        /// Realiza una query a la base de datos y devuelve un dataSet con los resultados
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public static DataSet ConsultarDatosABD(string strQuery)
        {
            FbConnection conexion;
            FbDataAdapter oFbDataAdapter = null; //Para operar con la BD utilizano SQL
            DataSet oDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            int numRegistros = -1;


            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();
                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                oDataSet = new DataSet();
                oFbDataAdapter = new FbDataAdapter(strQuery, conexion);
                numRegistros = oFbDataAdapter.Fill(oDataSet);//se carga de datos de la BD el DataSet

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Faltan datos de la luminaria" + e.Message);
                Log.InsertaLogErrores("Función ConsultarDatosABD(" + strQuery + "): Error= " + e.Message, true);
            }
            finally
            {
                conexion.Dispose();
            }
            return oDataSet;
        }

        public static FbConnection ConectaBD()
        {
            FbConnection conexion;
            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();
            }
            catch (Exception e)
            {
                string aux = e.Message;
                Log.InsertaLogErrores("Función ConectaBD | Error= " + e.Message, true);
            }
            return conexion;
        }

        public static void Cierraconexion(FbConnection conexion)
        {
            try
            {
                conexion.Close();
            }
            catch (Exception e)
            {
                string aux = e.Message;
                Log.InsertaLogErrores("Función CierraConexion | Error= " + e.Message, true);
            }
            finally
            {
                conexion.Dispose();
            }


        }

        public static DataTable ConsultaTablaBD(string query, FbConnection conexion)
        {
            FbDataAdapter oFbDataAdapter;
            DataTable oDataTable = null;

            try
            {
                conexion.Open();
                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                oDataTable = new DataTable();
                oFbDataAdapter = new FbDataAdapter(query, conexion);
                oFbDataAdapter.Fill(oDataTable);//se carga de datos de la BD el DataSet

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Log.InsertaLogErrores("Función ConsultarDatosABD(" + query + ") | Error= " + e.Message, true);
            }
            finally
            {
                conexion.Dispose();
            }
            return oDataTable;
        }


        /// <summary>
        /// Ejecuta el comando scalar sobre la query pasada como referencia
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="parametros">Lista de parámetros con los datos. Si null sólo se ejecuta la query</param>
        /// <returns>true si no hay errores</returns>
        public static bool EjecutaScalar(string strQuery, FbParameter[] parametros)
        {
            FbConnection conexion;
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            bool resultado = true;
            Object objAux = null;

            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();
                oFbCommand = new FbCommand(strQuery, conexion);

                if (parametros != null)
                {
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        oFbCommand.Parameters.Add(parametros[i]);
                    }
                }
                objAux = oFbCommand.ExecuteScalar();

                //oFbDataAdapter = new FbDataAdapter();
                //oid=oFbCommand.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                Log.InsertaLogErrores("Función EjecutarScalar(" + strQuery + ") | Error= " + e.Message, true);
                resultado = false;
            }
            finally
            {
                conexion.Dispose();
            }

            return resultado;
        }



        /// <summary>
        /// Graba en la base de datos todos un registro
        /// </summary>
        /// <param name="strQuery">Query</param>
        /// <param name="parametros">Parámetros con los valores a grabar</param>
        /// <returns>Oid insertado (si la query lo indica(debe incluir returning oid)) sino el número de registros insertados</returns>
        public static int GrabarEnBDUnRegistro(string strQuery, FbParameter[] parametros)
        {
            FbConnection conexion;
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            //FbDataAdapter oFbDataAdapter;
            Object objAux = null;
            int oid=-1;

            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();
                oFbCommand = new FbCommand(strQuery, conexion);

                for (int i = 0; i < parametros.Length; i++)
                {
                    oFbCommand.Parameters.Add(parametros[i]);
                }

                //oFbDataAdapter = new FbDataAdapter();//(strQuery, conexion);
                objAux = oFbCommand.ExecuteScalar();
                if (objAux != null)
                    oid = (int)objAux;
                else
                    oid = -1; 
                //oid=oFbCommand.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                Log.InsertaLogErrores("Función GrabarEnBDUnRegistro(" + strQuery + ") | Error= " + e.Message, true);
                
            }

            return oid;
        }

        /// <summary>
        /// Graba en la base de datos todos los registros que se le pasan en la DataTable
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="parametros"></param>
        /// <param name="oTable"></param>
        /// <returns>Devuelve el número de registros almacenados</returns>
        public static int GrabarEnBDVariosRegistros(string strQuery, FbParameter[] parametros, DataTable oTable)
        {
            FbConnection conexion;
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            FbDataAdapter oFbDataAdapter;

            int numRegistros = 0;
            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;


            if (parametros.Length == oTable.Columns.Count)
            {
                try
                {
                    conexion.Open();
                    oFbCommand = new FbCommand(strQuery, conexion);

                    for (int i = 0; i < parametros.Length; i++)
                    {
                        oFbCommand.Parameters.Add(parametros[i]);
                    }

                    oFbDataAdapter = new FbDataAdapter();//(strQuery, conexion);

                    for (int i = 0; i < oTable.Rows.Count; i++)
                    {
                        DataRow oRow = oTable.Rows[i];
                        for (int j = 0; j < oFbCommand.Parameters.Count; j++)
                        {
                            oFbCommand.Parameters[j].Value = oRow[j];
                        }

                        numRegistros = numRegistros + oFbCommand.ExecuteNonQuery();
                    }

                    conexion.Close();
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    Log.InsertaLogErrores("Función GrabarEnBDVariosRegistros(" + strQuery + ") | Error= " + e.Message, true);
                }
            }

            return numRegistros;
        }


        public Exception CompruebaConexion(bool KeepConection = true)
        {
            FbConnection conexion = null;
            Exception error = null;
            if (conexion == null)
            {
                conexion = new FbConnection();
                //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
                conexion.ConnectionString = cadenaConexion;
            }
            if (conexion.State == ConnectionState.Closed)
                try
                {
                    conexion.Open();
                }
                catch (Exception e)
                {
                    error = e;
                    Log.InsertaLogErrores("Función CompruebaConexion | Error= " + e.Message, true);
                }
                finally
                {
                    if (!KeepConection)
                        conexion.Close();
                    //conexion.Dispose();    
                }
            return error;
        }
        public static void EjecutaSQL(string strQuery)
        {
            FbConnection conexion;
            conexion = new FbConnection();
           //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();
                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                FbCommand oFbCommand = new FbCommand(strQuery, conexion);
                oFbCommand.CommandType = CommandType.Text;

                oFbCommand.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception e)
            {
                  MessageBox.Show(e.Message);
                 Log.InsertaLogErrores("Función EjecutaSQL(" + strQuery + ") | Error= " + e.Message, true);
            }
            finally
            {
                conexion.Dispose();
            }

        }


        public static int GrabarEnBD(string strQuery)
        {
            FbConnection conexion;
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            int idInsertado = 0;
            Object oObjAux;


            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();


                oFbCommand = new FbCommand(strQuery, conexion);
                oObjAux = oFbCommand.ExecuteScalar();
                idInsertado = (int)oObjAux;

                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e.Message.ToString());
                Log.InsertaLogErrores("Función GrabarEnBD(" + strQuery + ") | Error= " + e.Message, true);
            }
            finally
            {
                conexion.Dispose();
            }

            return idInsertado;
        } 
        
        
        
        // Guarda y devuelve el ID del elemento guardado
        public static int RecuperarEnBD(string strQuery)
        {
            FbConnection conexion;
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            int idInsertado = 0;
            Object oObjAux;


            conexion = new FbConnection();
            //conexion.ConnectionString = configurador.Properties.Settings.Default.Firebird;
            conexion.ConnectionString = cadenaConexion;

            try
            {
                conexion.Open();


                oFbCommand = new FbCommand(strQuery, conexion);
                oObjAux = oFbCommand.ExecuteScalar();
                idInsertado = (int)oObjAux;

                conexion.Close();
            }
            catch (Exception e)
            {
                 MessageBox.Show("Error" + e.Message.ToString());
                Log.InsertaLogErrores("Función RecuperarEnBD(" + strQuery + ") | Error= " + e.Message, true);
            }
            finally
            {
                conexion.Dispose();
            }

            return idInsertado;
        } // Recupera el primer elemento de la primera fila de la primera tabla





    }
}