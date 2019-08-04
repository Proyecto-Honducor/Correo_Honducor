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
        private static int idempleado;
        private static int idempleadocreado;
        private static int idventa;

        public static int Idlog { get => idlog; set => idlog = value; }
        public static string Nomlog { get => nomlog; set => nomlog = value; }
        public static string Cargolog { get => cargolog; set => cargolog = value; }
        public static int Idempleado { get => idempleado; set => idempleado = value; }
        public static int Idempleadocreado { get => idempleadocreado; set => idempleadocreado = value; }
        public static int Idventa { get => idventa; set => idventa = value; }
    }
}
