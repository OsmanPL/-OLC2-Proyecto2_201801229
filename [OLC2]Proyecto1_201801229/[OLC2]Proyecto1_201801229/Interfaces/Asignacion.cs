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
            String valores = valor.traduccion(stack,heap,temporales,ref sp, ref hp, ref t, ref l).ToString();
            Elemento_Stack elemntoStack = stack.buscarElementoStack(id);
            if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
            {
                String retornar = "";
                int refH = hp;
                for (int i=0; i<valores.Length;i++)
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
                retornar += "Stack["+elemntoStack.ReferenciaStack+"]=" + refH + ";\n";
                return retornar;
            }
            else
            {
                String retornar = valores + "Stack[" + elemntoStack.ReferenciaStack + "]=" + valores.Split("\n")[valores.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                return retornar;
            }
        }
    }
}
