using _OLC2_Proyecto1_201801229.Analizador;
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
    }
}
