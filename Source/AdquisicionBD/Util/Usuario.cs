using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace configurador
{

        public enum NivelAutorizacion { Desconocido, Estandar, Tecnico, Administrador, Desarrollador }

        /// <summary>
        /// Proporciona los recursos necesarios para gestionar los usuarios de una aplicación
        /// </summary>
        public class Usuario
        {
            #region Propiedades y Métodos Estáticos

            /// <summary>
            /// Verifica si el usuario está o no validado.
            /// </summary>
            /// <param name="strUser">Nombre del usuario a validar</param>
            /// <param name="strPass">Password del usuario a validar</param>
            /// <returns>Objeto usuario si está validado, null en caso contrario</returns>
            public static Usuario Validar(string strUser, string strPass)
            {
                Usuario oUsuario = null;
                DataSet oData = null;
                string strQuery = "";
                string strEmail = "";
                string strDescripcion = "";
                NivelAutorizacion nivelAutorizacion = NivelAutorizacion.Desconocido;
                int intNivel = 0;
                int idUser = 0;

                strQuery = "Select niveles_acceso.Codigo, usuarios.OID, usuarios.Descripcion, usuarios.Email " +
                    "from niveles_acceso, usuarios where usuarios.or_nivel_remoto= niveles_acceso.oid " +
                    " and usuarios.Identificador='" + strUser + "' and usuarios.Clave='" + strPass+"'";

                oData =Funciones.ConsultarDatosABD(strQuery);
                        
                if (oData!=null && oData.Tables.Count > 0)
                {
                    foreach(DataRow oRow in oData.Tables[0].Rows)
                    {
                        intNivel = (int)oRow["Codigo"];
                        strDescripcion=(string)oRow["Descripcion"];
                        strEmail = (string)oRow["Email"];
                        idUser = (int)oRow["OID"];
                    }
                        nivelAutorizacion = NivelAutorizacion.Desconocido;
                    if (intNivel >= 0 && intNivel < 900)
                    {
                        nivelAutorizacion = NivelAutorizacion.Estandar;
                    }
                    if (intNivel >= 900 && intNivel < 9000)
                    {
                        nivelAutorizacion = NivelAutorizacion.Tecnico;
                    }
                    if (intNivel >= 9000 && intNivel < 90000)
                    {
                        nivelAutorizacion = NivelAutorizacion.Administrador;
                    }
                    if (intNivel >= 90000)
                    {
                        nivelAutorizacion = NivelAutorizacion.Desarrollador;
                    }
                                        
                    oUsuario = new Usuario(idUser, strUser, strPass, nivelAutorizacion,strDescripcion,strEmail);
                }
                return oUsuario;
            }


            #endregion

            
            #region Atributos y Propiedades

            private string _nombre = null;
            private string _password = null;
            private NivelAutorizacion _autorizacion = NivelAutorizacion.Desconocido;
            private bool _usuarioRegistrado = false;
            private int _idUsuario = 0;
            private string _email = "";
            private string _descripcion = "";


            /// <summary>
            /// Obtiene/Devuelve el nombre del usuario
            /// </summary>
            public string Nombre
            {
                set { _nombre = value; }
                get { return _nombre; }
            }

            public string Password
            {
                set { _password = value; }
                get { return _password; }
            }

            public NivelAutorizacion NivelDeAutorizacion
            {
                set { _autorizacion = value; }
                get { return _autorizacion; }
            }
            public string Email
            {
                set { _email = value; }
                get { return _email; }
            }
            public string Descripcion
            {
                set { _descripcion = value; }
                get { return _descripcion; }
            }
            public int IdUsuario
            {
                set { _idUsuario = value; }
                get { return _idUsuario; }
            }
            /// <summary>
            /// Cuando el usuario está registrado devuelve true, en caso contrario false
            /// </summary>
            public bool UsuarioRegistrado
            {
                get;
                set;
            }

            #endregion

            #region Constructores
            /*******  Constructores  ****/
            //public Usuario() { }

            //public Usuario(int idUser, string strUser, string strPass, NivelAutorizacion nivel)
            //{
            //    _idUsuario = idUser;
            //    _nombre = strUser;
            //    _password = strPass;
            //    _autorizacion = nivel;
            //}

            public Usuario(int idUser, string strUser, string strPass, NivelAutorizacion nivel, string descripcion, string email)
            {
                    this._idUsuario = idUser;
                    this._nombre = strUser;
                    this._password = strPass;
                    this._autorizacion = nivel;
                    this._descripcion = descripcion;
                    this._email = email;
                    this._usuarioRegistrado = true;
            }

            #endregion

            #region Métodos


            /******* Métodos  ************/
 

            public bool CambiarPassword(string passAntigua, string passNueva)
            {
                bool cambioRealizado = false;
                return cambioRealizado;

            }

            #endregion
        }
    }

