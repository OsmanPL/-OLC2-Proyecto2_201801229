using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
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
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            string retornar = "";
            if (condicion!=null)
            {
                foreach (Operacion op in condicion)
                {
                    retornar += "case "+op.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString().Split("\n")[0]+":\n";
                    if (sentencias != null)
                    {
                        foreach (Instruccion inst in sentencias)
                        {
                            retornar+=inst.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                        }
                    }
                    retornar += "break;\n";
                }
            }
            return retornar;
        }
    }
}
