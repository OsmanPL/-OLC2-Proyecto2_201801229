using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Linq;

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
            encabezado += "#include <stdio.h> \n";
            encabezado += "float Heap[100000];\n";
            encabezado += "float Stack[100000];\n";
            encabezado += "float SP; \n";
            encabezado += "float HP;\n";


            if (GeneradorAST.funcionesYprocedimientos != null)
            {
                foreach (Instruccion inst in GeneradorAST.funcionesYprocedimientos)
                {
                    if (inst.GetType() == typeof(Funcion))
                    {
                        Funcion func = (Funcion)inst;
                        int pos = func.Parametros!=null?func.Parametros.Count:0;
                        stack.agregarStack(new Elemento_Stack(func.Id,func.Retorno,pos,0,null,false));
                    }
                }
            }

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


            funciones += "void printString(){\n";
            String temp = "T" + t;
            funciones += "T" + t + "=Stack[(int)SP];\n";
            temporales.AddLast("T" + t);
            t++;
            funciones += "T" + t + "=Heap[(int)" + temp + "];\n";
            String temp2 = "T" + t;
            temporales.AddLast("T" + t);
            t++;
            funciones += "LoopImprimir:\n";
            funciones += "printf(\"%c\",(char)" + temp2 + ");\n";
            funciones += temp + "=" + temp + "+1;\n";
            funciones += temp2 + "=Heap[(int)" + temp + "];\n";
            funciones += "if(" + temp2 + "!= -1) goto LoopImprimir;\nreturn;\n";
            funciones += "}\n\n";

            funciones += "void printBool(){\n";
            funciones += "T" + t + "= Stack[(int)SP];\n";
            String tempo = "T" + t;
            temporales.AddLast("T" + t);
            String etiqueta1 = "L" + l;
            l++;
            String etiqueta2 = "L" + l;
            l++;
            String etiqueta3 = "L" + l;
            l++;
            funciones += "if(" + tempo + "==0)goto " + etiqueta1 + ";\ngoto " + etiqueta2 + ";\n";
            funciones += etiqueta1 + ":\n";
            funciones += "printf(\"%c\",(char)70);\n";
            funciones += "printf(\"%c\",(char)65);\n";
            funciones += "printf(\"%c\",(char)76);\n";
            funciones += "printf(\"%c\",(char)83);\n";
            funciones += "printf(\"%c\",(char)69);\ngoto " + etiqueta3 + ";\n";
            funciones += etiqueta2 + ":\n";
            funciones += "printf(\"%c\",(char)84);\n";
            funciones += "printf(\"%c\",(char)82);\n";
            funciones += "printf(\"%c\",(char)85);\n";
            funciones += "printf(\"%c\",(char)69);\n" + etiqueta3 + ":\nreturn;\n";
            funciones += "}\n";

            LinkedList<String> funcionesAnidadas = new LinkedList<string>();

            if (GeneradorAST.funcionesYprocedimientos != null)
            {

                for (int i =GeneradorAST.funcionesYprocedimientos.Count-1;i>=0;i--)
                {
                    Instruccion inst = GeneradorAST.funcionesYprocedimientos.ElementAt(i);
                    if (inst!=null)
                    {
                        funcionesAnidadas.AddLast(inst.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString());
                    }
                }
            }

            for (int i = funcionesAnidadas.Count - 1; i >= 0; i--)
            {
                funciones += funcionesAnidadas.ElementAt(i);
            }

            if (temporales.Count > 0)
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
