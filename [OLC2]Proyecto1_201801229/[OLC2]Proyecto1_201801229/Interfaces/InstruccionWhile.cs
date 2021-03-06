using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionWhile : Instruccion
    {
        Operacion condicion;
        LinkedList<Instruccion> sentencias;

        public InstruccionWhile(Operacion condicion, LinkedList<Instruccion> sentencias)
        {
            this.condicion = condicion;
            this.sentencias = sentencias;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            try
            {
                Boolean cond = (Boolean)condicion.ejecutar(ts);
                while (cond)
                {
                    if (sentencias != null)
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
                    }
                }
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
            String retornar = "";
            String retornarCondicion = condicion.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
            String[] condicionNot = retornarCondicion.Split("!");
            String inicioWhile = "L" + l;
            l++;
            String verdadero = "", falso = "", falsedad = "";
            retornar += inicioWhile + ":\n";

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
                            if (i == 0)
                            {
                                falso = "L" + l;
                                falsedad = falso;
                                l++;
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
                                            retornar += "if (" + linea.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falso + ";\n";
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
                                            retornar += "if (" + linea.Split("=")[0] + ")goto " + verdadero + ";\ngoto " + falsedad + ";\n";
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
                                    retornar += linea + "\n";
                                }
                            }
                        }

                        falso = "";
                    }
                    conteoNot = 0;
                }
            }
            GeneradorAST.LV = falsedad;
            GeneradorAST.LF = inicioWhile;

            if (sentencias != null)
            {
                retornar += verdadero + ":\n";
                foreach (Instruccion sentencia in sentencias)
                {
                    retornar += sentencia.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                }
                retornar += "goto " + inicioWhile + ";\n";
            }
            retornar += falsedad + ":\n";

            GeneradorAST.LV = tempoV;
            GeneradorAST.LF = tempoF;
            return retornar;
        }
    }
}
