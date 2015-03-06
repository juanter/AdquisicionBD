using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_VMSA
{
    enum EstadoPeticion {Ninguno, EsperandoACK, EsperandoRespuesta}
    // Los tipos de comando que se pueden enviar
    enum ComandoEnvio { Ninguno, Logger, ComandoVelo, ComandoPID, ComandoControl }

    class Envio
    {
        public ComandoEnvio Comando = ComandoEnvio.Ninguno;
        //public string stenvio;
        public byte[] buffer;
        public int numdatos;
    }
}
