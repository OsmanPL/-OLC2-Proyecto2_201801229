using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
