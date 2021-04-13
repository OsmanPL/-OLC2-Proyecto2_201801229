using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Analizador
{
    class Error
    {
        public enum TipoError
        {
            LEXICO,
            SINTACTICO,
            SEMANTICO
        }

        TipoError tipo;
        String err;
        int linea, columna;

        internal TipoError Tipo { get => tipo; set => tipo = value; }
        public string Err { get => err; set => err = value; }
        public int Linea { get => linea; set => linea = value; }
        public int Columna { get => columna; set => columna = value; }

        public Error(String err, TipoError tipo, int linea, int columna)
        {
            this.Err = err;
            this.Tipo = tipo;
            this.Linea = linea;
            this.Columna = columna;
        }
    }
}
