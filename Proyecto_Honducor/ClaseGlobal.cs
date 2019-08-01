using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Honducor
{

    class ClaseGlobal
    {
        private static int idlog;
        private static string nomlog;
        private static string cargolog;

        public static int Idlog { get => idlog; set => idlog = value; }
        public static string Nomlog { get => nomlog; set => nomlog = value; }
        public static string Cargolog { get => cargolog; set => cargolog = value; }
    }
}
