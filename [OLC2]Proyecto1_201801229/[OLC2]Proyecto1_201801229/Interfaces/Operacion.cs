using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using _OLC2_Proyecto1_201801229.Analizador;
using _OLC2_Proyecto1_201801229.Estructuras;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class Operacion : Instruccion
    {
        public enum Tipo_operacion
        {
            SUMA,
            CONCAT,
            RESTA,
            MULTIPLICACION,
            DIVISION,
            MODULAR,
            NEGATIVO,
            NUMERO,
            IDENTIFICADOR,
            CADENA,
            DECIMAL,
            MAYOR_QUE,
            MENOR_QUE,
            AND,
            OR,
            NOT,
            MAYOR_IGUAL_QUE,
            MENOR_IGUAL_QUE,
            IGUAL,
            DIFERENTE,
            LLAMADAPROCEDIMIENTO,
            LLAMADAFUNCION,
            BOOLEAN,
            OBJECT,
            ARRAY,
            TYPE
        }

        private Tipo_operacion tipo;
        private String id;
        private Operacion operadorIzq;
        private Operacion operadorDer;
        private Object valor;
        private LinkedList<Operacion> valores;
        private String type;

        internal Tipo_operacion Tipo { get => tipo; set => tipo = value; }

        public Operacion()
        {

        }
        public Operacion(String id, String type, Tipo_operacion tipo)
        {
            this.id = id;
            this.type = type;
            this.tipo = tipo;
        }

        public Operacion(String id, Operacion operadorDer, Tipo_operacion tipo)
        {
            this.id = id;
            this.operadorDer = operadorDer;
            this.tipo = tipo;
        }
        public Operacion(Operacion operadorIzq, Operacion operadorDer, Tipo_operacion tipo)
        {
            this.Tipo = tipo;
            this.operadorIzq = operadorIzq;
            this.operadorDer = operadorDer;
        }
        public Operacion(Operacion operadorIzq, Tipo_operacion tipo)
        {
            this.Tipo = tipo;
            this.operadorIzq = operadorIzq;
        }
        public Operacion(String id, Tipo_operacion tipo)
        {
            this.id = id;
            this.Tipo = tipo;
        }
        public Operacion(String id, LinkedList<Operacion> valor, Tipo_operacion tipo)
        {
            this.id = id;
            this.valores = valor;
            this.Tipo = tipo;
        }
        public Operacion(Object valor, Tipo_operacion tipo)
        {
            this.valor = valor;
            this.Tipo = tipo;
        }


        public Object ejecutar(TablaSimbolos ts)
        {
            Object valorIzquierdo;
            Object valorDerecho;
            switch (tipo)
            {
                //Operaciones Logicas
                case Tipo_operacion.AND:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Boolean && valorDerecho is Boolean)
                        {
                            return (Boolean)valorIzquierdo && (Boolean)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.OR:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Boolean && valorDerecho is Boolean)
                        {
                            return (Boolean)valorIzquierdo || (Boolean)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.NOT:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    if (valorIzquierdo != null)
                    {
                        if (valorIzquierdo is Boolean)
                        {
                            return !(Boolean)valorIzquierdo;
                        }
                    }
                    return null;

                //Operaciones Relacionales
                case Tipo_operacion.MAYOR_IGUAL_QUE:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo >= (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.MAYOR_QUE:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo > (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.MENOR_IGUAL_QUE:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo <= (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.MENOR_QUE:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo < (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.IGUAL:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        return (operadorIzq.ejecutar(ts)).Equals((operadorDer.ejecutar(ts)));
                    }
                    return null;
                case Tipo_operacion.DIFERENTE:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo != (Double)valorDerecho;
                        }
                    }
                    return null;

                //Operaciones Aritmeticaas
                case Tipo_operacion.SUMA:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo + (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.RESTA:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo - (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.MULTIPLICACION:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo * (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.DIVISION:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo / (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.MODULAR:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    valorDerecho = operadorDer.ejecutar(ts);
                    if (valorIzquierdo != null && valorDerecho != null)
                    {
                        if (valorIzquierdo is Double && valorDerecho is Double)
                        {
                            return (Double)valorIzquierdo % (Double)valorDerecho;
                        }
                    }
                    return null;
                case Tipo_operacion.NEGATIVO:
                    valorIzquierdo = operadorIzq.ejecutar(ts);
                    if (valorIzquierdo != null)
                    {
                        if (valorIzquierdo is Double)
                        {
                            return (Double)valorIzquierdo * -1;
                        }
                    }
                    return null;


                //Operacion Concatenar
                case Tipo_operacion.CONCAT:
                    return operadorIzq.ejecutar(ts).ToString() + operadorDer.ejecutar(ts).ToString();

                //Valores
                case Tipo_operacion.NUMERO:
                    try
                    {
                        return Double.Parse(valor.ToString());
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo integer", "Error");
                        return null;
                    }
                case Tipo_operacion.DECIMAL:
                    try
                    {
                        return Double.Parse(valor.ToString());
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.BOOLEAN:
                    try
                    {
                        return Boolean.Parse(valor.ToString());
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo boolean", "Error");
                        return null;
                    }
                case Tipo_operacion.CADENA:
                    try
                    {
                        return valor.ToString();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.OBJECT:
                    try
                    {
                        return valor;
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.IDENTIFICADOR:
                    try
                    {
                        bool arrtype = true;
                        foreach (ArrayPascal ar in GeneradorAST.arrays)
                        {
                            if (ar.Id.ToLower().Equals(valor.ToString().ToLower()))
                            {
                                arrtype = false;
                                return new ArrayPascal(ar.Id, ar.LimInferior, ar.LimSuperior, ar.Limi, ar.Lims, ar.Tipo, ar.Type1, ar.Arreglo);
                            }
                        }
                        if (arrtype)
                        {
                            Simbolo sim = ts.getSimbolo(valor.ToString());
                            if (sim != null)
                            {
                                switch (sim.Tipo)
                                {
                                    case Simbolo.TipoDato.INTEGER:
                                        return Double.Parse(ts.getValor(valor.ToString()).ToString());
                                    case Simbolo.TipoDato.OBJECT:
                                        return ts.getValor(valor.ToString());
                                    case Simbolo.TipoDato.STRING:
                                        return ts.getValor(valor.ToString()).ToString();
                                    case Simbolo.TipoDato.REAL:
                                        return Double.Parse(ts.getValor(valor.ToString()).ToString());
                                    case Simbolo.TipoDato.BOOLEAN:
                                        return Boolean.Parse(ts.getValor(valor.ToString()).ToString());
                                    default:
                                        return null;
                                }
                            }
                            else
                            {
                                GeneradorAST.listaErrores.AddLast(new Error("Error simbolo no existe", Error.TipoError.SEMANTICO, 0, 0));
                            }
                            return null;

                        }
                        else
                        {
                            return null;
                        }

                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.ARRAY:
                    try
                    {
                        Object arr = ts.getValor(id);
                        ArrayPascal arreglo = (ArrayPascal)arr;
                        switch (arreglo.Tipo)
                        {
                            case Simbolo.TipoDato.INTEGER:
                                return Double.Parse(arreglo.buscarValor(int.Parse(operadorDer.ejecutar(ts).ToString())).ToString());
                            case Simbolo.TipoDato.OBJECT:
                                return arreglo.buscarValor(int.Parse(operadorDer.ejecutar(ts).ToString())).ToString();
                            case Simbolo.TipoDato.STRING:
                                return arreglo.buscarValor(int.Parse(operadorDer.ejecutar(ts).ToString())).ToString();
                            case Simbolo.TipoDato.REAL:
                                return Double.Parse(arreglo.buscarValor(int.Parse(operadorDer.ejecutar(ts).ToString())).ToString());
                            case Simbolo.TipoDato.BOOLEAN:
                                return Boolean.Parse(arreglo.buscarValor(int.Parse(operadorDer.ejecutar(ts).ToString())).ToString());
                            default:
                                return null;
                        }

                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.TYPE:
                    try
                    {
                        Object arr = ts.getValor(id);
                        InstruccionType arreglo = (InstruccionType)arr;
                        return arreglo.buscarValor(type, ts);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo type", "Error");
                        return null;
                    }

                //Funcion
                case Tipo_operacion.LLAMADAFUNCION:
                    try
                    {
                        bool fp = true;
                        bool err = false;
                        Funcion func = new Funcion();
                        int i = 0;
                        if (GeneradorAST.funciones != null)
                        {
                            foreach (Funcion f in GeneradorAST.funciones)
                            {
                                if (f.Id.ToLower().Equals(id.ToLower()))
                                {
                                    func = new Funcion(f.Id, f.Retorno, f.ValorRetorno, f.Parametros, f.Instrucciones, f.Sentencias, f.Type);
                                    fp = false;
                                    if (valores.Count == f.Parametros.Count)
                                    {
                                        foreach (ParametroFP par in f.Parametros)
                                        {
                                            if (par.Rv == ParametroFP.TipoValor.REFERENCIA)
                                            {
                                                if (valores.ElementAt(i).Tipo == Tipo_operacion.IDENTIFICADOR)
                                                {
                                                    Object val = valores.ElementAt(i).ejecutar(ts);
                                                    switch (par.Tipo)
                                                    {
                                                        case Simbolo.TipoDato.INTEGER:
                                                            par.Valor = Double.Parse(val.ToString());
                                                            par.Refe = valores.ElementAt(i).valor.ToString();
                                                            break;
                                                        case Simbolo.TipoDato.OBJECT:
                                                            par.Valor = val;
                                                            par.Refe = valores.ElementAt(i).valor.ToString();
                                                            break;
                                                        case Simbolo.TipoDato.STRING:
                                                            par.Valor = val.ToString();
                                                            par.Refe = valores.ElementAt(i).valor.ToString();
                                                            break;
                                                        case Simbolo.TipoDato.REAL:
                                                            par.Valor = Double.Parse(val.ToString());
                                                            par.Refe = valores.ElementAt(i).valor.ToString();
                                                            break;
                                                        case Simbolo.TipoDato.BOOLEAN:
                                                            par.Valor = Boolean.Parse(val.ToString());
                                                            par.Refe = valores.ElementAt(i).valor.ToString();
                                                            break;
                                                        default:
                                                            par.Valor = null;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    Object val = valores.ElementAt(i).ejecutar(ts);
                                                    switch (par.Tipo)
                                                    {
                                                        case Simbolo.TipoDato.INTEGER:
                                                            par.Valor = Double.Parse(val.ToString());
                                                            break;
                                                        case Simbolo.TipoDato.OBJECT:
                                                            par.Valor = val;
                                                            break;
                                                        case Simbolo.TipoDato.STRING:
                                                            par.Valor = val.ToString();
                                                            break;
                                                        case Simbolo.TipoDato.REAL:
                                                            par.Valor = Double.Parse(val.ToString());
                                                            break;
                                                        case Simbolo.TipoDato.BOOLEAN:
                                                            par.Valor = Boolean.Parse(val.ToString());
                                                            break;
                                                        default:
                                                            par.Valor = null;
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Object val = valores.ElementAt(i).ejecutar(ts);
                                                switch (par.Tipo)
                                                {
                                                    case Simbolo.TipoDato.INTEGER:
                                                        par.Valor = Double.Parse(val.ToString());
                                                        break;
                                                    case Simbolo.TipoDato.OBJECT:
                                                        par.Valor = val;
                                                        break;
                                                    case Simbolo.TipoDato.STRING:
                                                        par.Valor = val.ToString();
                                                        break;
                                                    case Simbolo.TipoDato.REAL:
                                                        par.Valor = Double.Parse(val.ToString());
                                                        break;
                                                    case Simbolo.TipoDato.BOOLEAN:
                                                        par.Valor = Boolean.Parse(val.ToString());
                                                        break;
                                                    default:
                                                        par.Valor = null;
                                                        break;
                                                }
                                            }
                                            i++;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        err = true;
                                    }

                                }
                            }
                        }
                        if (fp)
                        {
                            Procedimiento pro = new Procedimiento();
                            if (GeneradorAST.procedimientos != null)
                            {
                                foreach (Procedimiento f in GeneradorAST.procedimientos)
                                {
                                    if (f.Id.ToLower().Equals(id.ToLower()))
                                    {
                                        pro = new Procedimiento(f.Id, f.Parametros, f.Instrucciones, f.Sentencias);
                                        fp = false;
                                        if (valores.Count == f.Parametros.Count)
                                        {
                                            foreach (ParametroFP par in f.Parametros)
                                            {
                                                if (par.Rv == ParametroFP.TipoValor.REFERENCIA)
                                                {
                                                    if (valores.ElementAt(i).Tipo == Tipo_operacion.IDENTIFICADOR)
                                                    {
                                                        Object val = valores.ElementAt(i).ejecutar(ts);
                                                        switch (par.Tipo)
                                                        {
                                                            case Simbolo.TipoDato.INTEGER:
                                                                par.Valor = Double.Parse(val.ToString());
                                                                par.Refe = valores.ElementAt(i).valor.ToString();
                                                                break;
                                                            case Simbolo.TipoDato.OBJECT:
                                                                par.Valor = val;
                                                                par.Refe = valores.ElementAt(i).valor.ToString();
                                                                break;
                                                            case Simbolo.TipoDato.STRING:
                                                                par.Valor = val.ToString();
                                                                par.Refe = valores.ElementAt(i).valor.ToString();
                                                                break;
                                                            case Simbolo.TipoDato.REAL:
                                                                par.Valor = Double.Parse(val.ToString());
                                                                par.Refe = valores.ElementAt(i).valor.ToString();
                                                                break;
                                                            case Simbolo.TipoDato.BOOLEAN:
                                                                par.Valor = Boolean.Parse(val.ToString());
                                                                par.Refe = valores.ElementAt(i).valor.ToString();
                                                                break;
                                                            default:
                                                                par.Valor = null;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Object val = valores.ElementAt(i).ejecutar(ts);
                                                        switch (par.Tipo)
                                                        {
                                                            case Simbolo.TipoDato.INTEGER:
                                                                par.Valor = Double.Parse(val.ToString());
                                                                break;
                                                            case Simbolo.TipoDato.OBJECT:
                                                                par.Valor = val;
                                                                break;
                                                            case Simbolo.TipoDato.STRING:
                                                                par.Valor = val.ToString();
                                                                break;
                                                            case Simbolo.TipoDato.REAL:
                                                                par.Valor = Double.Parse(val.ToString());
                                                                break;
                                                            case Simbolo.TipoDato.BOOLEAN:
                                                                par.Valor = Boolean.Parse(val.ToString());
                                                                break;
                                                            default:
                                                                par.Valor = null;
                                                                break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Object val = valores.ElementAt(i).ejecutar(ts);
                                                    switch (par.Tipo)
                                                    {
                                                        case Simbolo.TipoDato.INTEGER:
                                                            par.Valor = Double.Parse(val.ToString());
                                                            break;
                                                        case Simbolo.TipoDato.OBJECT:
                                                            par.Valor = val;
                                                            break;
                                                        case Simbolo.TipoDato.STRING:
                                                            par.Valor = val.ToString();
                                                            break;
                                                        case Simbolo.TipoDato.REAL:
                                                            par.Valor = Double.Parse(val.ToString());
                                                            break;
                                                        case Simbolo.TipoDato.BOOLEAN:
                                                            par.Valor = Boolean.Parse(val.ToString());
                                                            break;
                                                        default:
                                                            par.Valor = null;
                                                            break;
                                                    }
                                                }
                                                i++;
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            err = true;
                                        }

                                    }
                                }
                            }
                            if (!fp)
                            {
                                pro.ejecutar(ts);
                            }
                            else
                            {
                                GeneradorAST.listaErrores.AddLast(new Error("No existe ninguna funcion o procedimiento con el nombre " + id, Error.TipoError.SEMANTICO, 0, 0));
                            }
                            return null;
                        }
                        else
                        {
                            return func.ejecutar(ts);
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                case Tipo_operacion.LLAMADAPROCEDIMIENTO:
                    try
                    {
                        bool fp = true;
                        bool err = false;
                        Funcion func = new Funcion();
                        foreach (Funcion f in GeneradorAST.funciones)
                        {
                            if (f.Id.ToLower().Equals(id.ToLower()))
                            {
                                func = new Funcion(f.Id, f.Retorno, f.ValorRetorno, f.Parametros, f.Instrucciones, f.Sentencias, f.Type);
                                fp = false;
                            }
                        }
                        if (fp)
                        {
                            Procedimiento pro = new Procedimiento();
                            if (GeneradorAST.procedimientos != null)
                            {
                                foreach (Procedimiento f in GeneradorAST.procedimientos)
                                {
                                    if (f.Id.ToLower().Equals(id.ToLower()))
                                    {
                                        pro = new Procedimiento(f.Id, f.Parametros, f.Instrucciones, f.Sentencias);
                                        fp = false;
                                        break;
                                    }
                                }
                            }
                            if (!fp)
                            {
                                pro.ejecutar(ts);
                            }
                            else
                            {
                                GeneradorAST.listaErrores.AddLast(new Error("No existe ninguna funcion o procedimiento con el nombre " + id, Error.TipoError.SEMANTICO, 0, 0));
                            }
                            return null;
                        }
                        else
                        {
                            return func.ejecutar(ts);
                        }
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
            }
            return null;
        }
        public Object traduccion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String valorIzquierdo;
            String valorDerecho;
            String retornar = "";
            switch (tipo)
            {
                //Operaciones Logicas
                case Tipo_operacion.AND:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    retornar += valorIzquierdo + "&&" + valorDerecho;
                    return retornar;
                case Tipo_operacion.OR:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    retornar += valorIzquierdo + "||" + valorDerecho;
                    return retornar;

                //Operaciones Relacionales
                case Tipo_operacion.MAYOR_IGUAL_QUE:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ">=" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MAYOR_QUE:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ">" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MENOR_IGUAL_QUE:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "<=" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MENOR_QUE:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "<" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.IGUAL:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "==" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.DIFERENTE:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "!=" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;

                //Operaciones Aritmeticaas
                case Tipo_operacion.SUMA:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }

                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "+" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.RESTA:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "-" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MULTIPLICACION:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "*" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.DIVISION:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "/" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MODULAR:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= (int)" + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "% (int)" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.NEGATIVO:
                    valorIzquierdo = operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.caseActual!=null)
                    {
                        retornar += "-" + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0];
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        retornar += "T" + t + "= -" + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                        temporales.AddLast("T" + t);
                        t++;
                    }
                    return retornar;

                case Tipo_operacion.CONCAT:
                    return operadorIzq.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString() + operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();

                //Valores
                case Tipo_operacion.NUMERO:
                    try
                    {
                        return valor.ToString() + "\n";
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo integer", "Error");
                        return null;
                    }
                case Tipo_operacion.DECIMAL:
                    try
                    {

                        return valor.ToString() + "\n";
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.BOOLEAN:
                    try
                    {
                        if (Boolean.Parse(valor.ToString()))
                        {

                            return "1" + "\n";
                        }
                        else
                        {

                            return "0" + "\n";
                        }
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo boolean", "Error");
                        return null;
                    }
                case Tipo_operacion.CADENA:
                    try
                    {
                        return valor.ToString();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.IDENTIFICADOR:
                    Elemento_Stack elemntoStack = stack.buscarElementoStack(valor.ToString());
                    if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
                    {
                        retornar = "";
                        int rh = elemntoStack.ReferenciaHeap;
                        Elemento_Heap elemntoHeap = heap.buscarElementoHeap(rh);
                        while (elemntoHeap.Valor != -1)
                        {
                            retornar += "" + (char)elemntoHeap.Valor;
                            rh++;
                            elemntoHeap = heap.buscarElementoHeap(rh);
                        }
                        return retornar;
                    }
                    else
                    {
                        if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                        {
                            if (elemntoStack.Fu)
                            {
                                String temp = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += temp + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                                retornar += "T" + t + "= Stack[(int)" + temp + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else
                            {
                                retornar = "T" + t + "= Stack[" + elemntoStack.ReferenciaStack + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }

                            return retornar;
                        }
                        else
                        {
                            retornar = "T" + t + "= Stack[" + elemntoStack.ReferenciaStack + "];\n";
                            temporales.AddLast("T" + t);
                            t++;
                            return retornar;
                        }
                    }
                case Tipo_operacion.LLAMADAFUNCION:
                    try
                    {
                        Elemento_Funcion elementoFucnion = GeneradorAST.pilaFuncion.buscar(id);
                        if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {
                                Funcion func = (Funcion)elementoFucnion.Funcion;

                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornarParametro += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornarParametro += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornarParametro += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornarParametro += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornarParametro += "HP=HP+1;\n";
                                        hp++;
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornarParametro += temp1 + "=SP+1;\n";
                                        retornarParametro += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornarParametro += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornarParametro += "Stack[(int)" + temp3 + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornarParametro += temp1 + "=SP+1;\n";
                                        retornarParametro += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornarParametro += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornarParametro += "Stack[(int)" + temp3 + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    parametros.AddLast(retornarParametro);
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP+" + func.Parametros.Count + ";\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                Procedimiento func = (Procedimiento)elementoFucnion.Funcion;

                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornarParametro += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornar += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornar += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornar += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornar += "HP=HP+1;\n";
                                        hp++;
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornar += temp1 + "=SP+1;\n";
                                        retornar += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornar += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornar += "Stack[(int)" + temp3 + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornarParametro += temp1 + "=SP+1;\n";
                                        retornarParametro += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornarParametro += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornarParametro += "Stack[(int)" + temp3 + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                        parametros.AddLast(retornarParametro);
                                    }
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                            }
                        }
                        else
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {
                                Funcion func = (Funcion)elementoFucnion.Funcion;
                                int te = sp;
                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\n";
                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                retornar += "SP=SP+1;\n";
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornarParametro += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornarParametro += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornarParametro += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornarParametro += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornarParametro += "HP=HP+1;\n";
                                        hp++;
                                        retornarParametro += "T" + t + "=SP+" + j + ";\n";
                                        retornarParametro += "Stack[(int)T" + t + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        retornarParametro += "T" + t + "=SP+" + j + ";\n";
                                        retornarParametro += "Stack[(int)T" + t + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    parametros.AddLast(retornarParametro);
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP+" + func.Parametros.Count + ";\n";
                                retornar += "SP=" + t1 + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                Procedimiento func = (Procedimiento)elementoFucnion.Funcion;
                                int te = sp;
                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\n";
                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                retornar += "SP=SP+1;\n";
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornar += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornar += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornar += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornar += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornar += "HP=HP+1;\n";
                                        hp++;
                                        retornar += "T" + t + "=SP+" + j + ";\n";
                                        retornar += "Stack[(int)T" + t + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        retornarParametro += "T" + t + "=SP+" + j + ";\n";
                                        retornarParametro += "Stack[(int)T" + t + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                        parametros.AddLast(retornarParametro);
                                    }
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=" + t1 + ";\n";
                            }
                        }
                        return retornar;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                case Tipo_operacion.LLAMADAPROCEDIMIENTO:
                    try
                    {
                        Elemento_Funcion elementoFucnion = GeneradorAST.pilaFuncion.buscar(id);
                        if (GeneradorAST.funcionActual != null || GeneradorAST.procedimientoActual != null)
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {
                                Funcion func = (Funcion)elementoFucnion.Funcion;
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP+0;\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                            }
                        }
                        else
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {

                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\nSP=SP+1;\n";
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP;\n";
                                retornar += "SP=" + t1 + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\nSP=SP+1;\n";
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=" + t1 + ";\n";
                            }
                        }

                        return retornar;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

            }
            return null;
        }
        public String retornarTipo()
        {
            return valor != null ? valor.ToString() : "";
        }
        public Tipo_operacion retornarTipo2()
        {
            return tipo;
        }
        public String retornarTipo3()
        {
            return tipo == Tipo_operacion.LLAMADAPROCEDIMIENTO || tipo == Tipo_operacion.LLAMADAFUNCION ? id : "";
        }

        public Object traduccionCondicion(Estructura_Stack stack, Estructura_Heap heap, LinkedList<String> temporales, ref int sp, ref int hp, ref int t, ref int l)
        {
            String valorIzquierdo;
            String valorDerecho;
            String retornar = "";
            switch (tipo)
            {
                //Operaciones Logicas
                case Tipo_operacion.AND:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    retornar += valorIzquierdo + "&&" + valorDerecho ;
                    return retornar;
                case Tipo_operacion.OR:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    retornar += valorIzquierdo + "||" + valorDerecho;
                    return retornar;
                case Tipo_operacion.NOT:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();

                    retornar += "@" + valorIzquierdo;
                    return retornar;

                //Operaciones Relacionales
                case Tipo_operacion.MAYOR_IGUAL_QUE:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ">=" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "\n";

                    return retornar;
                case Tipo_operacion.MAYOR_QUE:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ">" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "\n";

                    return retornar;
                case Tipo_operacion.MENOR_IGUAL_QUE:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "<=" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "\n";

                    return retornar;
                case Tipo_operacion.MENOR_QUE:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "<" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "\n";

                    return retornar;
                case Tipo_operacion.IGUAL:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "==" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "\n";

                    return retornar;
                case Tipo_operacion.DIFERENTE:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    if (valorDerecho.Contains("T"))
                    {
                        retornar += valorDerecho;
                    }
                    retornar += valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "!=" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "\n";

                    return retornar;

                //Operaciones Aritmeticaas
                case Tipo_operacion.SUMA:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "+" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.RESTA:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "-" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MULTIPLICACION:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "*" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";
                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.DIVISION:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= " + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "/" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.MODULAR:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    valorDerecho = operadorDer.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                    if (GeneradorAST.funcionActual != null||GeneradorAST.procedimientoActual!=null)
                    {
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                    }
                    else
                    {
                        if (valorIzquierdo.Contains("T"))
                        {
                            retornar += valorIzquierdo;
                        }
                        if (valorDerecho.Contains("T"))
                        {
                            retornar += valorDerecho;
                        }
                    }
                    retornar += "T" + t + "= (int)" + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + "% (int)" + valorDerecho.Split("\n")[valorDerecho.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;
                case Tipo_operacion.NEGATIVO:
                    valorIzquierdo = operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();

                    if (valorIzquierdo.Contains("T"))
                    {
                        retornar += valorIzquierdo;
                    }
                    retornar += "T" + t + "= -" + valorIzquierdo.Split("\n")[valorIzquierdo.Split("\n").Length - 2].Split("=")[0].Split(";")[0] + ";\n";

                    temporales.AddLast("T" + t);
                    t++;
                    return retornar;

                case Tipo_operacion.CONCAT:
                    return operadorIzq.traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString() + operadorDer.traduccion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();

                //Valores
                case Tipo_operacion.NUMERO:
                    try
                    {
                        return valor.ToString() + "\n";
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo integer", "Error");
                        return null;
                    }
                case Tipo_operacion.DECIMAL:
                    try
                    {

                        return valor.ToString() + "\n";
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.BOOLEAN:
                    try
                    {
                        if (Boolean.Parse(valor.ToString()))
                        {

                            return "1" + "\n";
                        }
                        else
                        {

                            return "0" + "\n";
                        }
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo boolean", "Error");
                        return null;
                    }
                case Tipo_operacion.CADENA:
                    try
                    {
                        return valor.ToString();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("No es tipo real", "Error");
                        return null;
                    }
                case Tipo_operacion.IDENTIFICADOR:
                    Elemento_Stack elemntoStack = stack.buscarElementoStack(valor.ToString());
                    if (elemntoStack.Tipo == Simbolo.TipoDato.STRING)
                    {
                        retornar = "";
                        int rh = elemntoStack.ReferenciaHeap;
                        Elemento_Heap elemntoHeap = heap.buscarElementoHeap(rh);
                        while (elemntoHeap.Valor != -1)
                        {
                            retornar += "" + (char)elemntoHeap.Valor;
                            rh++;
                            elemntoHeap = heap.buscarElementoHeap(rh);
                        }
                        return retornar;
                    }
                    else
                    {
                        if (GeneradorAST.funcionActual != null || GeneradorAST.procedimientoActual != null)
                        {
                            if (elemntoStack.Fu)
                            {
                                String temp = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += temp + "=SP+" + elemntoStack.ReferenciaStack + ";\n";
                                retornar += "T" + t + "= Stack[(int)" + temp + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else
                            {
                                retornar = "T" + t + "= Stack[" + elemntoStack.ReferenciaStack + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }

                            return retornar;
                        }
                        else
                        {
                            retornar = "T" + t + "= Stack[" + elemntoStack.ReferenciaStack + "];\n";
                            temporales.AddLast("T" + t);
                            t++;
                            return retornar;
                        }
                    }
                case Tipo_operacion.LLAMADAFUNCION:
                    try
                    {
                        Elemento_Funcion elementoFucnion = GeneradorAST.pilaFuncion.buscar(id);
                        if (GeneradorAST.funcionActual != null || GeneradorAST.procedimientoActual != null)
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {
                                Funcion func = (Funcion)elementoFucnion.Funcion;

                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornarParametro += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornarParametro += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornarParametro += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornarParametro += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornarParametro += "HP=HP+1;\n";
                                        hp++;
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornarParametro += temp1 + "=SP+1;\n";
                                        retornarParametro += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornarParametro += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornarParametro += "Stack[(int)" + temp3 + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornarParametro += temp1 + "=SP+1;\n";
                                        retornarParametro += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornarParametro += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornarParametro += "Stack[(int)" + temp3 + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    parametros.AddLast(retornarParametro);
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP+" + func.Parametros.Count + ";\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                Procedimiento func = (Procedimiento)elementoFucnion.Funcion;

                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornarParametro += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornar += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornar += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornar += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornar += "HP=HP+1;\n";
                                        hp++;
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornar += temp1 + "=SP+1;\n";
                                        retornar += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornar += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornar += "Stack[(int)" + temp3 + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        String temp1 = "T" + t;
                                        temporales.AddLast(temp1);
                                        t++;
                                        String temp2 = "T" + t;
                                        temporales.AddLast(temp2);
                                        t++;
                                        String temp3 = "T" + t;
                                        temporales.AddLast(temp3);
                                        t++;
                                        retornarParametro += temp1 + "=SP+1;\n";
                                        retornarParametro += temp2 + "=" + temp1 + "+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                        retornarParametro += temp3 + "=" + temp2 + "+" + j + ";\n";
                                        retornarParametro += "Stack[(int)" + temp3 + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                        parametros.AddLast(retornarParametro);
                                    }
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                            }
                        }
                        else
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {
                                Funcion func = (Funcion)elementoFucnion.Funcion;
                                int te = sp;
                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\n";
                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                retornar += "SP=SP+1;\n";
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornarParametro += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornarParametro += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornarParametro += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornarParametro += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornarParametro += "HP=HP+1;\n";
                                        hp++;
                                        retornarParametro += "T" + t + "=SP+" + j + ";\n";
                                        retornarParametro += "Stack[(int)T" + t + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        retornarParametro += "T" + t + "=SP+" + j + ";\n";
                                        retornarParametro += "Stack[(int)T" + t + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    parametros.AddLast(retornarParametro);
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP+" + func.Parametros.Count + ";\n";
                                retornar += "SP=" + t1 + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                Procedimiento func = (Procedimiento)elementoFucnion.Funcion;
                                int te = sp;
                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\n";
                                int j = 0;
                                LinkedList<String> parametros = new LinkedList<string>();
                                retornar += "SP=SP+1;\n";
                                foreach (ParametroFP par in func.Parametros)
                                {
                                    String valu = valores.ElementAt(j).traduccionCondicion(stack, heap, temporales, ref sp, ref hp, ref t, ref l).ToString();
                                    String retornarParametro = "";
                                    if (valu.Contains("T"))
                                    {
                                        retornar += valu;
                                    }
                                    if (par.Tipo == Simbolo.TipoDato.STRING)
                                    {
                                        int refH = hp;
                                        for (int i = 0; i < valu.Length; i++)
                                        {
                                            char c = valu[i];
                                            retornar += "Heap[(int)HP]=" + (int)c + ";\n";
                                            heap.agregarHeap(new Elemento_Heap((int)c, hp, null));
                                            retornar += "HP=HP+1;\n";
                                            hp++;
                                        }
                                        retornar += "Heap[(int)HP]=-1;\n";
                                        heap.agregarHeap(new Elemento_Heap(-1, hp, null));
                                        retornar += "HP=HP+1;\n";
                                        hp++;
                                        retornar += "T" + t + "=SP+" + j + ";\n";
                                        retornar += "Stack[(int)T" + t + "]=" + refH + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                    }
                                    else
                                    {
                                        String[] valores = valu.Split("\n");
                                        String temp = valores[valores.Length - 2].Split("=")[0].Split(";")[0];
                                        retornarParametro += "T" + t + "=SP+" + j + ";\n";
                                        retornarParametro += "Stack[(int)T" + t + "]=" + temp + ";\n";
                                        temporales.AddLast("T" + t);
                                        t++;
                                        parametros.AddLast(retornarParametro);
                                    }
                                    j++;
                                }
                                for (int i = parametros.Count - 1; i >= 0; i--)
                                {
                                    retornar += parametros.ElementAt(i);
                                }
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=" + t1 + ";\n";
                            }
                        }
                        return retornar;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }

                case Tipo_operacion.LLAMADAPROCEDIMIENTO:
                    try
                    {
                        Elemento_Funcion elementoFucnion = GeneradorAST.pilaFuncion.buscar(id);
                        if (GeneradorAST.funcionActual != null || GeneradorAST.procedimientoActual != null)
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {
                                Funcion func = (Funcion)elementoFucnion.Funcion;
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP+0;\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                retornar += "SP=SP+1;\nSP=SP+" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=SP-1;\nSP=SP-" + (sp > 0 ? (int)(sp - 1) : 0) + ";\n";
                            }
                        }
                        else
                        {
                            if (elementoFucnion.Funcion.GetType() == typeof(Funcion))
                            {

                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\nSP=SP+1;\n";
                                retornar += id.ToLower() + "();\n";
                                String t2 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t2 + "=SP;\n";
                                retornar += "SP=" + t1 + ";\n";
                                retornar += "T" + t + "=Stack[(int)" + t2 + "];\n";
                                temporales.AddLast("T" + t);
                                t++;
                            }
                            else if (elementoFucnion.Funcion.GetType() == typeof(Procedimiento))
                            {
                                String t1 = "T" + t;
                                temporales.AddLast("T" + t);
                                t++;
                                retornar += t1 + "=SP;\nSP=SP+1;\n";
                                retornar += id.ToLower() + "();\n";
                                retornar += "SP=" + t1 + ";\n";
                            }
                        }

                        return retornar;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
            }
            return null;
        }
    }
}
