using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionElseIf:Instruccion
    {

        private Operacion condicion;
        private LinkedList<Instruccion> sentencias;

        public InstruccionElseIf(Operacion condicion, LinkedList<Instruccion> sentencias)
        {
            this.condicion = condicion;
            this.sentencias = sentencias;
        }

        public Boolean cond(TablaSimbolos ts)
        {
            return (Boolean)condicion.ejecutar(ts);
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            if (condicion.ejecutar(ts) != null)
            {
                if ((Boolean)condicion.ejecutar(ts))
                {
                    if (sentencias != null)
                    {
                        foreach (Instruccion inst in sentencias)
                        {
                            inst.ejecutar(ts);
                        }
                    }
                }
            }
           return null;
        }
    }
}
