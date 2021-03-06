using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Asignacion : Instruccion
    {
        String id;
        Operacion valor;
        String id_campo;
        Operacion posicion;

        public Asignacion(String id, Operacion valor)
        {
            this.id = id;
            this.valor = valor;
            this.posicion = null;
            this.id_campo = "";
        }
        public Asignacion(String id, Operacion valor, String id_campo)
        {
            this.id = id;
            this.valor = valor;
            this.id_campo = id_campo;
            this.posicion = null;
        }
        public Asignacion(String id, Operacion valor, Operacion posicion)
        {
            this.id = id;
            this.valor = valor;
            this.posicion = posicion;
            this.id_campo = "";
        }
        public Object eje(TablaSimbolos ts, String fu)
        {
            if (id.ToLower().Equals(fu.ToLower()))
            {
                return valor.ejecutar(ts);
            }
            return null;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            if (posicion != null)
            {
                try
                {
                    int posi = int.Parse(posicion.ejecutar(ts).ToString());
                    Simbolo sim = ts.getSimbolo(id);
                    ArrayPascal arr = (ArrayPascal)sim.Valor;
                    int pos = posi - int.Parse(arr.LimInferior.ejecutar(ts).ToString());
                    Object val = valor.ejecutar(ts);
                    try
                    {
                        switch (arr.Tipo)
                        {
                            case Simbolo.TipoDato.BOOLEAN:
                                arr.Arreglo[pos] = Boolean.Parse(val.ToString());
                                ts.setValor(id, arr);
                                break;
                            case Simbolo.TipoDato.OBJECT:
                                arr.Arreglo[pos] = val;
                                ts.setValor(id, arr);
                                break;
                            case Simbolo.TipoDato.INTEGER:
                                arr.Arreglo[pos] = int.Parse(val.ToString());
                                ts.setValor(id, arr);
                                break;
                            case Simbolo.TipoDato.REAL:
                                arr.Arreglo[pos] = Double.Parse(val.ToString());
                                ts.setValor(id, arr);
                                break;
                            case Simbolo.TipoDato.STRING:
                                if (valor.Tipo == Operacion.Tipo_operacion.CADENA || valor.Tipo == Operacion.Tipo_operacion.CONCAT)
                                {
                                    arr.Arreglo[pos] = val.ToString();
                                    ts.setValor(id, arr);
                                }
                                else
                                {
                                    GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " esperaba un valor de tipo " + sim.Tipo.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                                }
                                break;
                            case Simbolo.TipoDato.IDENTIFICADOR:
                                arr.Arreglo[pos] = val;
                                ts.setValor(id, arr);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        GeneradorAST.listaErrores.AddLast(new Error("El arreglo no es del mismo tipo de dato ", Error.TipoError.SEMANTICO, 0, 0));
                    }
                }
                catch (Exception e)
                {
                    GeneradorAST.listaErrores.AddLast(new Error("No es de tipo arreglo ", Error.TipoError.SEMANTICO, 0, 0));
                }


            }
            else if (!id_campo.Equals(""))
            {

            }
            else
            {
                Object val = valor.ejecutar(ts);
                Simbolo sim = ts.getSimbolo(id);
                if (sim != null && val != null)
                {
                    if (sim.TipoVar == Simbolo.TipoVarariable.VAR)
                    {
                        try
                        {
                            switch (sim.Tipo)
                            {
                                case Simbolo.TipoDato.BOOLEAN:
                                    ts.setValor(id, Boolean.Parse(val.ToString()));
                                    break;
                                case Simbolo.TipoDato.OBJECT:
                                    ts.setValor(id, val);
                                    break;
                                case Simbolo.TipoDato.INTEGER:
                                    ts.setValor(id, int.Parse(val.ToString()));
                                    break;
                                case Simbolo.TipoDato.REAL:
                                    ts.setValor(id, Double.Parse(val.ToString()));
                                    break;
                                case Simbolo.TipoDato.STRING:
                                    if (valor.Tipo == Operacion.Tipo_operacion.CADENA || valor.Tipo == Operacion.Tipo_operacion.CONCAT)
                                    {
                                        ts.setValor(id, val.ToString());
                                    }
                                    else
                                    {
                                        GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " esperaba un valor de tipo " + sim.Tipo.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                                    }
                                    break;
                                case Simbolo.TipoDato.IDENTIFICADOR:
                                    ts.setValor(id, val);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " esperaba un valor de tipo " + sim.Tipo.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                        }
                    }
                    else
                    {
                        GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " es una constante", Error.TipoError.SEMANTICO, 0, 0));
                    }

                }
                else
                {
                    if (val == null)
                    {
                        GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " valor nulo", Error.TipoError.SEMANTICO, 0, 0));
                    }
                    if (sim == null)
                    {
                        GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " no existe", Error.TipoError.SEMANTICO, 0, 0));
                    }


                }
            }
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String valores = valor.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
            if (GeneradorAST.funcionActual != null || GeneradorAST.procedimientoActual != null)
            {
                Elemento_Stack elemntoStack = stack.buscarElementoStack(id);
                if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
                {
                    String retornar = "";
                    String te = "T" + t + "";
                    temporales.AddLast("T" + t);
                    t++;
                    retornar += te + "=HP;\n";
                    int refH = hp;
                    for (int i = 0; i < valores.Length; i++)
                    {
                        char c = valores[i];
                        retornar += "Heap[(int)HP]=" + (int)c + ";\n";
                        heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                        retornar += "HP=HP+1;\n";
                        hp++;
                    }
                    retornar += "Heap[(int)HP]=-1;\n";
                    heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                    retornar += "HP=HP+1;\n";
                    hp++;
                    retornar += "T" + t + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                    retornar += "Stack[(int)T" + t + "]=" + te + ";\n";
                    temporales.AddLast("T" + t);
                    t++;
                    elemntoStack.ReferenciaHeap = refH;
                    return retornar;
                }
                else
                {
                    String retornar = "";

                    if (elemntoStack.Fu)
                    {
                        if (elemntoStack.Tipo == Simbolo.TipoDato.BOOLEAN)
                        {
                            if (valores.Contains("!") || valores.Contains("&&") || valores.Contains("||"))
                            {
                                String retornarCondicion = valor.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
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


                                retornar += verdadero + ":\n";
                                retornar += "T" + t + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                                retornar += "Stack[(int)T" + t + "]=1 ;\n";
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += "goto " + finalif + ";\n";
                                retornar += falsedad + ":\n";

                                retornar += "T" + t + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                                retornar += "Stack[(int)T" + t + "]=0 ;\n";
                                retornar += finalif + ":\n";
                            }
                            else
                            {
                                if (valores.Contains("T"))
                                {
                                    retornar += valores;
                                }
                                retornar += "T" + t + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                                retornar += "Stack[(int)T" + t + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                        }
                        else
                        {
                            if (valores.Contains("T"))
                            {
                                retornar += valores;
                            }
                            retornar += "T" + t + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                            retornar += "Stack[(int)T" + t + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                            temporales.AddLast("T" + t);
                            t++;
                        }
                    }
                    else
                    {
                        if (elemntoStack.Tipo == Simbolo.TipoDato.BOOLEAN)
                        {
                            if (valores.Contains("!") || valores.Contains("&&") || valores.Contains("||"))
                            {
                                String retornarCondicion = valor.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
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


                                retornar += verdadero + ":\n";
                                retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=1;\n";
                                retornar += "goto " + finalif + ";\n";
                                retornar += falsedad + ":\n";

                                retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=0;\n";

                                retornar += finalif + ":\n";
                            }
                            else
                            {
                                if (valores.Contains("T"))
                                {
                                    retornar += valores;
                                }
                                retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                            }
                        }
                        else
                        {
                            if (valores.Contains("T"))
                            {
                                retornar += valores;
                            }
                            retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                        }

                    }

                    return retornar;
                }
            }
            else
            {
                Elemento_Stack elemntoStack = stack.buscarElementoStack(id);
                if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
                {
                    String retornar = "";
                    int refH = hp;
                    if (valores.Contains("T"))
                    {
                        if (valores.Contains("T"))
                        {
                            retornar += valores;
                        }
                        retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                        return retornar;
                    }
                    else
                    {
                        String te = "T" + t + "";
                        temporales.AddLast("T" + t);
                        t++;
                        retornar += te + "=HP;\n";
                        for (int i = 0; i < valores.Length; i++)
                        {
                            char c = valores[i];
                            retornar += "Heap[(int)HP]=" + (int)c + ";\n";
                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                            retornar += "HP=HP+1;\n";
                            hp++;
                        }
                        retornar += "Heap[(int)HP]=-1;\n";
                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                        retornar += "HP=HP+1;\n";
                        hp++;
                        retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=" + te + ";\n";
                        elemntoStack.ReferenciaHeap = refH;
                        return retornar;
                    }

                }
                else
                {

                    String retornar = "";
                    if (elemntoStack.Tipo == Simbolo.TipoDato.BOOLEAN)
                    {
                        if (valores.Contains("!") || valores.Contains("&&") || valores.Contains("||"))
                        {
                            String retornarCondicion = valor.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
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


                            retornar += verdadero + ":\n";
                            retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=1;\n";
                            retornar += "goto " + finalif + ";\n";
                            retornar += falsedad + ":\n";

                            retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=0;\n";

                            retornar += finalif + ":\n";
                        }
                        else
                        {
                            if (valores.Contains("T"))
                            {
                                retornar += valores;
                            }
                            retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                        }
                    }
                    else
                    {
                        if (valores.Contains("T"))
                        {
                            retornar += valores;
                        }
                        retornar += "Stack[" + elemntoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    }
                    return retornar;
                }
            }

        }
    }
}
