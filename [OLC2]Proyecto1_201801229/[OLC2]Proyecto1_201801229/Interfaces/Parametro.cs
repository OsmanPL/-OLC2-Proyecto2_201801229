using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Parametro
    {
        Simbolo.TipoDato tipo;
        Operacion valor;
        String type;

        public Parametro(Simbolo.TipoDato tipo, Operacion valor, String type)
        {
            this.Tipo = tipo;
            this.Valor = valor;
            this.Type = type;
        }

        public Operacion Valor { get => valor; set => valor = value; }
        public string Type { get => type; set => type = value; }
        internal Simbolo.TipoDato Tipo { get => tipo; set => tipo = value; }
    }
}
