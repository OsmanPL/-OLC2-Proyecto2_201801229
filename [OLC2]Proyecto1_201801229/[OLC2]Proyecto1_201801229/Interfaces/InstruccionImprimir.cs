using _OLC2_Proyecto1_201801229.Estructuras;
using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class InstruccionImprimir:Instruccion
    {
        public enum TipoImprimir
        {
            WRITE,
            WRITELN
        }

        TipoImprimir tipo;
        LinkedList<Operacion> datos;

        public InstruccionImprimir(TipoImprimir tipo, LinkedList<Operacion> datos)
        {
            this.tipo = tipo;
            this.datos = datos;
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            switch (tipo)
            {
                case TipoImprimir.WRITE:
                    if (datos != null)
                    {

                        foreach (Operacion dato in datos)
                        {
                            Object valor = dato.ejecutar(ts);
                            if (valor != null)
                            {
                                
                            }
                        }
                    }
                    break;
                case TipoImprimir.WRITELN:
                    if (datos!=null)
                    {

                        foreach (Operacion dato in datos)
                        {
                            Object valor = dato.ejecutar(ts);
                            if (valor != null)
                            {
                                
                            }
                        }
                    }
                    break;
            }
            return null;
        }
        public String tipoImpresion(Simbolo.TipoDato tip)
        {
            switch (tip)
            {
                case Simbolo.TipoDato.INTEGER:
                    return "%d";
                case Simbolo.TipoDato.REAL:
                    return "%f";

            }
            return "";
        }
        public String imp(Simbolo.TipoDato tip)
        {
            switch (tip)
            {
                case Simbolo.TipoDato.INTEGER:
                    return "(int)";
                case Simbolo.TipoDato.REAL:
                    return "(float)";

            }
            return "";
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String retornar = "";
            switch (tipo)
            {
                case TipoImprimir.WRITE:
                    if (datos != null)
                    {

                        foreach (Operacion dato in datos)
                        {
                            String  ret = dato.traduccion(stack,heap,temporales,ref sp, ref hp, ref t, ref l).ToString();
                            String val = dato.retornarTipo();
                            Elemento_Stack elemntoStack = stack.buscarElementoStack(val);
                            retornar += ret+"printf(\""+tipoImpresion(elemntoStack.Tipo)+"\","+imp(elemntoStack.Tipo)+ret.Split("\n")[ret.Split("\n").Length -2].Split("=")[0].Split(";")[0]+");\n";
                        }
                    }
                    break;
                case TipoImprimir.WRITELN:
                    if (datos != null)
                    {

                        foreach (Operacion dato in datos)
                        {
                            String ret = dato.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                            String val = dato.retornarTipo();
                            Elemento_Stack elemntoStack = stack.buscarElementoStack(val);
                            retornar += ret + "printf(\"" + tipoImpresion(elemntoStack.Tipo) + "\","+ imp(elemntoStack.Tipo) + ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ");\n";
                            retornar += "printf(\"%c\",(char)10);\n";
                        }
                    }
                    break;
            }
            return retornar;
        }
    }
}
