using _OLC2_Proyecto1_201801229.Analizador;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionCase:Instruccion
    {
        LinkedList<Operacion> condicion;
        LinkedList<Instruccion> sentencias;

        public InstruccionCase(LinkedList<Operacion> condicion, LinkedList<Instruccion> sentencias)
        {
            this.condicion = condicion;
            this.sentencias = sentencias;
        }
        public bool Iguales(Object val, TablaSimbolos ts)
        {
            if (condicion != null)
            {
                foreach (Operacion op in condicion)
                {
                    Object va = op.ejecutar(ts);
                    if (va!=null)
                    {
                        if (val.Equals(va))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        GeneradorAST.listaErrores.AddLast(new Error("Valor del case retorna null", Error.TipoError.SEMANTICO, 0, 0));
                    }
                   
                }
            }
            return false;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            if (sentencias!=null)
            {
                foreach (Instruccion inst in sentencias)
                {
                    inst.ejecutar(ts);
                }
            }
            return null;
        }
    }
}
