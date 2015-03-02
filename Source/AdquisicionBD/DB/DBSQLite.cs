using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Util;
using System.Data.Common;
using System.Configuration;
using System.Data;//Para poder utilizar DataTables
using System.IO;

namespace Aplicacion.DB
{
    public class DBSQLite
    {
        public SQLiteConnection conexion;
        public SQLiteConnectionStringBuilder conexionString;

        #region Constructores
        public DBSQLite()
        {
            CargaDatosConexionBD();
            conexion = new SQLiteConnection();
            conexion.ConnectionString = this.conexionString.ConnectionString;           

        }

        ~DBSQLite()
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

        /* Rellena el objeto SQLiteConnectionStringBuilder con los datos de la conexión a la base de datos*/
        private void CargaDatosConexionBD()
        {
            this.conexionString = new SQLiteConnectionStringBuilder();
            System.Configuration.Configuration config;
            config = Config.GetConfiguracion();
            
            this.conexionString.DataSource = Config.leerSetting("SQlite_DataSource", Environment.CurrentDirectory + @"\BBDD\Datos.sdb", config);
            this.conexionString.Password = Config.leerSetting("SQlite_Pass", "", config); ;
            this.conexionString.Version = 3;
            this.conexionString.Pooling = true;
            this.conexionString.ReadOnly = false;
            this.conexionString.FailIfMissing = false; // si true deshabilita la creacion de la base de datos si no existe
            this.conexionString.Pooling = false;
        }

        #region Métodos de Acceso a datos de SQLite
        
        /// <summary>
        /// Comprueba el estado de conexión a la base de datos. Abre y cierra conexión
        /// </summary>
        /// <returns> True= si existe conexión, false en caso contrario</returns>
        public Exception CompruebaConexion(bool KeepConection = true)
        {
            Exception error = null;
            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }
            if (conexion.State == System.Data.ConnectionState.Closed)
            try
            {
                conexion.Open();
            }
            catch (Exception e)
            {
                error = e;
            }
            finally
            {
                if (!KeepConection)
                    conexion.Close();
                //conexion.Dispose();    
            }
            return error;
        }

        public Object EjecutarSelectScalar(string strQuery)
        {
            SQLiteCommand oSQLiteCommand = null; //Para dar orde SQL al DataAdapter
            Object oObjAux = null;

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            try
            {
                conexion.Open();
                oSQLiteCommand = new SQLiteCommand(strQuery, conexion);
                oObjAux = oSQLiteCommand.ExecuteScalar();
                conexion.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Dispose();
                if (oSQLiteCommand != null)
                {
                    oSQLiteCommand.Dispose();
                    oSQLiteCommand = null;
                }
            }

            return oObjAux;
        }

        public DataSet EjecutarSelect(string strQuery)
        {

            SQLiteDataAdapter oSQLiteDataAdapter = null; //Para operar con la BD utilizano SQL
            DataSet oDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            int numRegistros = -1;
            bool cerrar = false;

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                    cerrar = true;
                }
                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                oDataSet = new DataSet();
                oSQLiteDataAdapter = new SQLiteDataAdapter(strQuery, conexion);
                numRegistros = oSQLiteDataAdapter.Fill(oDataSet);//se carga de datos de la BD el DataSet

            }
            catch (Exception e)
            {
                string aux = e.Message;
            }
            finally
            {
                if (cerrar)
                    conexion.Close();
                if (oSQLiteDataAdapter != null)
                {
                    oSQLiteDataAdapter.Dispose();
                    oSQLiteDataAdapter = null;
                }
                //conexion.Dispose();
            }
            if (numRegistros > 0)
                return oDataSet;
            else
                return null;
        }


        public DataSet EjecutarReader(string strQuery)
        {

            //SQLiteDataAdapter oSQLiteDataAdapter = null; //Para operar con la BD utilizano SQL
            //SQLiteCommand oCommand = null;
            //FbDataReader oReader = null;
            //DataSet oDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            //DataTable oTable = null;
            //DataTable oTableEsquema = null;

            //int numRegistros = -1;

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            //try
            //{
            //    conexion.Open();
            //    oCommand = new SQLiteCommand(strQuery , conexion);
            //    oCommand.CommandType = CommandType.Text;//SQL
            //    oReader= oCommand.ExecuteReader();

            //    oTableEsquema = oReader.GetSchemaTable();


            //    foreach (DataRow oRow in oTableEsquema.Rows)
            //    {
            //        if (oTable == null)
            //        {
            //            oTable = new DataTable();
            //        }
            //        oTable.Columns.Add(oRow["columnName"].ToString());

            //        //foreach (DataColumn column in oTableEsquema.Columns)
            //        //{

            //        //    oTable.Columns.Add(column.ColumnName, column.DataType);
            //        //   // Console.WriteLine(String.Format("{0} = {1}",        column.ColumnName, oRow[column]));
            //        //}
            //    }

            //    while (oReader.HasRows)
            //    {
            //        while (oReader.Read())
            //        {
            //            DataRow oRow = oTable.NewRow();
            //            for(int i=0; i< oTableEsquema.Columns.Count; i++)
            //            //foreach (DataColumn column in oTableEsquema.Columns)
            //            {
            //                oRow[i] = oReader.GetValue(i);
            //            }
            //            oTable.Rows.Add(oRow);
            //        }
            //    }

            //    oDataSet = new DataSet();
            //    oDataSet.Tables.Add(oTable);
            //    oReader.Close();

            //    conexion.Close();
            //}
            //catch (Exception e)
            //{
            //    string aux = e.Message;
            //}
            //finally
            //{
            //    conexion.Dispose();
            //}
            //return oDataSet;
            return null;
        }

        void SQLiteDataAdapter_FillError(object sender, FillErrorEventArgs e)
        {
            string strError = "";

            strError = e.Errors.Message;


        }



        /// <summary>
        /// Ejecuta el procedimiento que se pasa como parámetro
        /// </summary>
        /// <param name="strNombreProc">Nombre del procedimiento a ejecutar</param>
        /// <returns>DataSet con los datos devueltos por el procedimiento. Por defecto null</returns>
        public System.Data.DataSet EjecutarProcedimiento(string strNombreProc)
        {
            SQLiteCommand objSQLiteCommand; //Para dar orde SQL al DataAdapter
            SQLiteDataAdapter objSQLiteDataAdapter = null; //Para operar con la BD utilizano SQL
            System.Data.DataSet objDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }
            objSQLiteCommand = new SQLiteCommand();

            try
            {
                objSQLiteCommand.Connection = conexion;
                objSQLiteCommand.CommandText = strNombreProc;
                objSQLiteCommand.CommandType = System.Data.CommandType.StoredProcedure;

                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                objSQLiteDataAdapter = new SQLiteDataAdapter();
                objDataSet = new System.Data.DataSet();

                objSQLiteDataAdapter.SelectCommand = objSQLiteCommand;//ejecuta el Command sobre la BD                                
                objSQLiteDataAdapter.Fill(objDataSet);//se carga de datos de la BD el DataSet
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
            finally
            {
                //Se cierran las conexiones
                conexion.Dispose();
                objSQLiteCommand.Dispose();
                objSQLiteDataAdapter.Dispose();
            }
            return objDataSet;
        }

        public System.Data.DataSet EjecutarProcedimiento(string strNombreProc, string[] parametros)
        {
            SQLiteCommand objSQLiteCommand; //Para dar orde SQL al DataAdapter
            SQLiteDataAdapter objSQLiteDataAdapter = null; //Para operar con la BD utilizano SQL
            System.Data.DataSet objDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            // SQLiteParameter objParam;

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }
            objSQLiteCommand = new SQLiteCommand();

            try
            {
                objSQLiteCommand.Connection = conexion;
                objSQLiteCommand.CommandText = strNombreProc;
                objSQLiteCommand.CommandType = CommandType.StoredProcedure;

                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                objSQLiteDataAdapter = new SQLiteDataAdapter();
                objDataSet = new DataSet();

                objSQLiteDataAdapter.SelectCommand = objSQLiteCommand;//ejecuta el Command sobre la BD                                
                objSQLiteDataAdapter.Fill(objDataSet);//se carga de datos de la BD el DataSet
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
            finally
            {
                //Se cierran las conexiones
                conexion.Dispose();
                objSQLiteCommand.Dispose();
                objSQLiteDataAdapter.Dispose();
            }
            return objDataSet;
        }

        /// <summary>
        /// Inserta en la base de datos según la query pasada como parámetro
        /// </summary>
        /// <param name="strQuery">Query de insercción</param>
        /// <param name="lstParamValor">Lista de campos-valor, cada item representa un registro a insertar</param>
        /// <returns>Diccionario con clave los id insertados en la BD y valor el objeto insertado. Si lstParam es nulo el value es null</returns>
        public Dictionary<int, Object> GrabarEnBD(string strQuery, List<string> lstParamValor, List<object> lstObj)
        {
            SQLiteCommand oSQLiteCommand; //Para dar orde SQL al DataAdapter
            Dictionary<int, object> dicDatos = null;
            string strQueryAux, strCampoValor;
            int idInsertado;
            Object oObjAux;

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            try
            {
                conexion.Open();

                if (lstParamValor != null)
                {
                    //foreach (string strCampoValor in lstParamValor)
                    for (int i = 0; i < lstParamValor.Count; i++)
                    {
                        strQueryAux = "";
                        strCampoValor = lstParamValor[i];
                        strQueryAux = strQuery + " " + strCampoValor;

                        oSQLiteCommand = new SQLiteCommand(strQueryAux, conexion);

                        oObjAux = oSQLiteCommand.ExecuteScalar();
                        idInsertado = (int)oObjAux;

                        if (dicDatos == null)
                        {
                            dicDatos = new Dictionary<int, object>();
                        }
                        dicDatos.Add(idInsertado, lstObj[i]);
                    }
                }
                else
                {
                    oSQLiteCommand = new SQLiteCommand(strQuery, conexion);
                    oObjAux = oSQLiteCommand.ExecuteScalar();
                    idInsertado = (int)oObjAux;

                    if (dicDatos == null)
                    {
                        dicDatos = new Dictionary<int, object>();
                    }
                    dicDatos.Add(idInsertado, null);
                }

                conexion.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Dispose();
            }

            return dicDatos;
        }



        /// <summary>
        /// Graba los datos en la base de datos devolviendo el número de registros insertados
        /// </summary>
        /// <param name="strQuery">Query</param>
        /// <param name="parametros">matriz de parametros de SQLite</param>
        /// <param name="oTable">Tabla con tantas columnas como parametros</param>
        /// <returns></returns>
        public int GrabarEnBD(string strQuery, SQLiteParameter[] parametros, System.Data.DataTable oTable)
        {
            SQLiteCommand oSQLiteCommand; //Para dar orde SQL al DataAdapter
            SQLiteDataAdapter oSQLiteDataAdapter;

            int numRegistros = 0;

            if (conexion == null)
            {
                conexion = new SQLiteConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            if (parametros.Length == oTable.Columns.Count)
            {
                try
                {
                    conexion.Open();
                    oSQLiteCommand = new SQLiteCommand(strQuery, conexion);

                    for (int i = 0; i < parametros.Length; i++)
                    {
                        oSQLiteCommand.Parameters.Add(parametros[i]);
                    }

                    oSQLiteDataAdapter = new SQLiteDataAdapter();//(strQuery, conexion);

                    for (int i = 0; i < oTable.Rows.Count; i++)
                    {
                        System.Data.DataRow oRow = oTable.Rows[i];
                        for (int j = 0; j < oSQLiteCommand.Parameters.Count; j++)
                        {
                            oSQLiteCommand.Parameters[j].Value = oRow[j];
                        }

                        numRegistros = numRegistros + oSQLiteCommand.ExecuteNonQuery();

                    }

                    conexion.Close();
                }
                catch (Exception e)
                {
                    string error = e.Message;
                    //System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.Dispose();
                }
            }

            return numRegistros;
        }
        #endregion

        #region Métodos de Acceso mantenimiento

        public void GetSchema(SQLiteConnectionStringBuilder cs)
        {

            SQLiteConnection connection = new SQLiteConnection(cs.ToString());
            connection.Open();

            // Get the available metadata Collection names
            DataTable metadataCollections = connection.GetSchema();

            // Get datatype information
            DataTable dataTypes = connection.GetSchema(DbMetaDataCollectionNames.DataTypes);

            // Get DataSource Information
            DataTable dataSourceInformation = connection.GetSchema(DbMetaDataCollectionNames.DataSourceInformation);

            // Get available reserved word
            DataTable reservedWords = connection.GetSchema(DbMetaDataCollectionNames.ReservedWords);

            // Get the list of User Tables
            // Restrictions:
            // TABLE_CATALOG
            // TABLE_SCHEMA
            // TABLE_NAME
            // TABLE_TYPE
            DataTable userTables = connection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });

            // Get the list of System Tables
            // Restrictions:
            // TABLE_CATALOG
            // TABLE_SCHEMA
            // TABLE_NAME
            // TABLE_TYPE
            DataTable systemTables = connection.GetSchema("Tables", new string[] { null, null, null, "SYSTEM TABLE" });

            // Get Table Columns
            // Restrictions:
            // TABLE_CATALOG
            // TABLE_SCHEMA
            // TABLE_NAME
            // COLUMN_NAME
            DataTable tableColumns = connection.GetSchema("Columns", new string[] { null, null, "TableName" });

            connection.Close();

        }
        #endregion

    }
}
