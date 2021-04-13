using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Programa : Instruccion
    {
        public static RichTextBox consola = new RichTextBox();

        LinkedList<Instruccion> sentencias;
        LinkedList<Instruccion> instrucciones;
        public Programa(LinkedList<Instruccion> sentencias , LinkedList<Instruccion> instrucciones)
        {
            this.sentencias = sentencias;
            this.instrucciones = instrucciones;
        }


        public Object ejecutar(TablaSimbolos ts)
        {
            if (sentencias != null)
            {
                foreach (Instruccion inst in sentencias)
                {
                    if (inst != null)
                    {

                        inst.ejecutar(ts);
                    }
                }
            }
            if(instrucciones != null)
            {
                foreach (Instruccion inst in instrucciones)
                {
                    if (inst != null)
                    {

                        inst.ejecutar(ts);
                    }
                }
            }
            return null;
        }
    }
}
