using _OLC2_Proyecto1_201801229.Analizador;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionFor:Instruccion
    {
        public enum TipoFor
        {
            INCREMENTO,
            DECREMENTO
        }
        Asignacion asignarValorFor;
        Operacion limite;
        LinkedList<Instruccion> sentencias;
        TipoFor tipo;
        String id;

        public InstruccionFor(Asignacion asignarValor, Operacion limite, LinkedList<Instruccion> sentencias, TipoFor tipo, String id)
        {
            this.asignarValorFor = asignarValor;
            this.limite = limite;
            this.sentencias = sentencias;
            this.tipo = tipo;
            this.id = id;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            try
            {
                switch (tipo)
                {
                    case TipoFor.INCREMENTO:
                        asignarValorFor.ejecutar(ts);
                        Boolean lim = Boolean.Parse(limite.ejecutar(ts).ToString());
                        if (!lim)
                        {
                            while (!lim)
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
                                }

                                Operacion op = new Operacion((Object)id, Operacion.Tipo_operacion.IDENTIFICADOR);
                                Operacion op1 = new Operacion(1, Operacion.Tipo_operacion.NUMERO);
                                Operacion op2 = new Operacion(op, op1, Operacion.Tipo_operacion.SUMA);
                                Asignacion nueva = new Asignacion(id, op2);
                                nueva.ejecutar(ts);
                                lim = Boolean.Parse(limite.ejecutar(ts).ToString());
                            }
                            Operacion op3 = new Operacion((Object)id, Operacion.Tipo_operacion.IDENTIFICADOR);
                            Operacion op4 = new Operacion(1, Operacion.Tipo_operacion.NUMERO);
                            Operacion op5 = new Operacion(op3, op4, Operacion.Tipo_operacion.RESTA);
                            Asignacion nueva1 = new Asignacion(id, op5);
                            nueva1.ejecutar(ts);
                        }

                        break;
                    case TipoFor.DECREMENTO:
                        asignarValorFor.ejecutar(ts);
                        Boolean lim2 = Boolean.Parse(limite.ejecutar(ts).ToString());
                        if (!lim2)
                        {
                            while (!lim2)
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
                                Operacion op = new Operacion((Object)id, Operacion.Tipo_operacion.IDENTIFICADOR);
                                Operacion op1 = new Operacion(1, Operacion.Tipo_operacion.NUMERO);
                                Operacion op2 = new Operacion(op, op1, Operacion.Tipo_operacion.RESTA);
                                Asignacion nueva = new Asignacion(id, op2);
                                nueva.ejecutar(ts);
                                lim2 = Boolean.Parse(limite.ejecutar(ts).ToString());
                            }
                            Operacion op6 = new Operacion((Object)id, Operacion.Tipo_operacion.IDENTIFICADOR);
                            Operacion op7 = new Operacion(1, Operacion.Tipo_operacion.NUMERO);
                            Operacion op8 = new Operacion(op6, op7, Operacion.Tipo_operacion.SUMA);
                            Asignacion nueva2 = new Asignacion(id, op8);
                            nueva2.ejecutar(ts);
                        }

                        break;
                }
            }
            catch (Exception e)
            {
                GeneradorAST.listaErrores.AddLast(new Error("Condicion no retorna un valor boolean", Error.TipoError.SEMANTICO, 0, 0));
            }
            return null;
        }
    }
}
