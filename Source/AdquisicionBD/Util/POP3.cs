using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using a = System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections;
using System.Net.Sockets;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace ServidorRevsa
{
    class POP_3
    {
        public POP_3()
        {

        }
        [Serializable]
        public class Pop3Exception : System.ApplicationException
        {
            public Pop3Exception(string str)
                : base(str)
            {

            }
        }
        public class Pop3Message
        {
            
            public long number;
            public long bytes;
            public bool retrieved;
            public string message;
            public string id;
        }

        public class Pop3 : System.Net.Sockets.TcpClient
        {
            public static string password1 = "", uNombre1 = "";
            public void Connect(string server, string username, string password, int puerto)
            {
                try
                {
                    string message;
                    string response;
                    ReceiveTimeout = 2000;
                    Connect(server, puerto); //Connect(server, puerto);                    

                    response = Response();
                    if (response.Substring(0, 3) != "+OK")
                    {
                        throw new Pop3Exception(response);
                    }
                    message = "USER " + username + "\r\n";
                    Write(message);
                    response = Response();
                    if (response.Substring(0, 3) != "+OK")
                    {
                        throw new Pop3Exception(response);
                    }
                    message = "PASS " + password + "\r\n";
                    Write(message);
                    response = Response();
                    if (response.Substring(0, 3) != "+OK")
                    {
                        throw new Pop3Exception(response);
                    }
                }
                catch(Exception e)
                {
                 
                }
            }

            public void Disconnect()
            {

                string message;
                string response;
                message = "QUIT\r\n";
                Write(message);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    throw new Pop3Exception(response);
                }
                
            }
            public ArrayList List()
            {
                string message;
                string response;

                ArrayList retval = new ArrayList();

                message = "LIST\r\n";
                
                Write(message);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    throw new Pop3Exception(response);
                }

                while (true)
                {
                    response = Response();
                    if (response == ".\r\n")
                    {
                        return retval;
                    }
                    else
                    {
                        Pop3Message msg = new Pop3Message();
                        char[] seps = { ' ' };
                        string[] values = response.Split(seps);
                        msg.number = Int32.Parse(values[0]);
                        msg.bytes = Int32.Parse(values[1]);
                        msg.retrieved = false;
                        retval.Add(msg);
                        continue;
                    }
                }

            }
            public ArrayList ListCompleta()
            {
                string message;
                string response;
                bool OK = false;
               // Array retval1 = Array.CreateInstance(typeof(String),500);
                List<String> retval2 = new List<string>();
               
                message = "UIDL\r\n";
                Program.varcontrol1 = "solicito la lista";
                Write(message);
                Program.varcontrol1 = "lista solicitada y espero respuesta";
                response = Response();
                Program.varcontrol1 = "obtengo respuesta";
                if (response.Substring(0, 3) != "+OK")
                {
                    Program.varcontrol1 = "error en la respuesta";
                    throw new Pop3Exception(response);
                }

                int i = 0;
                while (OK==false)
                {

                    response = Response();
                    if (response == ".\r\n")
                    {
                        OK = true;
                    }
                    else
                    {
                        Pop3Message msg = new Pop3Message();
                        char[] seps = { ' ' };
                        string[] values = response.Split(seps);
                        //retval1.SetValue(values[1].Replace("\r\n", ""), i); 
                        retval2.Add(values[1].Replace("\r\n", ""));
                        i++;
                        continue;
                    }
                }

                ArrayList retval = new ArrayList();

                message = "LIST\r\n";
                Program.varcontrol1 = "solicito la lista no unica";
                Write(message);
                Program.varcontrol1 = "solicitada la lista no unica espero respuesta";
                response = Response();
                Program.varcontrol1 = "recibido respuesta";
                if (response.Substring(0, 3) != "+OK")
                {
                    Program.varcontrol1 = "error recibiendo respuesta lista no unica";
                    throw new Pop3Exception(response);
                }
                i = 0;
                while (true)
                {
                    response = Response();
                    if (response == ".\r\n")
                    {
                        Program.varcontrol1 = "retorno valores lista";
                        return retval;
                    }
                    else
                    {
                        Pop3Message msg = new Pop3Message();
                        char[] seps = { ' ' };
                        string[] values = response.Split(seps);
                        msg.number = Int32.Parse(values[0]);
                        msg.bytes = Int32.Parse(values[1]);
                        msg.retrieved = false;
                       // msg.id = retval1.GetValue(i).ToString();
                        msg.id = retval2[i];
                        retval.Add(msg);
                        i++;
                        continue;
                    }
                }


            }
            public ArrayList Uidl()
            {
                string message;
                string response;

                ArrayList retval = new ArrayList();

                message = "UIDL\r\n";
                Write(message);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    throw new Pop3Exception(response);
                }

                while (true)
                {
                    response = Response();
                    if (response == ".\r\n")
                    {
                        return retval;
                    }
                    else
                    {
                        Pop3Message msg = new Pop3Message();
                        char[] seps = { ' ' };
                        string[] values = response.Split(seps);
                        msg.number = Int32.Parse(values[0]);
                        msg.id = values[1].Replace("\r\n", "");
                        msg.retrieved = false;
                        retval.Add(msg);
                        continue;
                    }
                }
            }
            public Pop3Message Retrieve(Pop3Message rhs)
            {
                Encoding.GetEncoding("ISO-8859-1");
                string message;
                string response;
                Pop3Message msg = new Pop3Message();
                msg.bytes = rhs.bytes;
                msg.number = rhs.number;
                message = "RETR " + rhs.number + "\r\n";
                Write(message);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    throw new Pop3Exception(response);
                }
                msg.retrieved = true;
                while (true)
                {
                    response = Response();
                    if (response == ".\r\n")
                    {
                        break;
                    }
                    else
                    {
                        Encoding.GetEncoding("ISO-8859-1");
                        msg.message += response;
                    }
                }
                return msg;
            }
            public Pop3Message Top(Pop3Message rhs, int lineas)
            {
                string message;
                string response;
                Pop3Message msg = new Pop3Message();
                msg.bytes = rhs.bytes;
                msg.number = rhs.number;
                message = "TOP " + rhs.number + " " + lineas + "\r\n";
                Write(message);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    throw new Pop3Exception(response);
                }
                msg.retrieved = true;
                while (true)
                {
                    response = Response();
                    if (response == ".\r\n")
                    {
                        break;
                    }
                    else
                    {
                        msg.message += response;
                    }
                }
                return msg;
            }

            public void Delete(Pop3Message rhs)
            {
                string message;
                string response;
                message = "DELE " + rhs.number + "\r\n";
                Write(message);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    throw new Pop3Exception(response);
                }
            }
            private void Write(string message)
            {
                try
                {
                    System.Text.ASCIIEncoding en = new System.Text.ASCIIEncoding();

                    byte[] WriteBuffer = new byte[1024];
                    WriteBuffer = en.GetBytes(message);
                    NetworkStream stream = GetStream();
                    stream.Write(WriteBuffer, 0, WriteBuffer.Length);
                    Debug.WriteLine("WRITE:" + message);
                    
                }
                
                catch(Exception e)
                {
            
                }
               
            }
            private string Response()
            {
                try
                {
                    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                    byte[] serverbuff = new Byte[1024];
                    Program.varcontrol1 = "antes de stream = getStream";
                    NetworkStream stream = GetStream();
                    Program.varcontrol1 = "despues de stream = getStream";
                    int count = 0;
                    while (true)
                    {

                        byte[] buff = new Byte[2];
                        int bytes = stream.Read(buff, 0, 1);
                        if (bytes == 1)
                        {
                            serverbuff[count] = buff[0];
                            count++;
                            if (buff[0] == '\n')
                            {
                                Program.varcontrol1 = "break fin buffer";
                                break;
                            }
                        }
                        else
                        {
                            Program.varcontrol1 = "break fin bytes==1";
                            break;
                        }
                    }
                    Program.varcontrol1 = "antes de retval = enc.GetString(serverbuff, 0, count)";
                    string retval = enc.GetString(serverbuff, 0, count);
                    Program.varcontrol1 = "antes de  Debug.WriteLine(READ: + retval);";
                    Debug.WriteLine("READ:" + retval);
                    
                    return retval;
                }

                catch
                {
                    string retval = "ERROR COMUNICACION";
                    return retval;
                }
            }
        }
    }
}

