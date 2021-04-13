using _OLC2_Proyecto1_201801229.Analizador;
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
                    inst.ejecutar(tsFuncion);
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
    }
}
