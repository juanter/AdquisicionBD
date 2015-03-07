using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using Util;
using Aplicacion;
using Aplicacion.DB;
using System.Windows.Forms.DataVisualization.Charting;


namespace Control_VMSA
{
    public partial class Control_VMSA : Form
    {
        #region Globales
        public SerialPort puerto;
        int numDatos, indReception;
        int consumo;
        long velocidad;
        float datoorientacion;
        long datolatitud, datolongitud;
        bool iniciadatrama;
        bool LogBrujula, LogGraficacion;
        byte[] mBufferRead;
        byte[] mBufferWrite;
        int[] muestreo;
        long[] muestreo_long;
        byte byteant;

        DateTime UltimoLogBrujula, UltimoLogGraficacion;
        int contFallosLogBrujula;

        Motores  motorDrone;

        // Anulados (no se usan)
        // bool recibiendotrama, recibidatrama;
        string srtReceived;

        // Variables protocolo serie maestro PC
        byte[] sendlogger;
        EstadoPeticion estadoactual;
        ComandoEnvio comandoactual;
        Envio UltimoEnvio; // último comando enviado en caso de fallo
        List<Envio> ListaEnvios;
        DateTime UltimoEnvioComando;
        int ContadorFallosSerial;

        // ----- Añadido Adquisicion -----
        private Adquision _adquisicion;
        static AutoResetEvent autoEvent;
        private long MemoriaAntesCerrar = 0;
        // la declaramos fuera de la función, para que mantenga su valor
        Boolean PrimeraVez = true;
        Boolean PuedeCerrar = false;
        // -------------------------------

        #endregion

        #region Inicialización objetos
        public Control_VMSA()
        {
            InitializeComponent();
            mBufferRead = new byte[255];
            mBufferWrite = new byte[255];
            muestreo = new int[50];
            muestreo_long = new long[50];
            indReception = 0;
            byteant = 0;
            velocidad = 0;
            motorDrone = new Motores();
            tB_PotenciaMotores.Text = motorDrone.motor.potencia.ToString();
            tb_BasculaMotores.Text = motorDrone.motor.bascula.ToString();

            // Maestro Serial PC
            // Varialbes globales logger
            sendlogger = new byte[2];
            ListaEnvios = new List<Envio>();
            UltimoEnvio = new Envio();
            estadoactual = EstadoPeticion.Ninguno;
            comandoactual = ComandoEnvio.Ninguno;

        }


        private void Control_VMSA_Load(object sender, EventArgs e)
        {
            
            cargarVelocidadesTransmision();
            listarPuertos();
            cboBaudios.SelectedIndex = 2;
            if (cboPuerto.Items.Count >= 1) cboPuerto.SelectedIndex = 0;
            try
            {
                puerto = new SerialPort(cboPuerto.Text, int.Parse(cboBaudios.Text));
            }
            catch (Exception ex)
            {
                MostrarTexto("Error asignación en puerto " + ex);
            }
            //puerto = new SerialPort("COM6", int.Parse(cboBaudios.Text));

            
            // ------- Añadido Adquisicion -------------- //
            Log.InsertaLogApp("-------- Iniciando Control_VMSA", true);
            
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Iniciando Servidor SAIH";
            notifyIcon1.ShowBalloonTip(20);

            // Evento de sincronización, lo iniciamos a unsigned 
            autoEvent = new AutoResetEvent(false);
            // backgroundWorker1
            // Aqui creamos el thread para actualice los datos en otro thread
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // Ejecutamos el Thread
            this.backgroundWorker1.RunWorkerAsync();
            // --------------------------------------

        }


        #endregion

        private void cargarVelocidadesTransmision()
        {
            cboBaudios.Items.Clear();
            cboBaudios.Items.Add(2400);
            cboBaudios.Items.Add(4800);
            cboBaudios.Items.Add(9600);
            cboBaudios.Items.Add(14400);
            cboBaudios.Items.Add(19200);
            cboBaudios.Items.Add(28800);
            cboBaudios.Items.Add(38400);
            cboBaudios.Items.Add(56000);
            cboBaudios.Items.Add(115200);
        }
        private void listarPuertos()
        {
            cboPuerto.Items.Clear();
            string[] puertos = SerialPort.GetPortNames();
            foreach (string puerto in puertos)
            {
                cboPuerto.Items.Add(puerto);
            }
        }

        private void mnuConectar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!puerto.IsOpen)
                {
                    //puerto.PortName = cboPuerto.SelectedItem.ToString();
                    //puerto.BaudRate = Convert.ToInt32(cboBaudios.SelectedItem.ToString());
                    puerto.PortName = "COM3";
                    puerto.BaudRate = 115200;

                    puerto.Open();
                    //lblEstado.Text = "Estado: Conectado en el puerto " + cboPuerto.Text;
                    lblEstado.Text = "Estado: Conectado en el puerto " + puerto.PortName + " a " + puerto.BaudRate.ToString();
                    puerto.DataReceived += new SerialDataReceivedEventHandler(puerto_DataReceived);
                    //habilitarBotones(false, true, false);
                }
            }
            catch (Exception ex)
            {
                sta.Text = ex.Message;
            }
        }

        
        private void puerto_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte bytenew;
            int tamanotrama;
            try
            {
                //srtReceived += puerto.ReadChar();
                //srtReceived = puerto.ReadExisting(); 
                //srtReceived = puerto.ReadLine();          // Este funcionaría si se enviasen caracteres. Al trocear un entero no entiende un carácter por encima de 169......
                //MostrarTexto(puerto.ReadExisting().Substring(1, puerto.ReadExisting().Length));
                //interpretatrama(srtReceived);
                //MostrarTexto("tamaño: " + srtReceived.Length + '\n');
                //this.Invoke(new EventHandler(actualizar)); //  Esto funciona para graficar con el comando de envío 'B', flag() en arduino
                while (puerto.BytesToRead > 0)
                {
                    bytenew = (byte)puerto.ReadByte();

                    if ((!iniciadatrama) & (bytenew == (byte)'K') & (byteant == (byte)'A'))     // Confirmáción de solicitud de atención desde 8051
                    {
                        EnviaComando(UltimoEnvio.buffer, UltimoEnvio.numdatos);
                        estadoactual = EstadoPeticion.EsperandoRespuesta;
                    }
                    else if ((!iniciadatrama) & (bytenew == 0x02))
                    {
                        indReception = 0;
                        iniciadatrama = true;
                    }
                    else if (iniciadatrama)
                    {                         
                        mBufferRead[indReception] = bytenew;
                        indReception++;
                        if (
                            (byteant == 13) & (bytenew == 10) & 
                            (((comandoactual == ComandoEnvio.Logger) & (indReception >= 6)) | 
                              ((comandoactual != ComandoEnvio.Logger) & (indReception >= Constants.LONG.TAMANOMINIMO))))  // Tamaño mínimo trama recibida;
                        {
                            tamanotrama = mBufferRead[3];//2
                            if (tamanotrama == indReception - 6)  //5
                            {
                                byte[] tramaaux = new byte[indReception];
                                for (int i = 0; i < indReception-2; i++) tramaaux[i] = mBufferRead[i];
                                //recibidatrama = true;
                                tramaOK(indReception, tramaaux);
                            }
                            else
                            { 
                                //error trama
                                MostrarTexto("Error trama\n");
                            }
                            indReception = 0;
                            bytenew = 0;
                            iniciadatrama = false;
                        }                                                                        
                    }
                    byteant = bytenew;
                }
                }
            catch (Exception ex)
            {
                MostrarTexto(ex.ToString());
            }
        }

        private void tramaOK(int numdatos, byte [] mBuffer)
        {
            int posLecturaBuffer = 4;
            
            if (((char)mBuffer[0] == 'L'))  // Recibiendo datos de logger
            {
                if ((mBuffer[1] & 0x01) != 0)     // Dado de graficación 
                {
                    Medida amedida = new Medida();
                    amedida.fecha = DateTime.Now;
                    amedida.velocidad = (mBuffer[5] << 8) | mBuffer[4];
                    //velocidad = (long)((mBuffer[7] << 24) | (mBuffer[6] << 16) | (mBuffer[5] << 8) | mBuffer[4]);

                    amedida.consumo = ((int)mBuffer[7] << 8) | (int)mBuffer[6];
                    MostrarTexto("velocidad: " + velocidad.ToString() + ", consumo: " + consumo.ToString() + '\n');
                    //UltimoLogGraficacion = DateTime.Now;
                    this.Invoke(new EventHandler(interpretaAR), amedida);
                    posLecturaBuffer = 8;
                }
                if ((mBuffer[1] & 0x02) != 0)     // Dado de orientación
                {
                    datoorientacion = System.BitConverter.ToSingle(mBuffer, posLecturaBuffer);
                    //datolatitud = System.BitConverter.ToUInt32(mBuffer, 7);
                    //datolongitud = System.BitConverter.ToUInt32(mBuffer, 11);
                    datolatitud = (long)((mBuffer[posLecturaBuffer+7] << 24) | (mBuffer[posLecturaBuffer+6] << 16) | (mBuffer[posLecturaBuffer+5] << 8) | mBuffer[posLecturaBuffer+4]);
                    datolongitud = (long)((mBuffer[posLecturaBuffer+11] << 24) | (mBuffer[posLecturaBuffer+10] << 16) | (mBuffer[posLecturaBuffer+9] << 8) | mBuffer[posLecturaBuffer+8]);
                    //datolatitud = 180;
                    //datolongitud = 90;
                    this.Invoke(new EventHandler(interpretaDO));
                }
                if (comandoactual == ComandoEnvio.Logger)
                {
                    estadoactual = EstadoPeticion.Ninguno;
                    comandoactual = ComandoEnvio.Ninguno;
                }
            }

            if (((char)mBuffer[0] == 'D') & ((char)mBuffer[1] == 'O')) // Recibiendo dato de orientación
            {
                datoorientacion = System.BitConverter.ToSingle(mBuffer, 3);
                datolatitud = System.BitConverter.ToUInt32(mBuffer, 7);
                datolongitud  = System.BitConverter.ToUInt32(mBuffer, 11);
                this.Invoke(new EventHandler(interpretaDO));
                UltimoLogBrujula = DateTime.Now;
                //MessageBox.Show(datoorientacion.ToString());
             }

            if (((char)mBuffer[0] == 'V') & (comandoactual == ComandoEnvio.ComandoVelo) & (mBuffer[4] == (byte)2))
            {
                // Recibida respuesta correcta a comando de velociad
                if (comandoactual == ComandoEnvio.ComandoVelo)
                {
                    comandoactual = ComandoEnvio.Ninguno;
                    estadoactual = EstadoPeticion.Ninguno;
                    MostrarTexto("Ok a comando velo\n");
                }
            }

            if (((char)mBuffer[0] == 'P') & (comandoactual == ComandoEnvio.ComandoPID) & (mBuffer[4] == (byte)3))
            {
                // Recibida respuesta correcta a comando de velociad
                if (comandoactual == ComandoEnvio.ComandoPID)
                {
                    comandoactual = ComandoEnvio.Ninguno;
                    estadoactual = EstadoPeticion.Ninguno;
                }
            }

            if (((char)mBuffer[0] == 'C') & (comandoactual == ComandoEnvio.ComandoControl) & (mBuffer[4] == (byte)1))
            {
                // Recibida respuesta correcta a comando de control
                if (comandoactual == ComandoEnvio.ComandoControl)
                {
                    comandoactual = ComandoEnvio.Ninguno;
                    estadoactual = EstadoPeticion.Ninguno;
                    MostrarTexto("Ok a comando control\n");
                }
            }

        }

        private void interpretaAR(object s, EventArgs e)
        {
            //if (this.tabMain.SelectedTab.Name == "Grafica")
            //{
               // Medida amedida = new Medida();
               // amedida.consumo = consumo;
               // amedida.velocidad = velocidad;
            _adquisicion.AddMedida((Medida)s);
            //ActualizaGrafica(velocidad);
            //ActualizaGrafica(consumo);
            //}
        }


        private void ActualizaGrafica(int punto)
        {
            RecorreArray(punto);
            this.chart_consumo.Series[0].Points.Clear();
            for (int i = 0; i < 50; i++)
            {
                //this.chart_Datos.Series[0].Points.AddY(muestreo[i]);
                this.chart_consumo.Series[0].Points.Add(muestreo[i]);
            }
        }

        private void ActualizaGrafica(long punto)
        {
            RecorreArray(punto);
            this.chart_Datos.Series[0].Points.Clear();
            for (int i = 0; i < 50; i++)
            {
                this.chart_Datos.Series[0].Points.AddY(muestreo_long[i]);
            }
        }

        private void RecorreArray(int punto)
        {
            int[] muestreoAux = new int[50];
            for (int i = 0; i < 49; i++)
            {
                muestreoAux[i + 1] = muestreo[i];
            }
            muestreoAux[0] = punto;
            muestreo = muestreoAux;
        }

        private void RecorreArray(long punto)   // Sobrecarga para el caso en que el punto a graficar sea un long....
        {
            long[] muestreoAux = new long[50];
            for (int i = 0; i < 49; i++)
            {
                muestreoAux[i + 1] = muestreo_long[i];
            }
            muestreoAux[0] = punto;
            muestreo_long = muestreoAux;
        }

        private void actualizar(object s, EventArgs e)
        {
            //int valor;
            if (this.txtLog.InvokeRequired)
            {
                MostrarTextoCallback d = new MostrarTextoCallback(MostrarTexto);
                this.BeginInvoke(d, new object[] { srtReceived });
            }
            else
            {
                velocidad = 0;
                // Lógica envío / recepción de datos
                // Dato comienza por 'B', numDatos, datos[..], '#'
                //valor = interpretatrama;
                ActualizaGrafica(velocidad);
                if (srtReceived[numDatos + 2] == '#')
                {
                    bto_ConfirmacionRecepcion_Click(null, null);
                }
            }
        }

        // método inicial interpreta trama.
        // se cambia a interpretrama 2 para pasar a leer un buffer de bytes:
        //private void interpretaAR(int valor)
        //{
        //    if (this.tabMain.SelectedTab.Name == "Grafica") 
        //       {ActualizaGrafica(valor); 
        //       }
        //     bto_ConfirmacionRecepcion_Click(null, null);
        //        if (mBufferRead[numDatos + 3] == (byte)'#')
        //        {
        //            //bto_ConfirmacionRecepcion_Click(null, null);
        //        }
        //    else MostrarTexto("Cabecera incorrecta: " + (char)mBufferRead[0] + '\n');
        //}

 
        private void interpretaDO(object s, EventArgs e)
        {
            if (this.tabMain.SelectedTab.Name == "Brujula")
            {
                Bitmap Imagen = new Bitmap(@"D:\Google Drive\AA_VMSA\Programacion\VisualStudio\Control_VMSA_V0.5\Control_VMSA\Imagenes\Brujula.gif", true);
                pictureBox1.Image = rotateImage(Imagen, datoorientacion);
                this.tB_Brujula.Text = datoorientacion.ToString();
                this.tB_Latitud.Text = Convert.ToString(datolatitud);
                this.tB_Longitud.Text = Convert.ToString(datolongitud);
            }

            bto_ConfirmacionRecepcion_Click(null, null);   // descomentar esto, que si no no vuelve a mandar el arduino
        }
        
        // Borrar?
        private void interpretatrama(object s, EventArgs e)
        {
            int valor;
            if ((srtReceived[0] == 0x41) & (srtReceived[1] == 0x52))
            {
                numDatos = (int)char.GetNumericValue(srtReceived[2]);
                //valor = ((int)srtReceived[3] << 8) | (int)srtReceived[4];
                valor = (byte)srtReceived[4];
                //MostrarTexto("Cabecera recibida ok. Valor: " + valor + '\n');
                for (int i = 0; i < 6; i++)
                {
                    MostrarTexto("srt[" + i + "]: " + srtReceived[i].ToString() + '\n');
                }
                    if (tabMain.SelectedTab.Name == "Grafica") { ActualizaGrafica(valor); }
                if (srtReceived[numDatos + 3] == '#')
                {
                    bto_ConfirmacionRecepcion_Click(null, null);
                }
            }
            else  MostrarTexto("Cabecera incorrecta: " + srtReceived[0] + '\n');
        }
      


        /* Graficación de la brújula*/
        private Bitmap rotateImage(Bitmap b, float angle)
        {
            Bitmap retunrBitmap = new Bitmap(b.Width, b.Height);
            Graphics g = Graphics.FromImage(retunrBitmap);
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            g.DrawImage(b, new Point(0, 0));
            return retunrBitmap;
        }

        private float escalavalor(int valor)  // Sirve para simular la orintación con potenciómetro. Definitivo en escalavalor_f
        {
            float myvalor;
            myvalor = (float)360 / 1023 * valor - 180;
            return myvalor;
        }


        private void MostrarTexto(string texto)
        {
            if (this.txtLog.InvokeRequired)
            {
                MostrarTextoCallback d = new MostrarTextoCallback(MostrarTexto);
                this.BeginInvoke(d, new object[] { texto });
            }
            else
            {
                txtLog.AppendText(texto);
            }
        }
        delegate void MostrarTextoCallback(string texto);

        private void EnviaComando(byte[] buffer, int numDatos)
        {
            if (puerto.IsOpen)
            {
                try
                {
                    puerto.Write(buffer, 0, numDatos);
                }

                catch (Exception ex)
                {
                    MostrarTexto(ex.ToString());
                }
            }
            else { txtLog.Text = "Puerto desconectado\n"; }

        }

        private void BTO_Conectar_Click(object sender, EventArgs e)
        {
            mnuConectar_Click(null, null);
        }

        private void btoConectar_Click(object sender, EventArgs e)
        {
            mnuConectar_Click(null, null);
        }

        private void mnuDesconectar_Click(object sender, EventArgs e)
        {
            mBufferWrite[0] = 76;    // Comando logger 'L'
            mBufferWrite[1] = 0;     // Orden desactiva log
            EnviaComando(mBufferWrite, 2);
            estadoactual = EstadoPeticion.Ninguno;
            sendlogger[0] = 0;
            puerto.Close();
            lblEstado.Text = "Estado: Desconectado ";
            byteant = 0;
            indReception = 0;
        }

        private void mnuBorrar_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
        }

        private void btoActivarLog_01_Click(object sender, EventArgs e)
        {
            activaLogGrafica();
        }

        private void btoDesactivarLog_01_Click(object sender, EventArgs e)
        {
            desactivaLogGrafica();
        }

        private void btoSaveLog_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText("d:\\hola.txt", txtLog.Text);
        }

        private void bto_ConfirmacionRecepcion_Click(object sender, EventArgs e)
        {
            mBufferWrite[0] = 0x06;
            mBufferWrite[1] = (byte)13;  // CR
            mBufferWrite[2] = (byte)10;  // LF
            EnviaComando(mBufferWrite, 3);
        }

        private void activaLogGrafica()
        {
            sendlogger[0] |= 0x01;
        }

        private void desactivaLogGrafica()
        {
            sendlogger[0] &= 0xFE;
        }

        private void activaLogOrientacion()
        {
            sendlogger[0] |= 0x02;
        }

        private void desactivaLogOrientacion()
        {
            sendlogger[0] &= 0xFD;
        }

        private void activaLogBdV()  // Acitvaciión del log 15 para el BdV. A 1 pasa el control al servo, con 0 pasa el control automáticamente a radiofrecuencia
        {
            sendlogger[1] |= 0x80;
        }

        private void desactivaLogBdV()
        {
            sendlogger[1] &= 0x7F;
        }



        private void enviaComandoVelocidad(byte vel_IZQ, byte vel_DER)
        {
            byte[] mBufferWrite;
            byte[] mBufferAttention = new byte[] { (byte)'A' };     // Primero se envía solicitud de atención al 8051
            mBufferWrite = new byte[Constants.LONG.COMMANDVELO];
            mBufferWrite[0] = Constants.COMANDOS.STX;           // STX
            mBufferWrite[1] = Constants.COMANDOS.COMMANDVELO;   // Comando velocidad
            mBufferWrite[2] = 2;   // Comando velocidad
            mBufferWrite[3] = vel_IZQ;
            mBufferWrite[4] = vel_DER;
            mBufferWrite[5] = Constants.COMANDOS.CR;  // CR
            mBufferWrite[6] = Constants.COMANDOS.LF;  // LF

            if ((estadoactual == EstadoPeticion.Ninguno) && false)
            {
                //EnviaComando(mBufferWrite, Constants.LONG.COMMANDVELO);
                EnviaComando(mBufferAttention, 1);
                estadoactual = EstadoPeticion.EsperandoACK;
                comandoactual = ComandoEnvio.ComandoVelo;
                UltimoEnvio.buffer = mBufferWrite;
                UltimoEnvio.numdatos = Constants.LONG.COMMANDVELO;
                UltimoEnvioComando = DateTime.Now;
            }
            else
            {
                for (int i = 0; i < ListaEnvios.Count; i++)
                {
                    if (ListaEnvios[i].Comando == ComandoEnvio.ComandoVelo)
                    {
                        ListaEnvios[i].buffer = mBufferWrite;
                        return;
                    }
                }
                Envio envio = new Envio();
                envio.Comando = ComandoEnvio.ComandoVelo;
                envio.buffer = mBufferWrite;
                envio.numdatos = 7;
                ListaEnvios.Add(envio);
            }
        }

        private void enviaComandoControl(char control)
        {
            byte[] mBufferWrite;
            byte[] mBufferAttention = new byte[] { (byte)'A' };     // Primero se envía solicitud de atención al 8051
            mBufferWrite = new byte[Constants.LONG.COMMANDVELO];
            mBufferWrite[0] = Constants.COMANDOS.STX;           // STX
            mBufferWrite[1] = Constants.COMANDOS.COMMANDCONTROL;   // Comando velocidad
            mBufferWrite[2] = 1;   // Comando velocidad
            mBufferWrite[3] = (byte)control;
            mBufferWrite[4] = Constants.COMANDOS.CR;  // CR
            mBufferWrite[5] = Constants.COMANDOS.LF;  // LF

            if (estadoactual == EstadoPeticion.Ninguno)
            {
                //EnviaComando(mBufferWrite, Constants.LONG.COMMANDVELO);
                EnviaComando(mBufferAttention, 1);
                estadoactual = EstadoPeticion.EsperandoACK;
                comandoactual = ComandoEnvio.ComandoControl;
                UltimoEnvio.buffer = mBufferWrite;
                UltimoEnvio.numdatos = Constants.LONG.COMMANDCONTROL;
                UltimoEnvioComando = DateTime.Now;
            }
            else
            {
                Envio envio = new Envio();
                envio.Comando = ComandoEnvio.ComandoControl;
                envio.buffer = mBufferWrite;
                envio.numdatos = Constants.LONG.COMMANDCONTROL;
                ListaEnvios.Add(envio);
            }
        }

        private void PeticionLogger(byte[] flagLogger)
        {
            byte[] mBufferWrite;
            byte[] mBufferAttention = new byte[] { (byte)'A' };     // Primero se envía solicitud de atención al 8051
            mBufferWrite = new byte[6];
            mBufferWrite[0] = 0x02; // STX
            mBufferWrite[1] = 10;   // Comando petición Logger
            mBufferWrite[2] = flagLogger[0];
            mBufferWrite[3] = flagLogger[1];
            mBufferWrite[4] = (byte)13;  // CR
            mBufferWrite[5] = (byte)10;  // LF
            //EnviaComando(mBufferWrite, 6);
            EnviaComando(mBufferAttention, 1);
            estadoactual = EstadoPeticion.EsperandoACK;
            comandoactual = ComandoEnvio.Logger;
            UltimoEnvio.numdatos = 6;
            UltimoEnvio.buffer = mBufferWrite;
            UltimoEnvioComando = DateTime.Now;
        }


        private void enviaparametros_PID_Velo(float consKP, float consKI, float consKD)
        {
            int numdatos = 0;
            byte[] byteArray_KP = BitConverter.GetBytes(consKP);
            byte[] byteArray_KI = BitConverter.GetBytes(consKI);
            byte[] byteArray_KD = BitConverter.GetBytes(consKD);

            mBufferWrite[0] = Constants.COMANDOS.STX;
            mBufferWrite[1] = 80;   // Comando de parámetros 'PID'
            mBufferWrite[2] = 3;    // Número de datos comando de PID

            mBufferWrite[3] = byteArray_KP[0];
            mBufferWrite[4] = byteArray_KP[1];
            mBufferWrite[5] = byteArray_KP[2];
            mBufferWrite[6] = byteArray_KP[3];

            mBufferWrite[7] = byteArray_KI[0];
            mBufferWrite[8] = byteArray_KI[1];
            mBufferWrite[9] = byteArray_KI[2];
            mBufferWrite[10] = byteArray_KI[3];

            mBufferWrite[11] = byteArray_KD[0];
            mBufferWrite[12] = byteArray_KD[1];
            mBufferWrite[13] = byteArray_KD[2];
            mBufferWrite[14] = byteArray_KD[3];

            mBufferWrite[15] = (byte)13;  // CR
            mBufferWrite[16] = (byte)10;  // LF

            if (estadoactual == EstadoPeticion.Ninguno)
            {
                EnviaComando(mBufferWrite, 17);
                estadoactual = EstadoPeticion.EsperandoRespuesta;
                comandoactual = ComandoEnvio.ComandoPID;
                UltimoEnvio.buffer = mBufferWrite;
                UltimoEnvio.numdatos = numdatos;
                UltimoEnvioComando = DateTime.Now;
            }
            else
            {
                Envio envio = new Envio();
                envio.Comando = ComandoEnvio.ComandoPID;
                envio.buffer = mBufferWrite;
                envio.numdatos = numdatos;
                ListaEnvios.Add(envio);
            }
        }

        private void bto_Iniciar_Click(object sender, EventArgs e)
        {
            activaLogGrafica();
        }

        private void bto_Stop_Click(object sender, EventArgs e)
        {
            desactivaLogGrafica();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] mBufferAttention = new byte[] { (byte)'A' };     // Primero se envía solicitud de atención al 8051
            //if (LogGraficacion)
            //{
            //    if (DateTime.Compare(DateTime.Now, UltimoLogGraficacion.AddMilliseconds(500)) > 0)
            //    {
            //        UltimoLogGraficacion = DateTime.Now;
            //        activaLogGrafica();
            //    }
            //}
            //if (LogBrujula)
            //{
            //    if (DateTime.Compare(DateTime.Now, UltimoLogBrujula.AddMilliseconds(2000)) > 0)
            //    {
            //        UltimoLogBrujula = DateTime.Now;
            //        activaLogOrientacion();
            //        contFallosLogBrujula++;
            //        MostrarTexto("Contador fallos logBrujula: " + contFallosLogBrujula + '\n');
            //        //MessageBox.Show("tiempo superado");
            //    }
            //}
            ///////////////////////////////////////////
            // Control lista envio
            ///////////////////////////////////////////
            if ((estadoactual == EstadoPeticion.Ninguno) && (ListaEnvios.Count > 0))
            {
                estadoactual = EstadoPeticion.EsperandoACK;
                //EnviaComando(ListaEnvios[0].buffer, ListaEnvios[0].numdatos);
                comandoactual = ListaEnvios[0].Comando;
                UltimoEnvio = ListaEnvios[0];
                ListaEnvios.RemoveAt(0);
                EnviaComando(mBufferAttention, 1);
                UltimoEnvioComando = DateTime.Now;
            }

            bool exixtlogger = (sendlogger[0] != 0 | sendlogger[1] != 0);
            if ((exixtlogger) & (estadoactual == EstadoPeticion.Ninguno))
            {
                PeticionLogger(sendlogger);
                //estadoactual = EstadoPeticion.EsperandoRespuesta;  MIRAR ESTO SI SIGUE FUNCIOANDNO .....-*****///***---****////
                comandoactual = ComandoEnvio.Logger;
                UltimoEnvioComando = DateTime.Now;
            }
            // En caso de fallo (se sobrepasa el tiempo esperando respuesta
            if ((estadoactual == EstadoPeticion.EsperandoRespuesta) && 
                (DateTime.Compare(DateTime.Now, UltimoEnvioComando.AddMilliseconds(5000)) > 0))
            {
                ContadorFallosSerial++;
                MostrarTexto("Tiempo excesivo respuesta último comando. Fallo: " + ContadorFallosSerial + '\n');
                UltimoEnvioComando = DateTime.Now;
                if (iniciadatrama)
                {
                    MostrarTexto("iniciada trama\n");
                }
                if (comandoactual == ComandoEnvio.ComandoVelo)
                {
                    comandoactual = ComandoEnvio.Ninguno;
                    estadoactual = EstadoPeticion.Ninguno;
                    MostrarTexto("Error de comando");
 
                }  // Terminar esto
                if (comandoactual == ComandoEnvio.Logger)
                {
                    comandoactual = ComandoEnvio.Ninguno;
                    estadoactual = EstadoPeticion.Ninguno;
                }
                
            }

            if ((estadoactual == EstadoPeticion.EsperandoACK) && 
                 //(comandoactual == ComandoEnvio.ComandoVelo) && 
                (DateTime.Compare(DateTime.Now, UltimoEnvioComando.AddMilliseconds(300)) > 0))
            {
                MostrarTexto("Tiempo excesivo ACK último comando velocidad. Reenvio comando" + '\n');
                EnviaComando(mBufferAttention, 1);
                UltimoEnvioComando = DateTime.Now;
            }

            actEstadoBoton(sendlogger[1]);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            byte[] mBufferWrite;
            byte[] mBufferAttention = new byte[] { (byte)'A' };     // Primero se envía solicitud de atención al 8051
            mBufferWrite = new byte[6];
            mBufferWrite[0] = 0x02; // STX
            mBufferWrite[1] = 10;   // Comando petición Logger
            mBufferWrite[2] = 0x01;
            mBufferWrite[3] = 0x00;
            mBufferWrite[4] = (byte)13;  // CR
            mBufferWrite[5] = (byte)10;  // LF
            if (estadoactual == EstadoPeticion.Ninguno)
            {
                //EnviaComando(mBufferWrite, Constants.LONG.COMMANDVELO);
                EnviaComando(mBufferAttention, 1);
                estadoactual = EstadoPeticion.EsperandoACK;
                comandoactual = ComandoEnvio.Logger;
                UltimoEnvio.buffer = mBufferWrite;
                UltimoEnvio.numdatos = 6;
                UltimoEnvioComando = DateTime.Now;
            }

        }

        private void bto_IniciarLoggerBrujula_Click(object sender, EventArgs e)
        {
            activaLogOrientacion();
        }

        private void bto_DetenerLoggerBrujula_Click(object sender, EventArgs e)
        {
            desactivaLogOrientacion();
        }

        private void trk_PotenciaMotores_ValueChanged(object sender, EventArgs e)
        {
            tB_PotenciaMotores.Text = trk_PotenciaMotores.Value.ToString();
            if (motorDrone.motor.motor_ON)
            {
                motorDrone.motor.potencia = (byte)trk_PotenciaMotores.Value;
                enviaComandoVelocidad(motorDrone.motor.potencia, motorDrone.motor.bascula);
            }
        }

        private void trk_BasculaMotores_ValueChanged(object sender, EventArgs e)
        {
            tb_BasculaMotores.Text = trk_BasculaMotores.Value.ToString();
            if (motorDrone.motor.motor_ON)
            {
                motorDrone.motor.bascula = (byte)trk_BasculaMotores.Value;
                enviaComandoVelocidad(motorDrone.motor.potencia, motorDrone.motor.bascula);
            }
        }



        private void tB_PotenciaMotores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (Convert.ToInt32(tB_PotenciaMotores.Text) > 255) MostrarTexto("Potencia incorrecta");
                else
                {
                    trk_PotenciaMotores.Value = Convert.ToByte(tB_PotenciaMotores.Text);
                    if (motorDrone.motor.motor_ON) motorDrone.motor.potencia = Convert.ToByte(tB_PotenciaMotores.Text);
                }
 
            }
        }

        private void tB_BasculaMotores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                if (Convert.ToInt32(tb_BasculaMotores.Text) > 255) MostrarTexto("La bascula debe estar entre 0 .. 255");
                else
                {
                    trk_BasculaMotores.Value = Convert.ToByte(tb_BasculaMotores.Text);
                    if (motorDrone.motor.motor_ON) motorDrone.motor.bascula = Convert.ToByte(tb_BasculaMotores.Text);
                }

            }
        }


        private void bot_MarchaMotores_Click(object sender, EventArgs e)
        {
            motorDrone.motor.motor_ON = true;
            motorDrone.motor.potencia = Convert.ToByte(tB_PotenciaMotores.Text);
            enviaComandoVelocidad(motorDrone.motor.potencia, motorDrone.motor.bascula);
            this.pB_LedMotorIzq.Visible = true;
            MostrarTexto("Enviado comando marcha\n");
        }

        private void bto_ParoMotores_Click(object sender, EventArgs e)
        {
            motorDrone.motor.motor_ON = false;
            motorDrone.motor.potencia = 0;
            enviaComandoVelocidad(motorDrone.motor.potencia, motorDrone.motor.bascula);
            this.pB_LedMotorIzq.Visible = false;
            MostrarTexto("Enviado comando paro\n");
        }

 
 

        private void bt_Send_ParamPID_Click(object sender, EventArgs e)
        {
            enviaparametros_PID_Velo((float)Convert.ToDouble(tb_consKP.Text), (float)Convert.ToDouble(tb_consKI.Text), (float)Convert.ToDouble(tb_consKD.Text));
            
        }

        private void bt_PeticionLoggerOrientacion_Click(object sender, EventArgs e)
        {
            byte[] mBufferWrite;
            byte[] mBufferAttention = new byte[] { (byte)'A' };     // Primero se envía solicitud de atención al 8051
            mBufferWrite = new byte[6];
            mBufferWrite[0] = 0x02; // STX
            mBufferWrite[1] = 10;   // Comando petición Logger
            mBufferWrite[2] = 0x02;
            mBufferWrite[3] = 0x00;
            mBufferWrite[4] = (byte)13;  // CR
            mBufferWrite[5] = (byte)10;  // LF
            if (estadoactual == EstadoPeticion.Ninguno)
            {
                //EnviaComando(mBufferWrite, Constants.LONG.COMMANDVELO);
                EnviaComando(mBufferAttention, 1);
                estadoactual = EstadoPeticion.EsperandoACK;
                comandoactual = ComandoEnvio.Logger;
                UltimoEnvio.buffer = mBufferWrite;
                UltimoEnvio.numdatos = 6;
                //UltimoEnvioComando = DateTime.Now;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float valorfloat, valorrecuperado;
            valorfloat = 100.50f;
            byte[] byteArray_valorfloat = BitConverter.GetBytes(valorfloat);
            valorrecuperado = System.BitConverter.ToSingle(byteArray_valorfloat, 0);

        }

        private void bt_Comando_Rx_Click(object sender, EventArgs e)
        {
            //enviaComandoControl('R');   // Enviá comando de control por radiofrecuencia
            desactivaLogBdV();
        }

        private void bt_ComandoServo_Click(object sender, EventArgs e)
        {
            //enviaComandoControl('S');   // Envia comando de control por servocontrol (.net)
            //Desestimada la línea anterior. Para pasar el control al servo, se pondrá a 1 el logger 15, BdV.....
            activaLogBdV();
        }

        private  void actEstadoBoton(byte estado)
        {
            if ((estado & 0x80) != 0)
            {
                this.bt_Comando_Rx.BackColor = Color.Gray;
                this.bt_ComandoServo.BackColor = Color.Green;
            }
            else
            {
                this.bt_Comando_Rx.BackColor = Color.Red;
                this.bt_ComandoServo.BackColor = Color.Gray;
            }
        }

        private void Control_VMSA_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.InsertaLogApp("-------- Finalizando Control_VMSA", true);
        }

        private void Control_VMSA_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                this.Visible = false;
        }

        private void Control_VMSA_Activated(object sender, EventArgs e)
        {
            //' La primera vez que se active, ocultar el form,
            //' es una chapuza, pero el formulario no permite que se oculte en el Form_Load
            if (PrimeraVez)
            {
                PrimeraVez = false;
                //Visible = false;
            }
        }

        private void SalirAplicacion()
        {
            DialogResult result1 = MessageBox.Show("Se dispone a salir de la aplicación.\n ¿Desea continuar?",
                    "Cerrar Servidor", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result1 == DialogResult.OK)
            {
                MemoriaAntesCerrar = Log.AppMemoryUsage();
                notifyIcon1.BalloonTipText = "Finalizando Control VMSA";
                _adquisicion.Terminado = true;
                if (backgroundWorker1.IsBusy)
                    autoEvent.Set(); 
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

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalirAplicacion();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                //lblResult.Text = "Cancelado!";
            }
            else if (e.Error != null)
            {
                // lblResult.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // lblResult.Text = "Hecho!";
                PuedeCerrar = true;
                this.Close();
            }
            //_resetEvent.Set(); // signal that worker is done  
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // aqui se pone el codigo que queremos ejecutar en el thread
            BackgroundWorker worker = sender as BackgroundWorker;

            _adquisicion = new Adquision(autoEvent);
            _adquisicion.ControlAdquisionWait(worker, e);
            _adquisicion = null;

        }

        // Tabla que usamos para almacenar los valores temporales
        private DataTable _oTableStatus = null;
        private IList tableDataSource = null;

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");

            // lblStatus.Text = (string)e.UserState;
            if (_oTableStatus == null)
            {
                _oTableStatus = new DataTable("MEDIDAS");
                _oTableStatus.Columns.Add("FECHA", typeof(DateTime));
                _oTableStatus.Columns.Add("VELOCIDAD", typeof(Double));
                _oTableStatus.Columns.Add("CONSUMO", typeof(Double));
                _oTableStatus.DefaultView.Sort = "FECHA ASC";//ordenación de la tabla por la fecha
                dgvStatus.DataSource = _oTableStatus;

                tableDataSource = (_oTableStatus as IListSource).GetList();

                String aserie = "Velocidad";
                //this.chart_Datos.Series.Add(aserie);
                //this.chart_Datos.Series[0].ChartType = SeriesChartType.Spline;
                this.chart_Datos.Series[0].XValueMember = "FECHA";
                this.chart_Datos.Series[0].XValueType = ChartValueType.DateTime;
                this.chart_Datos.Series[0].YValueMembers = "VELOCIDAD";
                this.chart_Datos.Series[0].YValueType = ChartValueType.Auto;

                this.chart_Datos.DataSource = tableDataSource;
                

                aserie = "Consumo";
                //this.chart_consumo.Series.Add(aserie);
                //this.chart_consumo.Series[0].ChartType = SeriesChartType.Spline;
                this.chart_consumo.Series[0].XValueMember = "FECHA";
                this.chart_consumo.Series[0].XValueType = ChartValueType.DateTime;
                this.chart_consumo.Series[0].YValueMembers = "CONSUMO";
                this.chart_consumo.Series[0].YValueType = ChartValueType.Auto;
                this.chart_consumo.DataSource = tableDataSource;

                // this.chart_Datos.DataBindTable(tableDataSource);
                // this.chart_consumo.DataBindTable(tableDataSource);
                
                
            }

            List<Medida> aMedidas = (List<Medida>)e.UserState;

            for (int i = 0; i < aMedidas.Count; i++)
            {
                DataRow oRow = _oTableStatus.NewRow();
                oRow[0] = aMedidas[i].fecha;
                oRow[1] = aMedidas[i].velocidad;
                oRow[2] = aMedidas[i].consumo;
                _oTableStatus.Rows.Add(oRow);
                if (_oTableStatus.Rows.Count > 1000)
                {
                    _oTableStatus.Rows.RemoveAt(0);
                }
            }


            this.chart_Datos.DataBind();
            this.chart_consumo.DataBind();
           // this.chart_Datos.Series[0].Points.AddY(muestreo_long[i]);

           // ActualizaGrafica(velocidad);
        }

        private void Control_VMSA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PuedeCerrar)
            {
                if (this.Visible)
                    this.Visible = false;
                e.Cancel = true;
            }
            else
            {
                notifyIcon1.BalloonTipText = "Terminado Control VMSA";
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

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalirAplicacion();
        }

        private void chart_Datos_Click(object sender, EventArgs e)
        {

        }


    }
    // Ejemplo de paso de argumento a evento (por si hace falta....)
    public class Eventos : EventArgs
    {
        private int myvalor;

        public Eventos(int myvalor)
        {
            this.Myvalor = myvalor;
        }

        public int Myvalor
        {
            get { return this.myvalor; }
            set
            {
                if (this.myvalor != value)
                {
                    myvalor = value;
                }
            }
 
        }
    }
}


