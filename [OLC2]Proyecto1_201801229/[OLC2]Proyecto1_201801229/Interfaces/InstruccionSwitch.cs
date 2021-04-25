using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionSwitch:Instruccion
    {
        Operacion condicion;
        LinkedList<InstruccionCase> listaCase;
        InstruccionElse instElse;

        public InstruccionSwitch(Operacion condicion, LinkedList<InstruccionCase> listaCase, InstruccionElse instElse)
        {
            this.condicion = condicion;
            this.listaCase = listaCase;
            this.instElse = instElse;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            Object valor = condicion.ejecutar(ts);
            if (valor != null)
            {
                bool ejelse = true;
                if (listaCase != null)
                {
                    foreach (InstruccionCase cas in listaCase)
                    {

                        bool iguales = cas.Iguales(valor, ts);
                        if (iguales)
                        {
                            ejelse = false;
                            cas.ejecutar(ts);
                            break;
                        }
                    }
                }
                if (instElse != null)
                {
                    if (ejelse)
                    {
                        instElse.ejecutar(ts);
                    }
                }
            }
            else
            {
                GeneradorAST.listaErrores.AddLast(new Error("Valor del switch retorna null", Error.TipoError.SEMANTICO, 0, 0));
            }
            
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String retornar = "";
            String cond = condicion.traduccion(stack,heap,temporales,ref sp, ref hp, ref t, ref l).ToString();
            retornar += cond + "switch((int)"+cond.Split("\n")[cond.Split("\n").Length-2].Split("=")[0].Split(";")[0]+"){\n";
            if (listaCase != null)
            {
                foreach (InstruccionCase ic in listaCase)
                {
                    retornar+= ic.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString(); 
                }
            }
            if (instElse!=null)
            {
                retornar += "default:\n";
                retornar += instElse.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
            }
            retornar += "}\n";
            return retornar;
        }
    }
}
