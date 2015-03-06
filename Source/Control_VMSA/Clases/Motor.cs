using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_VMSA
{
    class Motor
    {
        public byte potencia;
        public byte bascula;
        public bool motor_ON;

        public Motor()
        {
            potencia = 0;
            bascula = 128;  // 128 -> Los dos motores al 100%
        }
    }

    class Motores
    {
        public Motor motor;

        public Motores()
        {
            motor = new Motor();
        }

        public bool enviacomando()
        {
            //Envia
            return false;
        }
    }
}
