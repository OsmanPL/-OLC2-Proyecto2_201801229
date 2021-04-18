using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionExit:Instruccion
    {
        Operacion retornar;

        public InstruccionExit(Operacion retornar)
        {
            this.retornar = retornar;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            return retornar.ejecutar(ts);
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            return null;
        }
    }
}
