using System;
using System.Collections.Generic;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;
using FirebirdSql.Data.Isql;
using System.Data.Common;
using System.Configuration;
using System.Data;//Para poder utilizar DataTables
using System.IO;
using Util;

namespace Aplicacion.DB
{
    public class BDFirebird
    {
        public FbConnection conexion;
        public FbConnectionStringBuilder conexionString;

        #region Constructores
        public BDFirebird()
        {
            CargaDatosConexionBD();
            conexion = new FbConnection();
            conexion.ConnectionString = this.conexionString.ConnectionString;           

        }

        ~BDFirebird()
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
            this.conexionString = new FbConnectionStringBuilder();
            System.Configuration.Configuration config;
            config = Config.GetConfiguracion();
            this.conexionString.DataSource = Config.leerSetting("Fb_DataSource", "LOCALHOST", config);
            //this.conexionString.Port = 3050;
            //conexionString.Database = "C:\\Documents and Settings\\xabi\\Escritorio\\PruebasBorrame\\Borrame\\BD\\BD.fdb";
            this.conexionString.Database = Config.leerSetting("Fb_Database", Environment.CurrentDirectory + @"\2904.fbb", config);
            this.conexionString.UserID = Config.leerSetting("Fb_User", "SYSDBA", config); ;
            this.conexionString.Password = Config.leerSetting("Fb_Pass", "masterkey", config); ;
            this.conexionString.Charset = "NONE";
            this.conexionString.Pooling = false;
        }

        private void RegistrarEventos()
        {
            FbRemoteEvent revent = new FbRemoteEvent(conexion);
            revent.AddEvents(new string[] { "ALARMA_ACTIVA" });

            // Add callback to the Firebird events
            revent.RemoteEventCounts += new FbRemoteEventEventHandler(EventCounts);

            // Queue events
            revent.QueueEvents();
        }

        // Evento Firebird
        static void EventCounts(object sender, FbRemoteEventEventArgs args)
        {
            //    Console.WriteLine("Event {0} has {1} counts.", args.Name, args.Counts);
        }


        #region Métodos de Acceso a datos de FireBird
        
        /// <summary>
        /// Comprueba el estado de conexión a la base de datos. Abre y cierra conexión
        /// </summary>
        /// <returns> True= si existe conexión, false en caso contrario</returns>
        public Exception CompruebaConexion(bool KeepConection = true)
        {
            Exception error = null;
            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }
            if (conexion.State == ConnectionState.Closed)
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
            FbCommand oFbCommand=null; //Para dar orde SQL al DataAdapter
            Object oObjAux = null;

            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            try
            {
                conexion.Open();
                oFbCommand = new FbCommand(strQuery, conexion);
                oObjAux = oFbCommand.ExecuteScalar();
                conexion.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Dispose();
                if (oFbCommand != null)
                {
                    oFbCommand.Dispose();
                    oFbCommand = null;
                }
            }

            return oObjAux;
        }

        public DataSet EjecutarSelect(string strQuery)
        {

            FbDataAdapter oFbDataAdapter = null; //Para operar con la BD utilizano SQL
            DataSet oDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            int numRegistros = -1;
            bool cerrar = false;

            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                    cerrar = true;
                }
                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                oDataSet = new DataSet();
                oFbDataAdapter = new FbDataAdapter(strQuery, conexion);
                numRegistros = oFbDataAdapter.Fill(oDataSet);//se carga de datos de la BD el DataSet

            }
            catch (Exception e)
            {
                string aux = e.Message;
            }
            finally
            {
                if (cerrar)
                    conexion.Close();
                if (oFbDataAdapter != null)
                {
                    oFbDataAdapter.Dispose();
                    oFbDataAdapter = null;
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

            //FbDataAdapter oFbDataAdapter = null; //Para operar con la BD utilizano SQL
            //FbCommand oCommand = null;
            //FbDataReader oReader = null;
            //DataSet oDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            //DataTable oTable = null;
            //DataTable oTableEsquema = null;

            //int numRegistros = -1;

            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

            //try
            //{
            //    conexion.Open();
            //    oCommand = new FbCommand(strQuery , conexion);
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

        void FbDataAdapter_FillError(object sender, FillErrorEventArgs e)
        {
            string strError = "";

            strError = e.Errors.Message;


        }



        /// <summary>
        /// Ejecuta el procedimiento que se pasa como parámetro
        /// </summary>
        /// <param name="strNombreProc">Nombre del procedimiento a ejecutar</param>
        /// <returns>DataSet con los datos devueltos por el procedimiento. Por defecto null</returns>
        public DataSet EjecutarProcedimiento(string strNombreProc)
        {
            FbCommand objFbCommand; //Para dar orde SQL al DataAdapter
            FbDataAdapter objFbDataAdapter = null; //Para operar con la BD utilizano SQL
            DataSet objDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter

            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }
            objFbCommand = new FbCommand();

            try
            {
                objFbCommand.Connection = conexion;
                objFbCommand.CommandText = strNombreProc;
                objFbCommand.CommandType = CommandType.StoredProcedure;

                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                objFbDataAdapter = new FbDataAdapter();
                objDataSet = new DataSet();

                objFbDataAdapter.SelectCommand = objFbCommand;//ejecuta el Command sobre la BD                                
                objFbDataAdapter.Fill(objDataSet);//se carga de datos de la BD el DataSet
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
            finally
            {
                //Se cierran las conexiones
                conexion.Dispose();
                objFbCommand.Dispose();
                objFbDataAdapter.Dispose();
            }
            return objDataSet;
        }

        public DataSet EjecutarProcedimiento(string strNombreProc, string[] parametros)
        {
            FbCommand objFbCommand; //Para dar orde SQL al DataAdapter
            FbDataAdapter objFbDataAdapter = null; //Para operar con la BD utilizano SQL
            DataSet objDataSet = null;// Objeto que recupera los datos consultados en el DataAdapter
            // FbParameter objParam;

            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }
            objFbCommand = new FbCommand();

            try
            {
                objFbCommand.Connection = conexion;
                objFbCommand.CommandText = strNombreProc;
                objFbCommand.CommandType = CommandType.StoredProcedure;

                //Ejecuta procedimiento y rellena datos devueltos en un dataset
                objFbDataAdapter = new FbDataAdapter();
                objDataSet = new DataSet();

                objFbDataAdapter.SelectCommand = objFbCommand;//ejecuta el Command sobre la BD                                
                objFbDataAdapter.Fill(objDataSet);//se carga de datos de la BD el DataSet
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
            finally
            {
                //Se cierran las conexiones
                conexion.Dispose();
                objFbCommand.Dispose();
                objFbDataAdapter.Dispose();
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
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            Dictionary<int, object> dicDatos = null;
            string strQueryAux, strCampoValor;
            int idInsertado;
            Object oObjAux;

            if (conexion == null)
            {
                conexion = new FbConnection();
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

                        oFbCommand = new FbCommand(strQueryAux, conexion);

                        oObjAux = oFbCommand.ExecuteScalar();
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
                    oFbCommand = new FbCommand(strQuery, conexion);
                    oObjAux = oFbCommand.ExecuteScalar();
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
        /// <param name="parametros">matriz de parametros de Firebird</param>
        /// <param name="oTable">Tabla con tantas columnas como parametros</param>
        /// <returns></returns>
        public int GrabarEnBD(string strQuery, FbParameter[] parametros, DataTable oTable)
        {
            FbCommand oFbCommand; //Para dar orde SQL al DataAdapter
            FbDataAdapter oFbDataAdapter;

            int numRegistros = 0;

            if (conexion == null)
            {
                conexion = new FbConnection();
                conexion.ConnectionString = this.conexionString.ConnectionString;
            }

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
        public void EjecutarScript(string filename)
        {
            FbConnectionStringBuilder csb = new FbConnectionStringBuilder();
            csb.ServerType = FbServerType.Default;
            csb.Database = "employee.fdb";
            csb.UserID = "SYSDBA";
            csb.Password = "masterkey";

            Console.WriteLine("Using: " + csb);

            if (File.Exists(csb.Database))
            {
                Console.WriteLine("Deleting the old database.");
                File.Delete(csb.Database);
            }
            Console.WriteLine("Creating new database.");
            Console.WriteLine(csb.ToString());
            FbConnection.CreateDatabase(csb.ToString());

            FbScript script = new FbScript(filename);
            script.Parse();

            FbConnection c = new FbConnection(csb.ToString());
            c.Open();

            FbBatchExecution fbe = new FbBatchExecution(c);
            foreach (string cmd in script.Results)
            {
                fbe.SqlStatements.Add(cmd);
            }

            Console.WriteLine("Executing.");

            DateTime start = DateTime.Now;
            fbe.Execute();
            Console.WriteLine("Took " + (DateTime.Now - start));
            c.Close();
        }

        public void DatabaseBackup()
        {
            FbBackup backupSvc = new FbBackup();

            backupSvc.ConnectionString = conexion.ConnectionString;
            backupSvc.BackupFiles.Add(new FbBackupFile(@"c:\testdb.gbk", 2048));
            backupSvc.Verbose = true;

            backupSvc.Options = FbBackupFlags.IgnoreLimbo;

            backupSvc.ServiceOutput += new ServiceOutputEventHandler(ServiceOutput);

            backupSvc.Execute();
        }

        public void DatabaseRestore()
        {
            FbRestore restoreSvc = new FbRestore();

            restoreSvc.ConnectionString = conexion.ConnectionString;
            restoreSvc.BackupFiles.Add(new FbBackupFile(@"c:\testdb.gbk", 2048));
            restoreSvc.Verbose = true;
            restoreSvc.PageSize = 4096;
            restoreSvc.Options = FbRestoreFlags.Create | FbRestoreFlags.Replace;

            restoreSvc.ServiceOutput += new ServiceOutputEventHandler(ServiceOutput);

            restoreSvc.Execute();
        }

        static void ServiceOutput(object sender, ServiceOutputEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        public void GetSchema()
        {

            FbConnectionStringBuilder cs = new FbConnectionStringBuilder();

            cs.DataSource = "localhost";
            cs.Database = "employee.fdb";
            cs.UserID = "SYSDBA";
            cs.Password = "masterkey";
            cs.Charset = "NONE";
            cs.Pooling = false;

            FbConnection connection = new FbConnection(cs.ToString());
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
