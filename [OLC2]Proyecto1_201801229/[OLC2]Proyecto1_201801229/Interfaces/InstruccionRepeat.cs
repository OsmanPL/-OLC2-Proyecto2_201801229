using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionRepeat:Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> sentencias;

        public InstruccionRepeat(Operacion condicion, LinkedList<Instruccion> sentencias)
        {
            this.condicion = condicion;
            this.sentencias = sentencias;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            try
            {
                Boolean cond = true;
                do
                {
                    foreach (Instruccion inst in sentencias)
                    {
                        if (inst.GetType() == typeof(InstruccionBreak))
                        {
                            return null;
                        }
                        else if (inst.GetType() == typeof(InstruccionContinue))
                        {
                            continue;
                        }
                        inst.ejecutar(ts);
                    }
                    cond = (Boolean)condicion.ejecutar(ts);
                } while (!cond);
            }
            catch (Exception e)
            {
                GeneradorAST.listaErrores.AddLast(new Error("Condicion no retorna un valor boolean", Error.TipoError.SEMANTICO, 0, 0));
            }
            
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String retornar = "";
            String retornarCondicion = condicion.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
            String[] condicionAnd = retornarCondicion.Split("&&");
            String inicioWhile = "L" + l;
            l++;
            String verdadero = "", falso = "", falsedad = "";
            retornar += inicioWhile + ":\n";
            if (sentencias != null)
            {
                foreach (Instruccion sentencia in sentencias)
                {
                    retornar += sentencia.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                }
            }
           
            for (int i = 0; i < condicionAnd.Length; i++)
            {
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
                                    String lin = linea;
                                    if (linea.Contains("=="))
                                    {
                                        lin = linea.Replace("==", "!=");
                                    }
                                    else if (linea.Contains(">="))
                                    {
                                        lin = linea.Replace(">=", "<");
                                    }
                                    else if (linea.Contains("<="))
                                    {
                                        lin = linea.Replace("<=", ">");
                                    }
                                    else if (linea.Contains("!="))
                                    {
                                        lin = linea.Replace("!=", "==");
                                    }
                                    else if (linea.Contains(">"))
                                    {
                                        lin = linea.Replace(">", "<=");
                                    }
                                    else if (linea.Contains("<"))
                                    {
                                        lin = linea.Replace("<", ">=");
                                    }
                                    retornar += "if (" + lin.TrimStart('!') + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                }
                                else
                                {
                                    retornar += linea.TrimStart('!') + "\n";
                                    retornar += "if (" + linea.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                }
                            }
                            else
                            {
                                if ((linea.Contains("==") || linea.Contains(">=") || linea.Contains("<=") || linea.Contains("!=") || linea.Contains(">") || linea.Contains("<")))
                                {
                                    if (linea.StartsWith("!"))
                                    {
                                        String lin = linea;
                                        if (linea.Contains("=="))
                                        {
                                            lin = linea.Replace("==", "!=");
                                        }
                                        else if (linea.Contains(">="))
                                        {
                                            lin = linea.Replace(">=", "<");
                                        }
                                        else if (linea.Contains("<="))
                                        {
                                            lin = linea.Replace("<=", ">");
                                        }
                                        else if (linea.Contains("!="))
                                        {
                                            lin = linea.Replace("!=", "==");
                                        }
                                        else if (linea.Contains(">"))
                                        {
                                            lin = linea.Replace(">", "<=");
                                        }
                                        else if (linea.Contains("<"))
                                        {
                                            lin = linea.Replace("<", ">=");
                                        }
                                        retornar += "if (" + lin.TrimStart('!') + ")goto " + verdadero + ";\ngoto " + inicioWhile + ";\n";
                                    }
                                    else
                                    {
                                        retornar += "if (" + linea + ")goto " + verdadero + ";\ngoto " + inicioWhile + ";\n";
                                    }
                                }
                                else
                                {
                                    retornar += linea.TrimStart('!') + "\n";
                                    retornar += "if (" + linea.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + inicioWhile + ";\n";
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

            retornar += verdadero + ":\n";

            return retornar;
        }
    }
}
