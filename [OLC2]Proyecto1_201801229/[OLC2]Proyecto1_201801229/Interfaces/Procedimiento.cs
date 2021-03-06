using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Procedimiento:Instruccion
    {
        String id;
        LinkedList<ParametroFP> parametros ;
        LinkedList<Instruccion> instrucciones;
        LinkedList<Instruccion> sentencias;
        public Procedimiento()
        {

        }
        public Procedimiento(String id, LinkedList<ParametroFP> parametros, LinkedList<Instruccion> instrucciones, LinkedList<Instruccion> sentencias)
        {
            this.Parametros = parametros;
            this.Id = id;
            this.Instrucciones = instrucciones;
            this.Sentencias = sentencias;
        }

        public string Id { get => id; set => id = value; }
        internal LinkedList<ParametroFP> Parametros { get => parametros; set => parametros = value; }
        internal LinkedList<Instruccion> Instrucciones { get => instrucciones; set => instrucciones = value; }
        internal LinkedList<Instruccion> Sentencias { get => sentencias; set => sentencias = value; }

        public Object ejecutar(TablaSimbolos ts)
        {
            TablaSimbolos tsFuncion = new TablaSimbolos(Id);
            if (Parametros != null)
            {
                foreach (ParametroFP d in Parametros)
                {
                    tsFuncion.AddLast(new Simbolo(d.Id.ToString(), d.Tipo, d.Valor, Id, Simbolo.TipoVarariable.VAR));
                }
            }
            foreach (Simbolo s in ts)
            {
                if (!tsFuncion.existe(s.Id))
                {
                    tsFuncion.AddLast(s);
                }
            }
            if (Instrucciones != null)
            {
                foreach (Instruccion inst in Instrucciones)
                {
                    if (inst != null)
                    {

                        inst.ejecutar(tsFuncion);
                    }
                }
            }
            if (Sentencias!=null)
            {
                foreach (Instruccion inst in Sentencias)
                {
                    inst.ejecutar(tsFuncion);
                }
            }
            if (Parametros != null)
            {
                foreach (ParametroFP p in Parametros)
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
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            int spFunc = parametros != null ? parametros.Count : 0;

            Estructura_Stack StckFunc = new Estructura_Stack();
            StckFunc.agregarStack(stack.Top);
            int j = 0;
            if (parametros != null)
            {
                foreach (ParametroFP par in parametros)
                {
                    StckFunc.agregarStack(new Elemento_Stack(par.Id, par.Tipo, j, 0, null, true));
                    j++;
                }
            }
            GeneradorAST.procedimientoActual = this;
            String retornar = "";
            String temp = "T" + t;
            temporales.AddLast("T" + t);
            t++;
            retornar += "void " + id.ToLower() + "(){\n";
            if (Instrucciones != null)
            {
                foreach (Instruccion inst in Instrucciones)
                {
                    if (inst != null)
                    {

                        retornar += inst.traduccion(StckFunc, heap,temporales,ref spFunc, ref hp, ref t, ref l).ToString();
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
            retornar += "Retornar"+this.Id.ToLower()+":\n";
            retornar += "return;\n}\n";
            GeneradorAST.procedimientoActual = null;
            stack.vaciarStack();
            stack.agregarStack(StckFunc.Top);
            return retornar;
        }
    }
}
