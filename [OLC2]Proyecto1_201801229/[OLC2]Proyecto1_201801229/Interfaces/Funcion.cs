﻿using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Funcion:Instruccion
    {
        String id;
        Simbolo.TipoDato retorno;
        String type;
        LinkedList<ParametroFP> parametros;
        LinkedList<Instruccion> instrucciones;
        LinkedList<Instruccion> sentencias;
        Object valorRetorno;
        bool exit = false;
        public Funcion(String id, Simbolo.TipoDato retorno, Object valorRetorno,LinkedList<ParametroFP> parametros, LinkedList<Instruccion> instrucciones,LinkedList<Instruccion> sentencias, String type)
        {
            this.Parametros = parametros;
            this.Id = id;
            this.Retorno = retorno;
            this.Instrucciones = instrucciones;
            this.Sentencias = sentencias;
            this.Type = type;
            this.valorRetorno = valorRetorno;
        }
        public Funcion()
        {

        }
        public string Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public LinkedList<ParametroFP> Parametros { get => parametros; set => parametros = value; }
        internal Simbolo.TipoDato Retorno { get => retorno; set => retorno = value; }
        internal LinkedList<Instruccion> Instrucciones { get => instrucciones; set => instrucciones = value; }
        internal LinkedList<Instruccion> Sentencias { get => sentencias; set => sentencias = value; }
        public object ValorRetorno { get => valorRetorno; set => valorRetorno = value; }

        public Object ejecutar(TablaSimbolos ts)
        {
            TablaSimbolos tsFuncion = new TablaSimbolos(id);
            if (Parametros != null)
            {
                foreach (ParametroFP d in parametros)
                {
                    tsFuncion.AddLast(new Simbolo(d.Id.ToString(), d.Tipo, d.Valor, id, Simbolo.TipoVarariable.VAR));
                }
            }
            tsFuncion.AddLast(new Simbolo(Id,retorno,valorRetorno,id, Simbolo.TipoVarariable.VAR));
            foreach (Simbolo s in ts)
            {
                if (!tsFuncion.existe(s.Id))
                {
                    tsFuncion.AddLast(s);
                }
            }
            if(instrucciones != null)
            {
                foreach (Instruccion inst in instrucciones)
                {
                    if (inst != null)
                    {

                        inst.ejecutar(tsFuncion);
                    }
                }
            }
            if (sentencias != null)
            {
                foreach (Instruccion inst in sentencias)
                {
                    if (inst.GetType() == typeof(InstruccionExit))
                    {
                        ValorRetorno = inst.ejecutar(tsFuncion);
                        if (ValorRetorno != null)
                        {
                            try
                            {
                                switch (this.Retorno)
                                {
                                    case Simbolo.TipoDato.INTEGER:
                                        return Double.Parse(ValorRetorno.ToString());
                                    case Simbolo.TipoDato.REAL:
                                        return Double.Parse(ValorRetorno.ToString());
                                    case Simbolo.TipoDato.BOOLEAN:
                                        return Boolean.Parse(ValorRetorno.ToString());
                                    case Simbolo.TipoDato.STRING:
                                        return ValorRetorno.ToString();
                                    case Simbolo.TipoDato.OBJECT:
                                        return ValorRetorno;
                                }
                            }
                            catch (Exception e)
                            {
                                GeneradorAST.listaErrores.AddLast(new Error("La funcion " + id + " no retorna un valor " + this.retorno.ToString(), Error.TipoError.SEMANTICO, 0, 0));
                            }

                        }
                        else
                        {
                            GeneradorAST.listaErrores.AddLast(new Error("La funcion " + id + " no esta retornando nada", Error.TipoError.SEMANTICO, 0, 0));
                        }
                    }
                    else
                    {

                        inst.ejecutar(tsFuncion);
                    }
                }
            }
            if (Parametros != null)
            {

                foreach (ParametroFP p in parametros)
                {
                    if (p.Rv == ParametroFP.TipoValor.REFERENCIA)
                    {
                        ts.setValor(p.Refe, p.Valor);
                    }
                }
            }
            foreach (Simbolo s in tsFuncion)
            {
                    if (!GeneradorAST.tablaCompleta.existe(s.Id))
                        GeneradorAST.tablaCompleta.AddLast(s);
                    else GeneradorAST.tablaCompleta.setValor(s.Id, s.Valor);
            }
            if (tsFuncion.getValor(id)!=null)
            {
                try
                {
                    switch (this.Retorno)
                    {
                        case Simbolo.TipoDato.INTEGER:
                            return Double.Parse(tsFuncion.getValor(id).ToString());
                        case Simbolo.TipoDato.REAL:
                            return Double.Parse(tsFuncion.getValor(id).ToString());
                        case Simbolo.TipoDato.BOOLEAN:
                            return Boolean.Parse(tsFuncion.getValor(id).ToString());
                        case Simbolo.TipoDato.STRING:
                            return tsFuncion.getValor(id).ToString();
                        case Simbolo.TipoDato.OBJECT:
                            return tsFuncion.getValor(id);
                    }
                }
                catch (Exception e)
                {
                    GeneradorAST.listaErrores.AddLast(new Error("La funcion "+id+" no retorna un valor "+this.retorno.ToString(),Error.TipoError.SEMANTICO,0,0));
                }
                
            }
            else
            {
                GeneradorAST.listaErrores.AddLast(new Error("La funcion "+id+" no esta retornando nada",Error.TipoError.SEMANTICO,0,0)) ;
            }
            
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            int spFunc = parametros.Count;

            Estructura_Stack StckFunc = new Estructura_Stack();
            StckFunc.agregarStack(stack.Top);
            int j = 0;
            foreach (ParametroFP par in parametros)
            {
                StckFunc.agregarStack(new Elemento_Stack(par.Id,par.Tipo,j,0,null,true));
                j++;
            }
            GeneradorAST.funcionActual = this;
            String retornar = "";
            String temp = "T" + t;
            temporales.AddLast("T"+t);
            t++;
            retornar += "void " + id.ToLower() + "(){\n";
            retornar += temp + "=SP+" + spFunc+";\n";
            retornar += "Stack[(int)"+temp+"]=0;\n";
            StckFunc.agregarStack(new Elemento_Stack(id.ToString(), Retorno, spFunc, 0, null,true));
            spFunc++;
            if (Instrucciones != null)
            {
                foreach (Instruccion inst in Instrucciones)
                {
                    if (inst != null)
                    {
                        retornar += inst.traduccion(StckFunc, heap, temporales, ref spFunc, ref hp, ref t, ref l).ToString();
                    }
                }
            }
            if (Sentencias != null)
            {
                foreach (Instruccion inst in Sentencias)
                {
                    if (inst != null)
                    {
                        retornar += inst.traduccion(StckFunc, heap, temporales, ref spFunc, ref hp, ref t, ref l).ToString();
                    }
                }
            }
            retornar += "Retornar"+id.ToLower()+":\n";
            retornar += "return;\n}\n";
            GeneradorAST.funcionActual = null;
            return retornar;
        }
    }
}
