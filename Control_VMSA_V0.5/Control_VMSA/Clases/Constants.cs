using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_VMSA
{
    class Constants
    {
        public struct LONG
        {
            public const int COMMANDVELO = 7;       // Tamaño trama de comando de velocidad
            public const int TAMANOMINIMO = 6;
            public const int COMMANDCONTROL = 6;    // Tamaño trama de comando de control

        }
        public struct COMANDOS
        {
            public const byte STX = 0x02;
            public const byte CR = (byte)13;         // CR
            public const byte LF = (byte)10;         // LF
            public const byte COMMANDVELO = 86;      // 'V'
            public const byte COMMANDCONTROL = 67;   // 'C'
        }
    }
}
