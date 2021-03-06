using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionIf:Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> sentencias;
        LinkedList<InstruccionElseIf> listaElseIf;
        InstruccionElse instElse;

        public InstruccionIf(Operacion condicion, LinkedList<Instruccion> sentencias, LinkedList<InstruccionElseIf> listaElseIf, InstruccionElse insElse)
        {
            this.condicion = condicion;
            this.sentencias = sentencias;
            this.listaElseIf = listaElseIf;
            this.instElse = insElse;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            
            if (condicion.ejecutar(ts) != null)
            {
                try
                {
                    if ((Boolean)condicion.ejecutar(ts))
                    {
                        if (sentencias != null)
                        {
                            foreach (Instruccion inst in sentencias)
                            {
                                inst.ejecutar(ts);
                            }
                        }
                    }
                    else
                    {
                        bool es = true;
                        if (listaElseIf != null)
                        {
                            foreach (InstruccionElseIf ei in listaElseIf)
                            {
                                Boolean validar = ei.cond(ts);
                                if (validar)
                                {
                                    es = false;
                                    ei.ejecutar(ts);
                                    break;
                                }
                            }
                        }
                        if (instElse != null)
                        {
                            if (es)
                            {
                                instElse.ejecutar(ts);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    GeneradorAST.listaErrores.AddLast(new Error("Condicion no retorna un valor boolean", Error.TipoError.SEMANTICO, 0, 0));
                }
               
            }
            else
            {
                GeneradorAST.listaErrores.AddLast(new Error("Condicion no retorna un valor", Error.TipoError.SEMANTICO, 0, 0));
            }
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String retornar = "";
            String retornarCondicion = condicion.traduccionCondicion(stack,heap,temporales,ref sp, ref hp, ref t ,ref l).ToString();
            String[] condicionNot = retornarCondicion.Split("@");
            String finalif = "L" + l;
            l++;
            String verdadero = "", falso = "", falsedad = "";
            int conteoNot = 0;
            for (int k=0; k<condicionNot.Length;k++)
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
                            retornar += verdadero + ":\n";
                        }
                        
                        verdadero = "L" + l;
                        l++;
                        String condAnd = condicionAnd[i];
                        String[] condicionOr = condAnd.Split("||");
                        for (int j = 0; j < condicionOr.Length; j++)
                        {
                            if (!falso.Equals(""))
                            {
                                retornar += falso + ":\n";
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
                                            retornar += "if (" + linea + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                        }
                                        else
                                        {
                                            String lin = linea;
                                            if (linea.StartsWith("T"))
                                            {
                                                retornar += linea + "\n";
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
                                            retornar += "if (" + lin.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                        }

                                    }
                                    else
                                    {
                                        if ((linea.Contains("==") || linea.Contains(">=") || linea.Contains("<=") || linea.Contains("!=") || linea.Contains(">") || linea.Contains("<")))
                                        {
                                            retornar += "if (" + linea + ")goto " + verdadero + ";\ngoto " + falsedad + ";\n";
                                        }
                                        else
                                        {
                                            String lin = linea;
                                            if (linea.StartsWith("T"))
                                            {
                                                retornar += linea + "\n";
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

                                            retornar += "if (" + lin.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falsedad + ";\n";
                                        }
                                        if (!(conteoNot%2==0) && i == condicionAnd.Length-1)
                                        {
                                            String tempV = verdadero;
                                            verdadero = falsedad;
                                            falsedad = tempV;
                                        }
                                    }
                                }
                                else
                                {
                                    retornar += linea + "\n";
                                }
                            }
                        }

                        falso = "";
                    }
                    conteoNot = 0;
                }
            }

            if (sentencias!=null)
            {
                retornar += verdadero + ":\n";
                foreach (Instruccion sentencia in sentencias)
                {
                    retornar += sentencia.traduccion(stack,heap,temporales,ref sp,ref hp, ref t, ref l).ToString();
                }
                retornar += "goto " + finalif + ";\n";
            }
            retornar += falsedad + ":\n";
            if (instElse!=null)
            {
                retornar += instElse.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
            }
            retornar += finalif + ":\n";
            return retornar;
        }
    }
}
