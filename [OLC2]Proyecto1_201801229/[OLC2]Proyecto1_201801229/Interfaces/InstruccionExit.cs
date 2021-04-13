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
    }
}
