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
                            if (elemntoStack==null)
                            {
                                val = dato.retornarTipo3();
                                elemntoStack = stack.buscarElementoStack(val);
                            }
                            if (elemntoStack != null)
                            {
                                if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
                                {
                                    retornar += "T" + t + "= SP;\n";
                                    String temp = "T" + t;
                                    temporales.AddLast("T" + t);
                                    t++;
                                    retornar += "SP=" + elemntoStack.ReferenciaStack + ";\n";
                                    retornar += "printString();\n";
                                    retornar += "SP=" + temp + ";\n";
                                }
                                else if (elemntoStack.Tipo == Simbolo.TipoDato.BOOLEAN)
                                {
                                    retornar += "T" + t + "= SP;\n";
                                    String temp = "T" + t;
                                    temporales.AddLast("T" + t);
                                    t++;
                                    retornar += "SP=" + elemntoStack.ReferenciaStack + ";\n";
                                    retornar += "printBool();\n";
                                    retornar += "SP=" + temp + ";\n";
                                }
                                else
                                {
                                    retornar += ret + "printf(\"" + tipoImpresion(elemntoStack.Tipo) + "\"," + imp(elemntoStack.Tipo) + ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ");\n";
                                }
                            }
                            else
                            {
                                if (ret.Contains("T"))
                                {
                                    retornar += ret;
                                    Operacion.Tipo_operacion op = dato.retornarTipo2();

                                    if (op == Operacion.Tipo_operacion.SUMA || op == Operacion.Tipo_operacion.RESTA || op == Operacion.Tipo_operacion.MULTIPLICACION || op == Operacion.Tipo_operacion.MODULAR || op == Operacion.Tipo_operacion.DIVISION || op == Operacion.Tipo_operacion.NEGATIVO)
                                    {
                                        retornar += "printf(\"%f\"," + "(float)" + ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ");\n";
                                    }
                                    else
                                    {
                                        String etiqueta1 = "L" + l;
                                        l++;
                                        String etiqueta2 = "L" + l;
                                        l++;
                                        String etiqueta3 = "L" + l;
                                        l++;
                                        retornar += "if(" + ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "==0)goto " + etiqueta1 + ";\ngoto " + etiqueta2 + ";\n";
                                        retornar += etiqueta1 + ":\n";
                                        retornar += "printf(\"%c\",(char)70);\n";
                                        retornar += "printf(\"%c\",(char)65);\n";
                                        retornar += "printf(\"%c\",(char)76);\n";
                                        retornar += "printf(\"%c\",(char)83);\n";
                                        retornar += "printf(\"%c\",(char)69);\ngoto " + etiqueta3 + ";\n";
                                        retornar += etiqueta2 + ":\n";
                                        retornar += "printf(\"%c\",(char)84);\n";
                                        retornar += "printf(\"%c\",(char)82);\n";
                                        retornar += "printf(\"%c\",(char)85);\n";
                                        retornar += "printf(\"%c\",(char)69);\n" + etiqueta3 + ":\n";
                                    }
                                }
                                else
                                {
                                    Operacion.Tipo_operacion op = dato.retornarTipo2();
                                    if (op == Operacion.Tipo_operacion.CADENA || op == Operacion.Tipo_operacion.CONCAT)
                                    {
                                        String comparar = ret.Split("\n")[0];
                                        for (int i = 0; i < comparar.Length; i++)
                                        {
                                            char c = ret[i];
                                            retornar += "printf(\"%c\",(char)" + (int)c + ");\n";
                                        }
                                    }
                                    else if (op == Operacion.Tipo_operacion.BOOLEAN)
                                    {
                                        String comparar = ret.Split("\n")[0];
                                        if (comparar.Equals("0"))
                                        {
                                            retornar += "printf(\"%c\",(char)70);\n";
                                            retornar += "printf(\"%c\",(char)65);\n";
                                            retornar += "printf(\"%c\",(char)76);\n";
                                            retornar += "printf(\"%c\",(char)83);\n";
                                            retornar += "printf(\"%c\",(char)69);\n";
                                        }
                                        else
                                        {
                                            retornar += "printf(\"%c\",(char)84);\n";
                                            retornar += "printf(\"%c\",(char)82);\n";
                                            retornar += "printf(\"%c\",(char)85);\n";
                                            retornar += "printf(\"%c\",(char)69);\n";
                                        }
                                    }
                                    else
                                    {
                                        retornar += "printf(\"%f\",(float)" + ret.Split("\n")[0] + ");\n";
                                    }
                                }
                            }
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
                            if (elemntoStack == null)
                            {
                                val = dato.retornarTipo3();
                                elemntoStack = stack.buscarElementoStack(val);
                            }
                            if (elemntoStack != null)
                            {
                                if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
                                {
                                    retornar += "T" + t + "= SP;\n";
                                    String temp = "T" + t;
                                    temporales.AddLast("T" + t);
                                    t++;
                                    retornar += "SP=" + elemntoStack.ReferenciaStack + ";\n";
                                    retornar += "printString();\n";
                                    retornar += "SP=" + temp + ";\n";
                                }
                                else if (elemntoStack.Tipo == Simbolo.TipoDato.BOOLEAN)
                                {
                                    retornar += "T" + t + "= SP;\n";
                                    String temp = "T" + t;
                                    temporales.AddLast("T" + t);
                                    t++;
                                    retornar += "SP=" + elemntoStack.ReferenciaStack + ";\n";
                                    retornar += "printBool();\n";
                                    retornar += "SP=" + temp + ";\n";
                                }
                                else
                                {
                                    retornar += ret + "printf(\"" + tipoImpresion(elemntoStack.Tipo) + "\"," + imp(elemntoStack.Tipo) + ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ");\n";
                                }
                            }
                            else
                            {
                                if (ret.Contains("T"))
                                {
                                    retornar += ret;
                                    Operacion.Tipo_operacion op = dato.retornarTipo2();

                                    if (op == Operacion.Tipo_operacion.SUMA || op == Operacion.Tipo_operacion.RESTA || op == Operacion.Tipo_operacion.MULTIPLICACION || op == Operacion.Tipo_operacion.MODULAR || op == Operacion.Tipo_operacion.DIVISION || op == Operacion.Tipo_operacion.NEGATIVO)
                                    {
                                        retornar += "printf(\"%f\"," + "(float)"+ ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ");\n";
                                    }
                                    else
                                    {
                                        String etiqueta1 = "L" + l;
                                        l++;
                                        String etiqueta2 = "L" + l;
                                        l++;
                                        String etiqueta3 = "L" + l;
                                        l++;
                                        retornar += "if(" + ret.Split("\n")[ret.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "==0)goto " + etiqueta1 + ";\ngoto " + etiqueta2 + ";\n";
                                        retornar += etiqueta1 + ":\n";
                                        retornar += "printf(\"%c\",(char)70);\n";
                                        retornar += "printf(\"%c\",(char)65);\n";
                                        retornar += "printf(\"%c\",(char)76);\n";
                                        retornar += "printf(\"%c\",(char)83);\n";
                                        retornar += "printf(\"%c\",(char)69);\ngoto " + etiqueta3 + ";\n";
                                        retornar += etiqueta2 + ":\n";
                                        retornar += "printf(\"%c\",(char)84);\n";
                                        retornar += "printf(\"%c\",(char)82);\n";
                                        retornar += "printf(\"%c\",(char)85);\n";
                                        retornar += "printf(\"%c\",(char)69);\n" + etiqueta3 + ":\n";
                                    }
                                }
                                else
                                {
                                    Operacion.Tipo_operacion op = dato.retornarTipo2();
                                    if (op == Operacion.Tipo_operacion.CADENA || op == Operacion.Tipo_operacion.CONCAT)
                                    {
                                        String comparar = ret.Split("\n")[0];
                                        for (int i=0;i<comparar.Length;i++)
                                        {
                                            char c = ret[i];
                                            retornar += "printf(\"%c\",(char)"+(int)c+");\n";
                                        }
                                    }else if (op == Operacion.Tipo_operacion.BOOLEAN)
                                    {
                                        String comparar = ret.Split("\n")[0];
                                        if (comparar.Equals("0"))
                                        {
                                            retornar += "printf(\"%c\",(char)70);\n";
                                            retornar += "printf(\"%c\",(char)65);\n";
                                            retornar += "printf(\"%c\",(char)76);\n";
                                            retornar += "printf(\"%c\",(char)83);\n";
                                            retornar += "printf(\"%c\",(char)69);\n";
                                        }
                                        else
                                        {
                                            retornar += "printf(\"%c\",(char)84);\n";
                                            retornar += "printf(\"%c\",(char)82);\n";
                                            retornar += "printf(\"%c\",(char)85);\n";
                                            retornar += "printf(\"%c\",(char)69);\n";
                                        }
                                    }
                                    else
                                    {
                                        retornar += "printf(\"%f\",(float)" + ret.Split("\n")[0] + ");\n";
                                    }
                                }
                            }
                            
                        }
                        retornar += "printf(\"%c\",(char)10);\n";
                    }
                    break;
            }
            return retornar;
        }
    }
}
