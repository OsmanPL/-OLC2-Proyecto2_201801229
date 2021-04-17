﻿using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionElse:Instruccion
    {
        LinkedList<Instruccion> sentencias;

        public InstruccionElse(LinkedList<Instruccion> sentencias)
        {
            this.sentencias = sentencias;
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
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, int sp, int hp, int t)
        {
            return null;
        }
    }
}
