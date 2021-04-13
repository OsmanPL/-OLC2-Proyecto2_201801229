using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using _OLC2_Proyecto1_201801229.Analizador;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Declaracion : Instruccion
    {
        private Object id;
        private Simbolo.TipoDato tipo;
        private Simbolo.TipoVarariable vc;
        private Operacion valor;
        private String type;
        private bool lista;

        public Declaracion(Object id, Simbolo.TipoDato tipo, Operacion valor,Simbolo.TipoVarariable vc, String type, bool lista)
        {
            this.id = id;
            this.tipo = tipo;
            this.valor = valor;
            this.vc = vc;
            this.type = type;
            this.lista = lista;
        }

        public Object ejecutar(TablaSimbolos ts)
        {
            if (vc == Simbolo.TipoVarariable.CONST)
            {
                bool existe = ts.existe(id.ToString().ToLower());
                if (!existe)
                {
                    Object val = valor.ejecutar(ts);
                    ts.AddLast(new Simbolo(id.ToString(),tipo,val,ts.Entorno,vc));
                }
                else
                {
                    GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " ya fue declarada anteriormente", Error.TipoError.SEMANTICO, 0, 0));
                }
            }
            else
            {
                if (lista)
                {
                    foreach (String ide in (LinkedList<String>)id)
                    {
                        bool existe = ts.existe(ide.ToString().ToLower());
                        if (!existe)
                        {
                            Object val = valor.ejecutar(ts);
                            try
                            {
                                switch (tipo)
                                {
                                    case Simbolo.TipoDato.BOOLEAN:
                                        ts.AddLast(new Simbolo(ide.ToString(), tipo, Boolean.Parse(val.ToString()), ts.Entorno,vc));
                                        break;
                                    case Simbolo.TipoDato.OBJECT:
                                        ts.AddLast(new Simbolo(ide.ToString(), tipo, val, ts.Entorno, vc));
                                        break;
                                    case Simbolo.TipoDato.INTEGER:
                                        ts.AddLast(new Simbolo(ide.ToString(), tipo, int.Parse(val.ToString()), ts.Entorno, vc));
                                        break;
                                    case Simbolo.TipoDato.REAL:
                                        ts.AddLast(new Simbolo(ide.ToString(), tipo, Double.Parse(val.ToString()), ts.Entorno, vc));
                                        break;
                                    case Simbolo.TipoDato.STRING:
                                        ts.AddLast(new Simbolo(ide.ToString(), tipo, val.ToString(), ts.Entorno, vc));
                                        break;
                                    case Simbolo.TipoDato.IDENTIFICADOR:
                                        ts.AddLast(new Simbolo(ide.ToString(),tipo,val,type,ts.Entorno, vc));
                                        break;
                                }
                            }
                            catch (Exception e)
                            {
                                GeneradorAST.listaErrores.AddLast(new Error(ide.ToString() + " esperaba un valor de tipo "+tipo.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                            }

                        }
                        else
                        {
                            GeneradorAST.listaErrores.AddLast(new Error(ide.ToString() +" ya fue declarada anteriormente",Error.TipoError.SEMANTICO,0,0)) ;
                        }
                    }
                }
                else
                {
                    bool existe = ts.existe(id.ToString().ToLower());
                    if (!existe)
                    {
                        Object val = valor.ejecutar(ts);
                        try
                        {
                            switch (tipo)
                            {
                                case Simbolo.TipoDato.BOOLEAN:
                                    ts.AddLast(new Simbolo(id.ToString(), tipo, ((Boolean)val), ts.Entorno, vc));
                                    break;
                                case Simbolo.TipoDato.OBJECT:
                                    ts.AddLast(new Simbolo(id.ToString(), tipo, val, ts.Entorno, vc));
                                    break;
                                case Simbolo.TipoDato.INTEGER:
                                    ts.AddLast(new Simbolo(id.ToString(), tipo, int.Parse(val.ToString()), ts.Entorno, vc));
                                    break;
                                case Simbolo.TipoDato.REAL:
                                    ts.AddLast(new Simbolo(id.ToString(), tipo, Double.Parse(val.ToString()), ts.Entorno, vc));
                                    break;
                                case Simbolo.TipoDato.STRING:
                                    if (valor.Tipo == Operacion.Tipo_operacion.CONCAT || valor.Tipo == Operacion.Tipo_operacion.CADENA)
                                    {
                                        ts.AddLast(new Simbolo(id.ToString(), tipo, val.ToString(), ts.Entorno, vc));
                                    }
                                    else
                                    {
                                        GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " esperaba un valor de tipo " + tipo.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                                    }
                                    break;
                                case Simbolo.TipoDato.IDENTIFICADOR:
                                    ts.AddLast(new Simbolo(id.ToString(), tipo, val, type, ts.Entorno, vc));
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + " esperaba un valor de tipo " + tipo.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                        }
                        
                    }
                    else
                    {
                        GeneradorAST.listaErrores.AddLast(new Error(id.ToString() + "  ya fue declarada anteriormente", Error.TipoError.SEMANTICO, 0, 0));
                    }
                }
            }
            
            return null;
        }
    }
}
