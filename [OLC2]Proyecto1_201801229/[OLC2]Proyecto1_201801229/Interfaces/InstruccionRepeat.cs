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
            String tempoV = GeneradorAST.LV, tempoF = GeneradorAST.LF;
            String retornar = "",retornar1="";
            String retornarCondicion = condicion.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
            
            String inicioWhile = "L" + l;
            l++;
            String verdadero = "", falso = "";
            retornar += inicioWhile + ":\n";


            String[] condicionNot = retornarCondicion.Split("!");

            int conteoNot = 0;
            for (int k = 0; k < condicionNot.Length; k++)
            {
                if (condicionNot[k].Equals(""))
                {
                    conteoNot++;
                }
                else
                {
                    String[] condicionAnd = condicionNot[k].Split("&&");
                    for (int i = 0; i < condicionAnd.Length; i++)
                    {
                        if (!verdadero.Equals(""))
                        {
                            retornar1 += verdadero + ":\n";
                        }
                        verdadero = "L" + l;
                        l++;
                        String condAnd = condicionAnd[i];
                        String[] condicionOr = condAnd.Split("||");
                        for (int j = 0; j < condicionOr.Length; j++)
                        {
                            if (!falso.Equals(""))
                            {
                                retornar1 += falso + ":\n";
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

                                            retornar1 += "if (" + linea + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                        }
                                        else
                                        {
                                            retornar1 += "if (" + linea.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
                                        }
                                    }
                                    else
                                    {
                                        String iniWhile = inicioWhile;
                                        String tempV = verdadero;
                                        if (!(conteoNot % 2 == 0) && i == condicionAnd.Length - 1)
                                        {
                                            tempV = inicioWhile;
                                            iniWhile = verdadero;
                                        }
                                        if ((linea.Contains("==") || linea.Contains(">=") || linea.Contains("<=") || linea.Contains("!=") || linea.Contains(">") || linea.Contains("<")))
                                        {
                                            retornar1 += "if (" + linea + ")goto " + tempV + ";\ngoto " + iniWhile + ";\n";
                                        }
                                        else
                                        {
                                            retornar1 += "if (" + linea.Split("=")[0] + ")goto " + tempV + ";\ngoto " + iniWhile + ";\n";
                                        }
                                    }
                                }
                                else
                                {
                                    retornar1 += linea + "\n";
                                }
                            }
                        }

                        falso = "";
                    }
                    conteoNot = 0;
                }
            }

            

            retornar1 += verdadero + ":\n";
            GeneradorAST.LV = verdadero;
            GeneradorAST.LF = inicioWhile;
            if (sentencias != null)
            {
                foreach (Instruccion sentencia in sentencias)
                {
                    retornar += sentencia.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                }
            }
            retornar += retornar1;
            GeneradorAST.LV = tempoV;
            GeneradorAST.LF = tempoF;
            return retornar;
        }
    }
}
