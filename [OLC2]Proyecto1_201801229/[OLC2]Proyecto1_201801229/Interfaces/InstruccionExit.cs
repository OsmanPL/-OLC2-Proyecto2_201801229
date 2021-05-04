using _OLC2_Proyecto1_201801229.Analizador;
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
            if (GeneradorAST.funcionActual!=null)
            {
                Elemento_Stack elementoStack = stack.buscarElementoStack(GeneradorAST.funcionActual.Id);
                if (elementoStack.Tipo == Simbolo.TipoDato.STRING)
                {
                    String valores = retornar.traduccion(stack,heap,temporales,ref sp, ref hp, ref t, ref l).ToString();
                    String cadena = "";
                    int refH = hp;
                    for (int i = 0; i < valores.Length; i++)
                    {
                        char c = valores[i];
                        cadena += "Heap[(int)HP]=" + (int)c + ";\n";
                        heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                        cadena += "HP=HP+1;\n";
                        hp++;
                    }
                    cadena += "Heap[(int)HP]=-1;\n";
                    heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                    cadena += "HP=HP+1;\n";
                    hp++;
                    cadena += "Stack[" + elementoStack.ReferenciaStack + "]=" + refH + ";\n";
                    elementoStack.ReferenciaHeap = refH;
                    cadena += "goto Retornar" + GeneradorAST.funcionActual.Id.ToLower() + ";\n";
                    return cadena;
                }
                else
                {
                    String cadena = "";
                    String valores = retornar.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valores.Contains("T"))
                    {
                        cadena += valores;
                    }
                    cadena += "Stack[" + elementoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                    cadena += "goto Retornar" + GeneradorAST.funcionActual.Id.ToLower() + ";\n";
                    return cadena;
                }
            }
            return null;
        }
    }
}
