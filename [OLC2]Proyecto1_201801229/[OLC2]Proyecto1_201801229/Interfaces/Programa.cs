using _OLC2_Proyecto1_201801229.Estructuras;
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
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String encabezado = "";
            String funciones = "";
            String main = "";
            MessageBox.Show("" + t);
            encabezado += "#include <stdio.h> \n";
            encabezado += "float Heap[100000];\n";
            encabezado += "float Stack[100000];\n";
            encabezado += "float SP; \n";
            encabezado += "float HP;\n";

            main += "int main(){\n";
            if (sentencias != null)
            {
                foreach (Instruccion inst in sentencias)
                {
                    if (inst != null)
                    {
                        main += inst.traduccion(stack, heap, temporales,ref sp,ref hp,ref t,ref l).ToString();
                    }
                }
            }
            if (instrucciones != null)
            {
                foreach (Instruccion inst in instrucciones)
                {
                    if (inst != null)
                    {
                        main += inst.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    }
                }
            }
            main += "return 0; \n}\n";
            if (temporales.Count >0)
            {
                encabezado += "float ";
                foreach (String temporal in temporales)
                {
                    encabezado += temporal + ",";
                }
                encabezado = encabezado.TrimEnd(',');
                encabezado += ";\n";
            }
            

            return encabezado + "\n" + funciones + "\n" + main;
        }
    }
}
