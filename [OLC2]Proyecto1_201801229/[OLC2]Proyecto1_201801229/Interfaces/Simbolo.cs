using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Simbolo
    {
        private TipoDato tipo;
        private TipoVarariable tipoVar;
        private String id;
        private Object valor;
        private String type;
        private String entorno;

        public Simbolo(String id, TipoDato tipo, Object valor, String entorno, TipoVarariable tipoVar)
        {
            this.Tipo = tipo;
            this.Id = id;
            this.Valor = valor;
            this.TipoVar = tipoVar;
            this.Entorno = entorno;
        }

        public Simbolo(String id, Object valor, String entorno)
        {
            this.Id = id;
            this.Valor = valor;
            this.TipoVar = TipoVarariable.CONST;
            this.Entorno = entorno;
        }

        public Simbolo(String id, TipoDato tipo, Object valor, String type, String entorno, TipoVarariable tipoVar)
        {
            this.Tipo = tipo;
            this.Id = id;
            this.Valor = valor;
            this.TipoVar = tipoVar;
            this.Type = type;
            this.Entorno = entorno;
        }



        public string Id { get => id; set => id = value; }
        public Object Valor { get => valor; set => valor = value; }
        internal TipoDato Tipo { get => tipo; set => tipo = value; }
        internal TipoVarariable TipoVar { get => tipoVar; set => tipoVar = value; }
        public string Type { get => type; set => type = value; }
        public string Entorno { get => entorno; set => entorno = value; }

        public enum TipoDato
        {
            INTEGER,
            STRING,
            REAL,
            BOOLEAN,
            OBJECT,
            IDENTIFICADOR
        }

        public enum TipoVarariable
        {
            VAR,
            CONST
        }
    }
}
