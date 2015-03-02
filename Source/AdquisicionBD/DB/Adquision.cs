using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.SQLite;
using System.ComponentModel;
using System.Windows.Forms;
using Util;

namespace Aplicacion.DB
{
    public class Medida
    {
        public DateTime Fecha;
        public float velocidad;
        public float potencia;
    }

    public class Adquision
    {
        public bool Terminado;
        private DBSQLite _oBD;
        private DataSet _dsVariables;
        
        public int tpoEspera = 300; // 5 mseg
        public string conexionstring;
        public DateTime fecha_adqusicion;
        public bool enEspera = false;
        public int error;
        public string emensaje;
        public List<Medida> medidas;
        public bool activo = false;


        #region Constructores

        /////*********   CONSTRUCTORES  *****************
        public Adquision()
        {
            _oBD = new DBSQLite();
            // Conexion = new FbConnection();
            medidas = new List<Medida>();

            _dsVariables = new DataSet("VARIABLES");
        }

        ~Adquision()
        {
            if (_oBD != null)
            {
                _oBD = null;
            }

            if (_dsVariables != null)
            {
                _dsVariables.Dispose();
                _dsVariables = null;
            }

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
                _oBD.conexion.Open();
                resultado = true;
                Log.InsertaLogApp("Adquisicion Iniciada", true);
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
                worker.ReportProgress(-1, "Inicialización de valores");
                bool resultado = Iniciar();
                if (resultado)
                    while ((!Terminado) && (!worker.CancellationPending))
                    {
                        ControlAdquision(worker);
                        System.Threading.Thread.Sleep(1000);
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
                _oBD.conexion.Close();
            }
        }

        public void ControlAdquision(BackgroundWorker worker)
        {
            Exception e = _oBD.CompruebaConexion();
            if (e == null)
            {
                if ((!Terminado) && (this.activo))
                {
                    if (this.tpoEspera > 0)
                    {
                        worker.ReportProgress(-1, "Inicio importación datos");

                    }

                }
            }
            else
            {
                Log.InsertaLogErrores("Excepcion Conexion Basedatos: " + e.Message);
            }

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