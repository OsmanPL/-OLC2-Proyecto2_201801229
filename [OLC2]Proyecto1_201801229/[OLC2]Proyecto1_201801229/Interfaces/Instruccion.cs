using System;
using System.Collections.Generic;
using System.Text;
using _OLC2_Proyecto1_201801229.Estructuras;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    interface Instruccion
    {
        Object ejecutar(TablaSimbolos ts);
        Object traduccion(Estructura_Stack stack, Estructura_Heap heap,LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l);
    }
}
