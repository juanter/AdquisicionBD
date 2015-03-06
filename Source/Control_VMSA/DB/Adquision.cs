using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;
using System.ComponentModel;
using System.Windows.Forms;
using Util;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.DB
{
    // Declare a delegate.
    // Metodo anonimo
    delegate void GetListaAdquisicion(string s);

    //private class State

    //{

    //    public EventWaitHandle eventWaitHandle = new ManualResetEvent(false);

    //    public int result;

    //    public object parameters[];

    //}

//    private static void PerformUserWorkItem( Object stateObject )

//    {

//        State state = stateObject as State;

//        if(state != null)

//        {

//            // do something lengthy with state.parameters...

//            state.result = 42;

//            state.eventWaitHandle.Set(); // signal we're done

//        }

//    }

//    public static object DoWorkSynchronous()

//    {

//        State state = new State();

//        ThreadPool.QueueUserWorkItem(PerformUserWorkItem, state);

//        state.eventWaitHandle.WaitOne();

//        Console.WriteLine(state.result);

//        return state.result;

//}
    public class Medida
    {
        public DateTime fecha;
        public Double velocidad;
        public Double consumo;
    }

    public class Adquision
    {
        //BlockingCollection<Medida> bc = new BlockingCollection<Medida>();

        public bool Terminado;
        private DBSQLite _oBD;
        const string sqlCreateMedidas =
            "create table if not exists MEDIDAS(ID INTEGER PRIMARY KEY, FECHA DATETIME NOT NULL, VELOCIDAD DOUBLE PRECISION NOT NULL DEFAULT 0," +
            " CONSUMO DOUBLE PRECISION NOT NULL DEFAULT 0);CREATE INDEX IF NOT EXISTS IDX_MEDIDAS ON MEDIDAS (FECHA)";
            
        const string sqlInsertMedida = "insert into MEDIDAS (FECHA, VELOCIDAD, CONSUMO) values (@FECHA, @VELOCIDAD, @CONSUMO)";
          // private DataSet _dsVariables;
        
        public int tpoEspera = 300; // 5 mseg
        public string conexionstring;
        public DateTime fecha_adqusicion;
        public bool enEspera = false;
        public int error;
        public string emensaje;
        public List<Medida> medidas;
        public bool activo = false;
        private AutoResetEvent _autoEvent;


        #region Constructores

        /////*********   CONSTRUCTORES  *****************
        public Adquision(AutoResetEvent autoEvent)
        {
            _oBD = new DBSQLite();
            _autoEvent = autoEvent;
            // Conexion = new FbConnection();
            medidas = new List<Medida>();

            // _dsVariables = new DataSet("VARIABLES");
        }

        ~Adquision()
        {
            if (_oBD != null)
            {
                _oBD = null;
            }

            // if (_dsVariables != null)
            // {
            //    _dsVariables.Dispose();
            //    _dsVariables = null;
            // }

            if (medidas != null)
            {
                medidas.Clear();
                medidas = null;
            }
        }
        #endregion

        public bool Iniciar()
        {
            bool resultado = false;
            try
            {
                Terminado = false;
                Log.InsertaLogApp("Abriendo BaseDatos: " + _oBD.conexionString.ToString(), true);
                Exception error = _oBD.CompruebaConexion(true);
                if (error == null)
                {
                    SQLiteCommand command = new SQLiteCommand(sqlCreateMedidas, _oBD.conexion);
                    command.ExecuteNonQuery();
                    resultado = true;
                    Log.InsertaLogApp("Adquisicion Iniciada", true);
                }
                else
                {
                    string aux = error.Message;
                    Log.InsertaLogApp("Excepcion Iniciar Adquisicion: " + aux, true);
                    Log.InsertaLogErrores("Excepcion Iniciar Adquisicion: " + aux);
                    System.Windows.Forms.MessageBox.Show(aux, "Error Iniciando Base de Datos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                
                }
                
            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                Log.InsertaLogApp("Excepcion Iniciar Adquisicion: " + aux, true);
                Log.InsertaLogErrores("Excepcion Iniciar Adquisicion: " + aux);
                System.Windows.Forms.MessageBox.Show(aux, "Error Iniciando Base de Datos", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return resultado;
        }

        public void ControlAdquisionWait(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                List<Medida> amedidas = null;
                //worker.ReportProgress(-1, "Inicialización de valores");
                bool resultado = Iniciar();
                if (resultado)
                    while ((!Terminado) && (!worker.CancellationPending))
                    {
                        amedidas = ComprobarMedidas();
                        if (amedidas != null)
                        {
                            InsertMedidas(amedidas);
                            worker.ReportProgress(1, amedidas);
                        }
                        // Evento de sincronización, espera a que ponga en signed y automaticamente al ser tratado se marca como unsigned
                        _autoEvent.WaitOne();
                        // Si no fuera auto con metodo reset lo pondriamos en unsigned
                        // _autoEvent.Reset();
                        // System.Threading.Thread.Sleep(1000);
                    }

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    Log.InsertaLogApp("Cancelado Trabajo Adquisicion", true);
                }
            }
            catch (Exception ex)
            {
                string aux = ex.Message;
                Log.InsertaLogErrores("Excepcion ControlAdquisionWait: " + aux);
                MessageBox.Show("Se ha producido una excepcion que cerrará el servidor:\n " + aux,
                    "Excepción Servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
               //  _oBD.conexion.Close();
            }
        }

        public int InsertMedidas(List<Medida> lmedidas)
        {
            int numRegistros = 0;
            Exception e = _oBD.CompruebaConexion();
            if (e == null)
            {
                        SQLiteTransaction MTransaccion = _oBD.conexion.BeginTransaction();
                        SQLiteCommand oCommand = new SQLiteCommand(sqlInsertMedida, _oBD.conexion, MTransaccion);
                        oCommand.CommandType = CommandType.Text;
                        oCommand.Parameters.Add("@FECHA", DbType.DateTime);
                        oCommand.Parameters.Add("@VELOCIDAD", DbType.Double);
                        oCommand.Parameters.Add("@CONSUMO", DbType.Double);
                        try
                        {
                            for (int i = 0; i < lmedidas.Count; i++)
                            {
                                oCommand.Parameters["@FECHA"].Value = lmedidas[i].fecha;
                                oCommand.Parameters["@VELOCIDAD"].Value = lmedidas[i].velocidad;
                                oCommand.Parameters["@CONSUMO"].Value = lmedidas[i].consumo;
                                numRegistros = numRegistros + oCommand.ExecuteNonQuery();
                            }
                            MTransaccion.Commit();
                        }
                        catch (Exception es)
                        {
                            // system.Windows.Forms.MessageBox.Show(e.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            string aux = es.Message;
                            // System.TimeSpan diffResult = DateTime.Now.Subtract(fechahoraini);
                            Log.InsertaLogErrores("Excepcion InsertMedidas: " + aux);
                            MTransaccion.Rollback();
                            MTransaccion = null;

                        }

            }
            else
            {
                Log.InsertaLogErrores("Excepcion Conexion Basedatos: " + e.Message);
            }
            return numRegistros;
        }

        public void AddMedida(Medida aMedida)
        {
            // lock(medidas)
            // {}
            try
            {
                System.Threading.Monitor.Enter(medidas);
                medidas.Add(aMedida);
            }
            finally
            {
                System.Threading.Monitor.Exit(medidas);
            }
            // Evento de sincronización, Marca como Signed, y automaticamente al ser tratado en el thread se marca como unsigned
            _autoEvent.Set();
        }

        public List<Medida> ComprobarMedidas()
        {
            List<Medida> amedidas = null; 
            try
            {
                Monitor.Enter(medidas);
                if (medidas.Count > 0)
                {
                    amedidas = new List<Medida>();
                    for (int i = 0; i < medidas.Count; i++)
                       amedidas.Add(medidas[i]);
                    medidas.Clear();
                }
            }
            finally
            {
                Monitor.Exit(medidas);
            }
            return amedidas;
        }
    }
}


// COPIAR cONTENIDO
// DataSet copyDataSet = customerDataSet.Copy();
//// Copy all changes.
//DataSet changeDataSet = customerDataSet.GetChanges();
//// Copy only new rows.
//DataSet addedDataSet= customerDataSet.GetChanges(DataRowState.Added);

////Para crear una copia de un DataSet que sólo incluya el esquema, utilice el métodoClone del DataSet. 
////También es posible agregar filas existentes al DataSet clonado mediante el método ImportRow del DataTable. 
////ImportRow agrega datos, el estado de fila e información de versión de fila a la tabla especificada. 
////Los valores de columna sólo se agregan cuando los nombres de columna coinciden y el tipo de datos es compatible.
////En el siguiente ejemplo de código se crea un clon de un DataSet y se agregan la filas del DataSet original a la tabla Customers del DataSet clonado 
////para aquellos clientes cuya columna CountryRegion tenga el valor "Germany".
//DataSet germanyCustomers = customerDataSet.Clone();
//DataRow[] copyRows = customerDataSet.Tables["Customers"].Select("CountryRegion = 'Germany'");
//DataTable customerTable = germanyCustomers.Tables["Customers"];
//foreach (DataRow copyRow in copyRows)
//  customerTable.ImportRow(copyRow);

// MAESTRO DETALLE
//string sql = "Select * from oces;Select * from Productor";
// SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cnnsimose"].ConnectionString);
// SqlDataAdapter da = new SqlDataAdapter(sql,cnn);
// da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
// da.TableMappings.Add("Table", "oces");
// da.TableMappings.Add("Table1", "Productores");
// DataSet ds = new DataSet();
// cnn.Open();
// da.Fill(ds);
// DataRelation relation = new DataRelation("oces_productores",ds.Tables["oces"].Columns["codoce"],ds.Tables["productores"].Columns["codoce"]);
// ds.Relations.Add(relation);
// foreach (DataRow row in ds.Tables[0].Rows)
// {
//   Response.Write("PADRE:" + row.Field<string>("siglas") + "<br/>" );
//     foreach (DataRow rowchild in row.GetChildRows(relation))
//     {
//         Response.Write("HIJO:" + rowchild.Field<string>("nombre")+"<br/>");
//     }
// }
//Aca muestro un ejemplo de como establecer relaciones entre datatable, luego utilizando expresiones como Parent y child, podemos mostrar datos de otra tabla o bien campos calculados que tengan relacion con la tabla madre. Para el ejemplo de aca OCES es la tabla madre y productor la tabla hija, la relacion es de uno a muchos.
//string sqlConnectString = WebConfigurationManager.ConnectionStrings["cnnsimose"].ConnectionString;
//        string sqlSelect = @"SELECT codoce,siglas FROM OCES;
//                SELECT CodOCE,Nombre,codproductor FROM Productor;";
//        DataSet ds = new DataSet();
//        // llenamos el dataset
//        SqlDataAdapter da = new SqlDataAdapter(sqlSelect, sqlConnectString);
//        da.TableMappings.Add("Table", "OCES");
//        da.TableMappings.Add("Table1", "Productor");
//        da.Fill(ds);
//        // crear la relacion
//        DataRelation dr = new DataRelation("MyRelation",ds.Tables["OCES"].Columns["CodOCE"],ds.Tables["Productor"].Columns  ["CodOCE"]);
//        ds.Relations.Add(dr);
//        //agrega a la tabla madre un campo calculado en base a los datos de la tabla hija
//        ds.Tables["OCES"].Columns.Add("Numero Productores", typeof (int), "COUNT(Child.CodProductor)");
//        this.GridView2.DataSource = ds.Tables["OCES"];
//        this.GridView2.DataBind();
//        //agrega a la tabla hija un campo de la tabla madre
//        ds.Tables["Productor"].Columns.Add("Organismo", typeof(string),"Parent(MyRelation).Siglas");
//        this.GridView1.DataSource = ds.Tables["Productor"];
//        this.GridView1.DataBind();


// MERGE DATASET
//SqlConnection cnn = new SqlConnection(WebConfigurationManager.ConnectionStrings["cnndb"].ConnectionString);
//   SqlDataAdapter adp1 = new SqlDataAdapter("select codoce,siglas from oces where codoce BETWEEN 1 AND 10",cnn);
//   SqlDataAdapter adp2 = new SqlDataAdapter("select codoce,siglas from oces where codoce BETWEEN 11 AND 20",cnn);
//   DataSet ds1 = new DataSet();
//   adp1.TableMappings.Add("Table", "OCES");
//   adp1.FillSchema(ds1, SchemaType.Mapped);
//   adp1.Fill(ds1);
//   DataSet ds2 = new DataSet();
//   adp2.TableMappings.Add("Table", "OCES");
//   adp2.FillSchema(ds2, SchemaType.Mapped);
//   adp2.Fill(ds2);
//   DataSet dsmerge = new DataSet("MergeDataSet");
//   dsmerge.Merge(ds1);
//   dsmerge.Merge(ds2);


//El ejemplo mostrara como podemos insertar un conjunto de datos que tiene una estructura en este caso una tabla, lo primero que haremos será crear las tablas y definir un tipo de dato estructurado, para el ejemplo se utilizo sql server 2008 y visual studio 2008, aunque debería de funcionar en versiones inferiores.
//Creamos la tabla
//CREATE TABLE CLIENTE(
//        Id int NOT NULL PRIMARY KEY,
//        Field1 nvarchar(50) NULL,
//        Field2 nvarchar(50) NULL )
//Luego creamos tipo de datos
//CREATE TYPE TipoCliente AS TABLE ( Id int, Field1 nvarchar(50), Field2 nvarchar(50))
//Ahora vamos a crear el procedimiento almacenado, encargado de insertar datos en la tabla, el parametro que recibe es de tipo Cliente.
//CREATE PROCEDURE InsertCliente (
//        @pcliente TipoCliente READONLY)
//    AS
//        SET NOCOUNT ON

//        INSERT INTO CLIENTE
//        SELECT Id, Field1, Field2 FROM @pcliente

//Veamos ahora como invocariamos el procedimiento almacenado en ADO.NET
//DataTable Cliente = new DataTable( );
//Cliente.Columns.Add("Id", typeof(int)); 
//Cliente.Columns.Add("Field1", typeof(string)).MaxLength = 50; 
//Cliente.Columns.Add("Field2", typeof(string)).MaxLength = 50; 
////agregamos datos a la tabla
//Cliente.Rows.Add(new object[] { 1, "Mi primer cliente", "buen cliente" }); 
//Cliente.Rows.Add(new object[] { 2, "Mi segundo cliente", "regular cliente" }); 
//Cliente.Rows.Add(new object[] { 3, "Mi tercer cliente", "mal cliente" });
////La conexcion en esta caso es directa pero se pudo haber leido de un archivo de configuracion
//SqlConnection connection = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=MYDB;Integrated Security=true;"); 
//SqlCommand command = new SqlCommand("InsertCliente", connection); 
//command.CommandType = CommandType.StoredProcedure; 
//SqlParameter param = command.Parameters.AddWithValue("@pcliente", Cliente);
//param.SqlDbType = SqlDbType.Structured; 
//connection.Open( ); 
//command.ExecuteNonQuery( ); 
//connection.Close( );