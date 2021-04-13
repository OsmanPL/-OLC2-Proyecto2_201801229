using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionType:Instruccion
    {
        String id;
        Hashtable campos = new Hashtable();

        public string Id { get => id; set => id = value; }
        public Hashtable Campos { get => campos; set => campos = value; }

        public Object buscarValor(String campo , TablaSimbolos ts)
        {
            Parametro val = (Parametro)Campos[campo];
            Object valor = val.Valor.ejecutar(ts);
            return valor;
        }
        public InstruccionType(String id, Hashtable campos)
        {
            this.Id = id;
            this.Campos = campos;
        }
        public Object ejecutar(TablaSimbolos ts)
        {

            return null;
        }
    }
}
