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
            if (retornar != null)
            {
                return retornar.ejecutar(ts);
            }
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            if (GeneradorAST.funcionActual!=null && retornar!=null)
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
                    String te = "T" + t;
                    temporales.AddLast("T" + t);
                    t++;
                    cadena += te+"=SP+"+elementoStack.ReferenciaStack+";\n";
                    cadena += "Stack[(int)" + te + "]=" + refH + ";\n";
                    elementoStack.ReferenciaHeap = refH;
                    cadena += "goto Retornar" + GeneradorAST.funcionActual.Id.ToLower() + ";\n";
                    return cadena;
                }
                else
                {
                    String cadena = "";
                    String valores = retornar.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();

                    if (elementoStack.Tipo == Simbolo.TipoDato.BOOLEAN)
                    {
                        if (valores.Contains("@")|| valores.Contains("&&")|| valores.Contains("||"))
                        {
                            String retornarCondicion = retornar.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                            String[] condicionNot = retornarCondicion.Split("@");
                            String finalif = "L" + l;
                            l++;
                            String verdadero = "", falso = "", falsedad = "";
                            int conteoNot = 0;
                            for (int k = 0; k < condicionNot.Length; k++)
                            {
                                if (k == 0)
                                {
                                    falsedad = "L" + l;
                                    l++;
                                }
                                if (condicionNot[k].Equals(""))
                                {
                                    conteoNot++;
                                }
                                else
                                {
                                    String[] condicionAnd = condicionNot[k].Split("&&");

                                    for (int i = 0; i < condicionAnd.Length; i++)
                                    {
                                        if (condicionAnd[i].Equals(""))
                                        {
                                            continue;
                                        }
                                        if (!verdadero.Equals(""))
                                        {
                                            cadena += verdadero + ":\n";
                                        }

                                        verdadero = "L" + l;
                                        l++;
                                        String condAnd = condicionAnd[i];
                                        String[] condicionOr = condAnd.Split("||");
                                        for (int j = 0; j < condicionOr.Length; j++)
                                        {
                                            if (!falso.Equals(""))
                                            {
                                                cadena += falso + ":\n";
                                            }

                                            String condOr = condicionOr[j];
                                            String[] lineasOR = condOr.Split("\n");
                                            foreach (String linea in lineasOR)
                                            {
                                                if (linea.Equals(lineasOR[lineasOR.Length - 2]))
                                                {
                                                    if (j != condicionOr.Length - 1)
                                                    {
                                                        falso = "L" + l;
                                                        l++;
                                                        if ((linea.Contains("==") || linea.Contains(">=") || linea.Contains("<=") || linea.Contains("!=") || linea.Contains(">") || linea.Contains("<")))
                                                        {
                                                            cadena += "if (" + linea + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                                        }
                                                        else
                                                        {
                                                            String lin = linea;
                                                            if (linea.StartsWith("T"))
                                                            {
                                                                cadena += linea + "\n";
                                                            }
                                                            else
                                                            {
                                                                if (conteoNot % 2 == 0)
                                                                {
                                                                    if (linea.Equals("0"))
                                                                    {
                                                                        lin = "1";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (linea.Equals("1"))
                                                                    {
                                                                        lin = "0";
                                                                    }
                                                                }
                                                            }
                                                            cadena += "if (" + lin.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if ((linea.Contains("==") || linea.Contains(">=") || linea.Contains("<=") || linea.Contains("!=") || linea.Contains(">") || linea.Contains("<")))
                                                        {
                                                            cadena += "if (" + linea + ")goto " + verdadero + ";\ngoto " + falsedad + ";\n";
                                                        }
                                                        else
                                                        {
                                                            String lin = linea;
                                                            if (linea.StartsWith("T"))
                                                            {
                                                                cadena += linea + "\n";
                                                            }
                                                            else
                                                            {
                                                                if (conteoNot % 2 == 0)
                                                                {
                                                                    if (linea.Equals("0"))
                                                                    {
                                                                        lin = "1";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (linea.Equals("1"))
                                                                    {
                                                                        lin = "0";
                                                                    }
                                                                }
                                                            }

                                                            cadena += "if (" + lin.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falsedad + ";\n";
                                                        }
                                                        if (!(conteoNot % 2 == 0) && i == condicionAnd.Length - 1)
                                                        {
                                                            String tempV = verdadero;
                                                            verdadero = falsedad;
                                                            falsedad = tempV;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    cadena += linea + "\n";
                                                }
                                            }
                                        }

                                        falso = "";
                                    }
                                    conteoNot = 0;
                                }
                            }


                            cadena += verdadero + ":\n";
                            cadena += "T" + t + "=SP+" + elementoStack.ReferenciaStack + ";\n";
                            cadena += "Stack[(int)T" + t + "]=1 ;\n";
                            temporales.AddLast("T" + t);
                            t++;
                            cadena += "goto " + finalif + ";\n";
                            cadena += falsedad + ":\n";

                            cadena += "T" + t + "=SP+" + elementoStack.ReferenciaStack + ";\n";
                            cadena += "Stack[(int)T" + t + "]=0 ;\n";
                            cadena += finalif + ":\n";
                            return cadena;
                        }
                        else
                        {
                            if (valores.Contains("T"))
                            {
                                cadena += valores;
                            }
                            String te = "T" + t;
                            temporales.AddLast("T" + t);
                            t++;
                            cadena += te + "=SP+" + elementoStack.ReferenciaStack + ";\n";
                            cadena += "Stack[(int)" + te + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                            cadena += "goto Retornar" + GeneradorAST.funcionActual.Id.ToLower() + ";\n";
                            return cadena;
                        }
                    }
                    else
                    {
                        if (valores.Contains("T"))
                        {
                            cadena += valores;
                        }
                        String te = "T" + t;
                        temporales.AddLast("T" + t);
                        t++;
                        cadena += te + "=SP+" + elementoStack.ReferenciaStack + ";\n";
                        cadena += "Stack[(int)" + te + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                        cadena += "goto Retornar" + GeneradorAST.funcionActual.Id.ToLower() + ";\n";
                    }
                    return cadena;
                }
            }
            else if (GeneradorAST.procedimientoActual != null)
            {
                String cadena = "";
                cadena += "goto Retornar" + GeneradorAST.procedimientoActual.Id.ToLower() + ";\n";
                return cadena;
            }
            return ""; 
        }
    }
}
