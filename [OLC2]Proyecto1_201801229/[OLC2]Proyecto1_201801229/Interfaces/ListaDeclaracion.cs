using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class ListaDeclaracion:Instruccion
    {
        LinkedList<Declaracion> declaraciones;

        public ListaDeclaracion(LinkedList<Declaracion> declaraciones)
        {
            this.declaraciones = declaraciones;
        }

        public Object ejecutar(TablaSimbolos ts)
        {
            if (declaraciones!=null)
            {
                foreach (Declaracion decla in declaraciones)
                {
                    decla.ejecutar(ts);
                }
            }
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String retornar = "";
            if (declaraciones != null)
            {
                foreach (Declaracion decla in declaraciones)
                {
                    retornar+=decla.traduccion(stack,heap,temporales,ref sp,ref hp,ref t,ref l).ToString();
                }
            }

            return retornar;
        }
    }
}
