using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Irony;
using Irony.Parsing;
using System.Text;
using _OLC2_Proyecto1_201801229.Interfaces;
using Irony.Ast;
using System.Windows.Forms;

namespace _OLC2_Proyecto1_201801229.Analizador
{
    class GeneradorAST
    {
        public static LinkedList<Funcion> funciones = new LinkedList<Funcion>();
        public static LinkedList<Procedimiento> procedimientos = new LinkedList<Procedimiento>();
        public static LinkedList<InstruccionType> type = new LinkedList<InstruccionType>();
        public static LinkedList<ArrayPascal> arrays = new LinkedList<ArrayPascal>();
        public static LinkedList<Error> listaErrores = new LinkedList<Error>();
        public static TablaSimbolos tablaCompleta = new TablaSimbolos("Completa");
        ParseTreeNode raiz = null;

        public ParseTreeNode retornarRaiz()
        {
            return raiz;
        }
        public void analizar(string codigo)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(codigo);
            raiz = arbol.Root;
            funciones = new LinkedList<Funcion>();
            procedimientos = new LinkedList<Procedimiento>();
            type = new LinkedList<InstruccionType>();
            tablaCompleta = new TablaSimbolos("Completa");
            listaErrores = new LinkedList<Error>();
            arrays = new LinkedList<ArrayPascal>();

            if (raiz != null && arbol.ParserMessages.Count == 0)
            {
                Programa AST = metodoProgram(raiz.ChildNodes.ElementAt(0));
                TablaSimbolos ts = new TablaSimbolos("Main");
                AST.ejecutar(ts);
                foreach (Simbolo sim in ts)
                {
                    if (!tablaCompleta.existe(sim.Id))
                        tablaCompleta.AddLast(sim);
                    else tablaCompleta.setValor(sim.Id, sim.Valor);
                }
                if (listaErrores.Count > 0)
                {
                    MessageBox.Show("Existen errores", "Error");
                }
                else
                {
                    MessageBox.Show("Se ejecuto todo con exito", "Correcto");
                }
            }
            else
            {
                for (int i = 0; i < arbol.ParserMessages.Count; i++)
                {
                    if (arbol.ParserMessages.ElementAt(i).Message.Contains("Syntax"))
                    {
                        listaErrores.AddLast(new Error(arbol.ParserMessages.ElementAt(i).Message, Error.TipoError.SINTACTICO, arbol.ParserMessages.ElementAt(i).Location.Line, arbol.ParserMessages.ElementAt(i).Location.Column));
                    }
                    else
                    {
                        listaErrores.AddLast(new Error(arbol.ParserMessages.ElementAt(i).Message, Error.TipoError.LEXICO, arbol.ParserMessages.ElementAt(i).Location.Line, arbol.ParserMessages.ElementAt(i).Location.Column));
                    }
                }

                MessageBox.Show("Existen errores", "Error");
            }
        }

        private LinkedList<Instruccion> Instrucciones(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> listaInstrucciones = Instrucciones(nodoActual.ChildNodes.ElementAt(0));
                listaInstrucciones.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(1)));
                return listaInstrucciones;

            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<Instruccion> listaInstrucciones = new LinkedList<Instruccion>();
                listaInstrucciones.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(0)));
                return listaInstrucciones;
            }
            else
            {
                return null;
            }
        }

        private Instruccion metodoInstruccion(ParseTreeNode nodoActual)
        {
            String sentencia = nodoActual.ChildNodes.ElementAt(0).Term.Name;
            switch (sentencia)
            {
                case "NT_program":
                    return metodoProgram(nodoActual.ChildNodes.ElementAt(0));
                case "NT_listaDeclaraciones":
                    return metodoListaDeclaracio(nodoActual.ChildNodes.ElementAt(0));
                case "NT_asignacion":
                    return metodoAsignacion(nodoActual.ChildNodes.ElementAt(0));
                case "NT_type":
                    return metodoTypeArray(nodoActual.ChildNodes.ElementAt(0));
                case "NT_if":
                    return metodoIf(nodoActual.ChildNodes.ElementAt(0));
                case "NT_sentenciaCase":
                    return metodoSwitch(nodoActual.ChildNodes.ElementAt(0));
                case "NT_while":
                    return metodoWhile(nodoActual.ChildNodes.ElementAt(0));
                case "NT_for":
                    return metodoFor(nodoActual.ChildNodes.ElementAt(0));
                case "NT_repeat":
                    return metodoRepeat(nodoActual.ChildNodes.ElementAt(0));
                case "break":
                    return new InstruccionBreak();
                case "continue":
                    return new InstruccionContinue();
                case "NT_funcion":
                    funciones.AddLast(metodoFuncion(nodoActual.ChildNodes.ElementAt(0)));
                    return null;
                case "NT_procedimiento":
                    procedimientos.AddLast(metodoProcedmiento(nodoActual.ChildNodes.ElementAt(0)));
                    return null;
                case "NT_write":
                    return metodoImprimir(nodoActual.ChildNodes.ElementAt(0), InstruccionImprimir.TipoImprimir.WRITE);
                case "NT_writeln":
                    return metodoImprimir(nodoActual.ChildNodes.ElementAt(0), InstruccionImprimir.TipoImprimir.WRITELN);
                case "NT_graficar_ts":
                    return new GraficarTS();
                case "NT_exit":
                    return metodoExit(nodoActual.ChildNodes.ElementAt(0));
                case "NT_llamadaFuncion":
                    return metodoExpresion(nodoActual.ChildNodes.ElementAt(0));
                case "NT_llamadaProcedimiento":
                    return metodoExpresion(nodoActual.ChildNodes.ElementAt(0));
            }
            return null;
        }

        private Programa metodoProgram(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 6)
            {
                return new Programa(Instrucciones(nodoActual.ChildNodes.ElementAt(1)), Instrucciones(nodoActual.ChildNodes.ElementAt(3)));
            }
            else
            {
                return null;
            }
        }
        private String NT_tipo_VAR_s(ParseTreeNode nodoActual)
        {
            return NT_tipo(nodoActual.ChildNodes.ElementAt(0));
        }

        private ListaDeclaracion metodoListaDeclaracio(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                String tipovar = nodoActual.ChildNodes.ElementAt(0).Term.Name.ToString().ToLower();
                if (tipovar.Equals("var"))
                {
                    return new ListaDeclaracion(listaDeclaracionVar(nodoActual.ChildNodes.ElementAt(1),Simbolo.TipoVarariable.VAR));
                }
                else if (tipovar.Equals("const"))
                {
                    return new ListaDeclaracion(listaDeclaracionConst(nodoActual.ChildNodes.ElementAt(1),Simbolo.TipoVarariable.CONST));
                }
            }
            return null;
        }

        private LinkedList<Declaracion> listaDeclaracionConst(ParseTreeNode nodoActual, Simbolo.TipoVarariable tipoVariable)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                LinkedList<Declaracion> listDeclaracion = listaDeclaracionConst(nodoActual.ChildNodes.ElementAt(0), tipoVariable);
                listDeclaracion.AddLast(metodoDeclaracionConstante(nodoActual.ChildNodes.ElementAt(1)));
                return listDeclaracion;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<Declaracion> listDeclaracion = new LinkedList<Declaracion>();
                listDeclaracion.AddLast(metodoDeclaracionConstante(nodoActual.ChildNodes.ElementAt(0)));
                return listDeclaracion;
            }
            return null;
        }
        private LinkedList<Declaracion> listaDeclaracionVar(ParseTreeNode nodoActual, Simbolo.TipoVarariable tipoVariable)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                LinkedList<Declaracion> listDeclaracion = listaDeclaracionVar(nodoActual.ChildNodes.ElementAt(0), tipoVariable);
                listDeclaracion.AddLast(metodoDeclaracionVariable(nodoActual.ChildNodes.ElementAt(1)));
                return listDeclaracion;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<Declaracion> listDeclaracion = new LinkedList<Declaracion>();
                listDeclaracion.AddLast(metodoDeclaracionVariable(nodoActual.ChildNodes.ElementAt(0)));
                return listDeclaracion;
            }
            return null;
        }
        private Declaracion metodoDeclaracionConstante(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 6)
            {

                Simbolo.TipoDato tipo = NT_tipo_VAR(nodoActual.ChildNodes.ElementAt(2));
                return new Declaracion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], tipo, metodoOperacion(nodoActual.ChildNodes.ElementAt(4)), Simbolo.TipoVarariable.CONST, "", false);
            }
            else if(nodoActual.ChildNodes.Count == 4)
            {
                return new Declaracion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], Simbolo.TipoDato.OBJECT, metodoOperacion(nodoActual.ChildNodes.ElementAt(2)), Simbolo.TipoVarariable.CONST, "", false);
            }
            return null;
        }
        private Declaracion metodoDeclaracionVariable(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 4)
            {
                Simbolo.TipoDato tipo = NT_tipo_VAR(nodoActual.ChildNodes.ElementAt(2));
                Object valorDafult = valorDefecto(tipo, NT_tipo_VAR_s(nodoActual.ChildNodes.ElementAt(2)));
                Operacion.Tipo_operacion tip = tipOp(tipo);
                return new Declaracion(listaVariables(nodoActual.ChildNodes.ElementAt(0)), tipo, new Operacion(valorDafult, tip), Simbolo.TipoVarariable.VAR, NT_tipo(nodoActual.ChildNodes.ElementAt(2)), true);
            }else if (nodoActual.ChildNodes.Count == 1)
            {
                return iniciallizarVariable(nodoActual.ChildNodes.ElementAt(0));
            }
            return null;
        }

        private Declaracion iniciallizarVariable(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 5)
            {
                Simbolo.TipoDato tipo = NT_tipo_VAR(nodoActual.ChildNodes.ElementAt(2));
                bool t = valorvacio(nodoActual.ChildNodes.ElementAt(3));
                if (t)
                {
                    return new Declaracion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], tipo, ini(nodoActual.ChildNodes.ElementAt(3)), Simbolo.TipoVarariable.VAR, NT_tipo(nodoActual.ChildNodes.ElementAt(2)), false);
                }
                else
                {
                    Object val = valorDefecto(tipo, NT_tipo_VAR_s(nodoActual.ChildNodes.ElementAt(2)));
                    Operacion.Tipo_operacion tip = tipOp(tipo);
                    return new Declaracion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], tipo, new Operacion(val, tip), Simbolo.TipoVarariable.VAR, NT_tipo(nodoActual.ChildNodes.ElementAt(2)), false);
                }
            }
            return null;
        }
        private Operacion.Tipo_operacion tipOp(Simbolo.TipoDato tipo)
        {
            switch (tipo)
            {
                case Simbolo.TipoDato.BOOLEAN:
                    return Operacion.Tipo_operacion.BOOLEAN;
                case Simbolo.TipoDato.INTEGER:
                    return Operacion.Tipo_operacion.NUMERO;
                case Simbolo.TipoDato.OBJECT:
                    return Operacion.Tipo_operacion.OBJECT;
                case Simbolo.TipoDato.REAL:
                    return Operacion.Tipo_operacion.DECIMAL;
                case Simbolo.TipoDato.STRING:
                    return Operacion.Tipo_operacion.CADENA;
                case Simbolo.TipoDato.IDENTIFICADOR:
                    return Operacion.Tipo_operacion.IDENTIFICADOR;
                default:
                    return Operacion.Tipo_operacion.IDENTIFICADOR;
            }
        }
        private Operacion ini(ParseTreeNode nodoActual)
        {
            return metodoOperacion(nodoActual.ChildNodes.ElementAt(1));
        }
        private bool valorvacio(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                return true;
            }
            return false;
        }

        private Operacion metodoOperacion(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                string operador = nodoActual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (operador.ToLower())
                {
                    case "and":
                        return new Operacion(metodoOperacion(nodoActual.ChildNodes.ElementAt(0)), metodoOperacion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.AND);
                    case "or":
                        return new Operacion(metodoOperacion(nodoActual.ChildNodes.ElementAt(0)), metodoOperacion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.OR);
                    default:
                        return metodoOperacion(nodoActual.ChildNodes.ElementAt(1));
                }
            }
            else if (nodoActual.ChildNodes.Count == 2)
            {
                return new Operacion(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.NOT);
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                return metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0));
            }
            return null;
        }
        private Operacion metodoOperacionRelacional(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                string operador = nodoActual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (operador.ToLower())
                {
                    case "=":
                        return new Operacion(metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0)), metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.IGUAL);
                    case ">":
                        return new Operacion(metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0)), metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MAYOR_QUE);
                    case "<":
                        return new Operacion(metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0)), metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MENOR_QUE);
                    case ">=":
                        return new Operacion(metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0)), metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MAYOR_IGUAL_QUE);
                    case "<=":
                        return new Operacion(metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0)), metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MENOR_IGUAL_QUE);
                    case "<>":
                        return new Operacion(metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(0)), metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.DIFERENTE);
                    default:
                        return metodoOperacionRelacional(nodoActual.ChildNodes.ElementAt(1));
                }
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                return metodoExpresion(nodoActual.ChildNodes.ElementAt(0));
            }
            return null;
        }
        private Operacion metodoExpresion(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                string operador = nodoActual.ChildNodes.ElementAt(1).ToString().Split(' ')[0];
                switch (operador)
                {
                    case "+":
                        Operacion opizq = metodoExpresion(nodoActual.ChildNodes.ElementAt(0));
                        Operacion opder = metodoExpresion(nodoActual.ChildNodes.ElementAt(2));
                        if (opizq.Tipo == Operacion.Tipo_operacion.CADENA || opizq.Tipo == Operacion.Tipo_operacion.CONCAT)
                        {
                            return new Operacion(opizq, opder, Operacion.Tipo_operacion.CONCAT);
                        }
                        else
                        {
                            return new Operacion(opizq, opder, Operacion.Tipo_operacion.SUMA);
                        }
                    case "-":
                        return new Operacion(metodoExpresion(nodoActual.ChildNodes.ElementAt(0)), metodoExpresion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.RESTA);
                    case "/":
                        return new Operacion(metodoExpresion(nodoActual.ChildNodes.ElementAt(0)), metodoExpresion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.DIVISION);
                    case "*":
                        return new Operacion(metodoExpresion(nodoActual.ChildNodes.ElementAt(0)), metodoExpresion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MULTIPLICACION);
                    case "%":
                        return new Operacion(metodoExpresion(nodoActual.ChildNodes.ElementAt(0)), metodoExpresion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.MODULAR);
                    case "(":
                        return new Operacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], Operacion.Tipo_operacion.LLAMADAPROCEDIMIENTO);
                    default:
                        return metodoExpresion(nodoActual.ChildNodes.ElementAt(1));
                }
            }
            else if (nodoActual.ChildNodes.Count == 2)
            {
                return new Operacion(metodoExpresion(nodoActual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.NEGATIVO);
            }
            else if (nodoActual.ChildNodes.Count == 4)
            {
                return new Operacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], valoresParametros(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.LLAMADAFUNCION);
            }
            else
            {
                if (pruebaValor(nodoActual.ChildNodes.ElementAt(0)))
                {
                    string operador = "";
                    Object valor = "";

                    operador = NT_valor(nodoActual.ChildNodes.ElementAt(0));
                    valor = NT_valor_valor(nodoActual.ChildNodes.ElementAt(0)).ToString().Split(' ')[0];
                    if (NT_valor_valor(nodoActual.ChildNodes.ElementAt(0)).ToString().Split(' ').Length > 2)
                    {
                        valor = "";
                        for (int i = 0; i < NT_valor_valor(nodoActual.ChildNodes.ElementAt(0)).ToString().Split(' ').Length; i++)
                        {
                            if (i != NT_valor_valor(nodoActual.ChildNodes.ElementAt(0)).ToString().Split(' ').Length - 1)
                            {
                                valor += NT_valor_valor(nodoActual.ChildNodes.ElementAt(0)).ToString().Split(' ')[i] + " ";
                            }
                        }
                    }

                    Console.WriteLine(operador);
                    Console.WriteLine(valor);
                    switch (operador.ToLower().Split(' ')[0])
                    {
                        case "cadena":
                            return new Operacion(valor, Operacion.Tipo_operacion.CADENA);
                        case "numero":
                            return new Operacion(valor, Operacion.Tipo_operacion.NUMERO);
                        case "decimal":
                            return new Operacion(valor, Operacion.Tipo_operacion.DECIMAL);
                        case "true":
                            return new Operacion(valor, Operacion.Tipo_operacion.BOOLEAN);
                        case "false":
                            return new Operacion(valor, Operacion.Tipo_operacion.BOOLEAN);
                        case "object":
                            return new Operacion(valor, Operacion.Tipo_operacion.OBJECT);
                        default:
                            return new Operacion(valor, Operacion.Tipo_operacion.IDENTIFICADOR);
                    }
                }
                else
                {
                    return valorArrayType(nodoActual.ChildNodes.ElementAt(0));
                }
            }
        }
        private Operacion valorArrayType(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 4)
            {
                return new Operacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], metodoOperacion(nodoActual.ChildNodes.ElementAt(2)), Operacion.Tipo_operacion.ARRAY);
            }
            else if (nodoActual.ChildNodes.Count == 3)
            {
                return new Operacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], nodoActual.ChildNodes.ElementAt(2).ToString().Split(' ')[0], Operacion.Tipo_operacion.TYPE);
            }
            return null;
        }
        private bool pruebaValor(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 1)
            {
                return true;
            }
            return false;
        }

        private LinkedList<Operacion> valoresParametros(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                LinkedList<Operacion> operaciones = valoresParametros(nodoActual.ChildNodes.ElementAt(0));
                operaciones.AddLast(metodoOperacion(nodoActual.ChildNodes.ElementAt(2)));
                return operaciones;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<Operacion> operaciones = new LinkedList<Operacion>();
                operaciones.AddLast(metodoOperacion(nodoActual.ChildNodes.ElementAt(0)));
                return operaciones;
            }
            return null;
        }

        private LinkedList<String> listaVariables(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                LinkedList<String> listaVar = listaVariables(nodoActual.ChildNodes.ElementAt(0));
                listaVar.AddLast(nodoActual.ChildNodes.ElementAt(2).ToString().Split(' ')[0]);
                return listaVar;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<String> listaVar = new LinkedList<String>();
                listaVar.AddLast(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0]);
                return listaVar;
            }
            return null;
        }

        private String NT_tipo(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 0)
            {
                return nodoActual.ToString().Split(' ')[0];
            }
            else
            {
                return nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            }
        }

        private Simbolo.TipoDato buscarTipoDato(String buscar)
        {
            switch (buscar.ToLower().Split(' ')[0])
            {
                case "integer":
                    return Simbolo.TipoDato.INTEGER;
                case "string":
                    return Simbolo.TipoDato.STRING;
                case "boolean":
                    return Simbolo.TipoDato.BOOLEAN;
                case "real":
                    return Simbolo.TipoDato.REAL;
                case "object":
                    return Simbolo.TipoDato.OBJECT;
                default:
                    return Simbolo.TipoDato.IDENTIFICADOR;
            }
        }
        private Object valorDefecto(Simbolo.TipoDato tipo, String g)
        {
            switch (tipo)
            {
                case Simbolo.TipoDato.INTEGER:
                    return 0;
                case Simbolo.TipoDato.STRING:
                    return "";
                case Simbolo.TipoDato.REAL:
                    return 0;
                case Simbolo.TipoDato.BOOLEAN:
                    return false;
                case Simbolo.TipoDato.OBJECT:
                    return "";
                default:
                    return g;
            }
        }
        private String NT_valor(ParseTreeNode nodoActual)
        {
            return nodoActual.ChildNodes.ElementAt(0).Term.Name.ToString().ToLower().Split(' ')[0];
        }
        private Object NT_valor_valor(ParseTreeNode nodoActual)
        {
            return nodoActual.ChildNodes.ElementAt(0);
        }
        private Simbolo.TipoDato NT_tipo_VAR(ParseTreeNode nodoActual)
        {
            return buscarTipoDato(nodoActual.ChildNodes.ElementAt(0).ToString().ToLower().Split(' ')[0]);
        }

        private Asignacion metodoAsignacion(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 4)
            {
                return new Asignacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], metodoOperacion(nodoActual.ChildNodes.ElementAt(2)));
            }
            else if (nodoActual.ChildNodes.Count == 6)
            {
                return new Asignacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], metodoOperacion(nodoActual.ChildNodes.ElementAt(4)), nodoActual.ChildNodes.ElementAt(2).ToString().Split(' ')[0]);
            }
            else if (nodoActual.ChildNodes.Count == 7)
            {
                return new Asignacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], metodoOperacion(nodoActual.ChildNodes.ElementAt(5)), metodoOperacion(nodoActual.ChildNodes.ElementAt(2)));
            }
            return null;
        }
        private Instruccion metodoTypeArray(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                return metodoSeparar(nodoActual.ChildNodes.ElementAt(1));
            }
            return null;
        }
        private Instruccion metodoSeparar(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 5)
            {
                String id = NT_declaracionObjecto(nodoActual.ChildNodes.ElementAt(0));
                Hashtable campos = camposType(nodoActual.ChildNodes.ElementAt(2));

                return new InstruccionType(id, campos);

            }
            else if (nodoActual.ChildNodes.Count == 4)
            {
                return metodoArray(nodoActual.ChildNodes.ElementAt(3), nodoActual.ChildNodes.ElementAt(0));
            }
            return null;
        }

        private String NT_declaracionObjecto(ParseTreeNode nodoActual)
        {
            return nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
        }

        private Hashtable camposType(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                Hashtable campos = camposType(nodoActual.ChildNodes.ElementAt(0));
                Hashtable camp = campoType(nodoActual.ChildNodes.ElementAt(1));
                foreach (DictionaryEntry item in camp)
                {
                    campos.Add(item.Key, item.Value);
                }
                return campos;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                Hashtable campos = new Hashtable();
                Hashtable camp = campoType(nodoActual.ChildNodes.ElementAt(0));
                foreach (DictionaryEntry item in camp)
                {
                    campos.Add(item.Key, item.Value);
                }
                return campos;
            }
            return null;
        }

        private Hashtable campoType(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 4)
            {
                LinkedList<String> vars = listaVariables(nodoActual.ChildNodes.ElementAt(0));
                Simbolo.TipoDato tipo = buscarTipoDato(NT_tipo(nodoActual.ChildNodes.ElementAt(2)));
                Object val = valorDefecto(tipo, NT_tipo(nodoActual.ChildNodes.ElementAt(2)));
                Operacion.Tipo_operacion tip = tipOp(tipo);
                Hashtable valores = new Hashtable();
                foreach (String id in vars)
                {
                    Parametro parametro = new Parametro(tipo, new Operacion(val, tip), NT_tipo(nodoActual.ChildNodes.ElementAt(2)));
                    valores.Add(id.ToLower(), parametro);
                }
                return valores;
            }
            return null;
        }

        private ArrayPascal metodoArray(ParseTreeNode nodoActual, ParseTreeNode identificador)
        {
            if (nodoActual.ChildNodes.Count == 9)
            {
                String tip = NT_tipo(nodoActual.ChildNodes.ElementAt(7));
                Simbolo.TipoDato tipo = buscarTipoDato(tip);
                return new ArrayPascal(identificador.ToString().Split(' ')[0], metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), metodoOperacion(nodoActual.ChildNodes.ElementAt(4)), tipo, tip);
            }
            return null;
        }
        private InstruccionIf metodoIf(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 9)
            {
                return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), Instrucciones(nodoActual.ChildNodes.ElementAt(4)), metodoListaElseIf(nodoActual.ChildNodes.ElementAt(7)), metodoElse(nodoActual.ChildNodes.ElementAt(8)));
            }
            else if (nodoActual.ChildNodes.Count == 8)
            {
                String operador = nodoActual.ChildNodes.ElementAt(7).Term.Name.ToString().Split(' ')[0];
                switch (operador)
                {
                    case "NT_else":
                        return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), Instrucciones(nodoActual.ChildNodes.ElementAt(4)), null, metodoElse(nodoActual.ChildNodes.ElementAt(7)));
                    case "NT_lista_else_if":
                        return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), Instrucciones(nodoActual.ChildNodes.ElementAt(4)), metodoListaElseIf(nodoActual.ChildNodes.ElementAt(7)), null);
                }
            }
            else if (nodoActual.ChildNodes.Count == 7)
            {
                return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), Instrucciones(nodoActual.ChildNodes.ElementAt(3)), null, null);
            }
            else if (nodoActual.ChildNodes.Count == 6)
            {
                LinkedList<Instruccion> sentenciasIf = new LinkedList<Instruccion>();
                sentenciasIf.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(3)));
                return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), sentenciasIf, metodoListaElseIf(nodoActual.ChildNodes.ElementAt(4)), metodoElse(nodoActual.ChildNodes.ElementAt(5)));
            }
            else if (nodoActual.ChildNodes.Count == 5)
            {
                String operador = nodoActual.ChildNodes.ElementAt(4).Term.Name.ToString().Split(' ')[0];
                LinkedList<Instruccion> sentenciasIf = new LinkedList<Instruccion>();
                sentenciasIf.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(3)));
                switch (operador)
                {
                    case "NT_else":
                        return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), sentenciasIf, null, metodoElse(nodoActual.ChildNodes.ElementAt(4)));
                    case "NT_lista_else_if":
                        return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), sentenciasIf, metodoListaElseIf(nodoActual.ChildNodes.ElementAt(4)), null);
                }
            }
            else if (nodoActual.ChildNodes.Count == 4)
            {
                LinkedList<Instruccion> sentenciasIf = new LinkedList<Instruccion>();
                sentenciasIf.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(3)));
                return new InstruccionIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), sentenciasIf, null, null);
            }
            return null;
        }

        private LinkedList<InstruccionElseIf> metodoListaElseIf(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                LinkedList<InstruccionElseIf> listaElseIf = metodoListaElseIf(nodoActual.ChildNodes.ElementAt(0));
                listaElseIf.AddLast(metodoElseIf(nodoActual.ChildNodes.ElementAt(1)));
                return listaElseIf;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<InstruccionElseIf> listaElseIf = new LinkedList<InstruccionElseIf>();
                listaElseIf.AddLast(metodoElseIf(nodoActual.ChildNodes.ElementAt(0)));
                return listaElseIf;
            }
            return null;
        }

        private InstruccionElseIf metodoElseIf(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 8)
            {
                return new InstruccionElseIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(2)), Instrucciones(nodoActual.ChildNodes.ElementAt(5)));
            }
            else if (nodoActual.ChildNodes.Count == 5)
            {
                LinkedList<Instruccion> sentenciasElseIf = new LinkedList<Instruccion>();
                sentenciasElseIf.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(4)));
                return new InstruccionElseIf(metodoOperacion(nodoActual.ChildNodes.ElementAt(2)), sentenciasElseIf);
            }
            return null;
        }

        private InstruccionElse metodoElse(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 5)
            {
                return new InstruccionElse(Instrucciones(nodoActual.ChildNodes.ElementAt(2)));
            }
            else if (nodoActual.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> sentenciasElse = new LinkedList<Instruccion>();
                sentenciasElse.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(1)));
                return new InstruccionElse(sentenciasElse);
            }
            return null;
        }
        private InstruccionSwitch metodoSwitch(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 7)
            {
                return new InstruccionSwitch(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), metodoListaCase(nodoActual.ChildNodes.ElementAt(3)), metodoElse(nodoActual.ChildNodes.ElementAt(4)));
            }
            return null;
        }

        private LinkedList<InstruccionCase> metodoListaCase(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 2)
            {
                LinkedList<InstruccionCase> listaCase = metodoListaCase(nodoActual.ChildNodes.ElementAt(0));
                listaCase.AddLast(metodoCase(nodoActual.ChildNodes.ElementAt(1)));
                return listaCase;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<InstruccionCase> listaCase = new LinkedList<InstruccionCase>();
                listaCase.AddLast(metodoCase(nodoActual.ChildNodes.ElementAt(0)));
                return listaCase;
            }
            return null;
        }

        private InstruccionCase metodoCase(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 6)
            {
                return new InstruccionCase(valoresParametros(nodoActual.ChildNodes.ElementAt(0)), Instrucciones(nodoActual.ChildNodes.ElementAt(3)));
            }
            else if (nodoActual.ChildNodes.Count == 3)
            {
                LinkedList<Instruccion> sentencia = new LinkedList<Instruccion>();
                sentencia.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(2)));
                return new InstruccionCase(valoresParametros(nodoActual.ChildNodes.ElementAt(0)), sentencia);
            }
            return null;
        }

        private InstruccionWhile metodoWhile(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 7)
            {
                return new InstruccionWhile(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), Instrucciones(nodoActual.ChildNodes.ElementAt(4)));
            }
            else if (nodoActual.ChildNodes.Count == 4)
            {
                LinkedList<Instruccion> senteciasWhile = new LinkedList<Instruccion>();
                senteciasWhile.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(3)));
                return new InstruccionWhile(metodoOperacion(nodoActual.ChildNodes.ElementAt(1)), senteciasWhile);
            }
            return null;
        }

        private InstruccionFor metodoFor(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 9)
            {
                String tipo = nodoActual.ChildNodes.ElementAt(2).Term.Name.ToString().Split(' ')[0].ToLower();
                if (tipo.Equals("to"))
                {
                    Operacion valu = new Operacion((Object)masgfor(nodoActual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.IDENTIFICADOR);
                    Operacion lim = metodoOperacion(nodoActual.ChildNodes.ElementAt(3));
                    Operacion incremento = new Operacion(valu, lim, Operacion.Tipo_operacion.MAYOR_QUE);

                    return new InstruccionFor(metodoAsignacionFor(nodoActual.ChildNodes.ElementAt(1)), incremento, Instrucciones(nodoActual.ChildNodes.ElementAt(6)), InstruccionFor.TipoFor.INCREMENTO, masgfor(nodoActual.ChildNodes.ElementAt(1)));
                }
                else if (tipo.Equals("downto"))
                {
                    Operacion valu = new Operacion((Object)masgfor(nodoActual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.IDENTIFICADOR);
                    Operacion lim = metodoOperacion(nodoActual.ChildNodes.ElementAt(3));
                    Operacion incremento = new Operacion(valu, lim, Operacion.Tipo_operacion.MENOR_QUE);
                    return new InstruccionFor(metodoAsignacionFor(nodoActual.ChildNodes.ElementAt(1)), incremento, Instrucciones(nodoActual.ChildNodes.ElementAt(6)), InstruccionFor.TipoFor.DECREMENTO, masgfor(nodoActual.ChildNodes.ElementAt(1)));
                }
            }
            else if (nodoActual.ChildNodes.Count == 6)
            {
                LinkedList<Instruccion> sentenciasFor = new LinkedList<Instruccion>();
                sentenciasFor.AddLast(metodoInstruccion(nodoActual.ChildNodes.ElementAt(5)));
                String tipo = nodoActual.ChildNodes.ElementAt(2).Term.Name.ToString().Split(' ')[0].ToLower();
                if (tipo.Equals("to"))
                {
                    Operacion valu = new Operacion((Object)masgfor(nodoActual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.IDENTIFICADOR);
                    Operacion lim = metodoOperacion(nodoActual.ChildNodes.ElementAt(3));
                    Operacion incremento = new Operacion(valu, lim, Operacion.Tipo_operacion.MAYOR_QUE);
                    return new InstruccionFor(metodoAsignacionFor(nodoActual.ChildNodes.ElementAt(1)), incremento, sentenciasFor, InstruccionFor.TipoFor.INCREMENTO, masgfor(nodoActual.ChildNodes.ElementAt(1)));
                }
                else if (tipo.Equals("downto"))
                {
                    Operacion valu = new Operacion((Object)masgfor(nodoActual.ChildNodes.ElementAt(1)), Operacion.Tipo_operacion.IDENTIFICADOR);
                    Operacion lim = metodoOperacion(nodoActual.ChildNodes.ElementAt(3));
                    Operacion incremento = new Operacion(valu, lim, Operacion.Tipo_operacion.MENOR_QUE);
                    return new InstruccionFor(metodoAsignacionFor(nodoActual.ChildNodes.ElementAt(1)), incremento, sentenciasFor, InstruccionFor.TipoFor.DECREMENTO, masgfor(nodoActual.ChildNodes.ElementAt(1)));
                }
            }
            return null;
        }
        private String masgfor(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                return nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0];
            }
            return "";
        }
        private Asignacion metodoAsignacionFor(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                return new Asignacion(nodoActual.ChildNodes.ElementAt(0).ToString().Split(' ')[0], metodoOperacion(nodoActual.ChildNodes.ElementAt(2)));
            }
            return null;
        }

        private InstruccionRepeat metodoRepeat(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 5)
            {
                return new InstruccionRepeat(metodoOperacion(nodoActual.ChildNodes.ElementAt(3)), Instrucciones(nodoActual.ChildNodes.ElementAt(1)));
            }
            return null;
        }

        private Funcion metodoFuncion(ParseTreeNode nodoctual)
        {
            if (nodoctual.ChildNodes.Count == 11)
            {
                Simbolo.TipoDato tipo = buscarTipoDato(NT_tipo(nodoctual.ChildNodes.ElementAt(4)));
                LinkedList<ParametroFP> parametros = metodoParam(nodoctual.ChildNodes.ElementAt(2));
                Object val = valorDefecto(tipo, "");
                return new Funcion(nodoctual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], tipo, val, parametros, Instrucciones(nodoctual.ChildNodes.ElementAt(6)), Instrucciones(nodoctual.ChildNodes.ElementAt(8)), NT_tipo(nodoctual.ChildNodes.ElementAt(4)));
            }
            return null;
        }

        private Procedimiento metodoProcedmiento(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 9)
            {
                LinkedList<ParametroFP> parametros = metodoParam(nodoActual.ChildNodes.ElementAt(2));
                return new Procedimiento(nodoActual.ChildNodes.ElementAt(1).ToString().Split(' ')[0], parametros, Instrucciones(nodoActual.ChildNodes.ElementAt(4)), Instrucciones(nodoActual.ChildNodes.ElementAt(6)));
            }
            return null;
        }

        private LinkedList<ParametroFP> metodoParam(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                return metodoParametrosFuncionProcedmiento(nodoActual.ChildNodes.ElementAt(1));
            }
            return null;
        }

        private LinkedList<ParametroFP> metodoParametrosFuncionProcedmiento(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                LinkedList<ParametroFP> campos = metodoParametrosFuncionProcedmiento(nodoActual.ChildNodes.ElementAt(0));
                LinkedList<ParametroFP> camp = metodoParametroFP(nodoActual.ChildNodes.ElementAt(2));
                foreach (ParametroFP item in camp)
                {
                    campos.AddLast(item);
                }
                return campos;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<ParametroFP> campos = new LinkedList<ParametroFP>();
                LinkedList<ParametroFP> camp = metodoParametroFP(nodoActual.ChildNodes.ElementAt(0));
                foreach (ParametroFP item in camp)
                {
                    campos.AddLast(item);
                }
                return campos;
            }
            return null;
        }

        private LinkedList<ParametroFP> metodoParametroFP(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 4)
            {
                LinkedList<String> vars = listaVariables(nodoActual.ChildNodes.ElementAt(1));
                Simbolo.TipoDato tipo = buscarTipoDato(NT_tipo(nodoActual.ChildNodes.ElementAt(3)));
                LinkedList<ParametroFP> valores = new LinkedList<ParametroFP>();
                foreach (String id in vars)
                {
                    ParametroFP parametro = new ParametroFP(id, tipo, NT_tipo(nodoActual.ChildNodes.ElementAt(3)), ParametroFP.TipoValor.REFERENCIA);
                    valores.AddLast(parametro);
                }
                return valores;
            }
            else if (nodoActual.ChildNodes.Count == 3)
            {
                LinkedList<String> vars = listaVariables(nodoActual.ChildNodes.ElementAt(0));
                Simbolo.TipoDato tipo = buscarTipoDato(NT_tipo(nodoActual.ChildNodes.ElementAt(2)));
                LinkedList<ParametroFP> valores = new LinkedList<ParametroFP>();
                foreach (String id in vars)
                {
                    ParametroFP parametro = new ParametroFP(id, tipo, NT_tipo(nodoActual.ChildNodes.ElementAt(2)), ParametroFP.TipoValor.VALOR);
                    valores.AddLast(parametro);
                }
                return valores;
            }
            return null;
        }
        private InstruccionImprimir metodoImprimir(ParseTreeNode nodoActual, InstruccionImprimir.TipoImprimir tipo)
        {
            if (nodoActual.ChildNodes.Count == 5)
            {
                LinkedList<Operacion> operaciones = imprimirDato(nodoActual.ChildNodes.ElementAt(2));
                return new InstruccionImprimir(tipo, operaciones);
            }
            return null;
        }

        private LinkedList<Operacion> imprimirDato(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 3)
            {
                LinkedList<Operacion> operaciones = imprimirDato(nodoActual.ChildNodes.ElementAt(0));
                operaciones.AddLast(metodoOperacion(nodoActual.ChildNodes.ElementAt(2)));
                return operaciones;
            }
            else if (nodoActual.ChildNodes.Count == 1)
            {
                LinkedList<Operacion> operaciones = new LinkedList<Operacion>();
                operaciones.AddLast(metodoOperacion(nodoActual.ChildNodes.ElementAt(0)));
                return operaciones;
            }
            return null;
        }

        private InstruccionExit metodoExit(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 5)
            {
                return new InstruccionExit(NT_opExit(nodoActual.ChildNodes.ElementAt(2)));
            }
            return null;
        }
        private Operacion NT_opExit(ParseTreeNode nodoActual)
        {
            if (nodoActual.ChildNodes.Count == 1)
            {
                return metodoOperacion(nodoActual.ChildNodes.ElementAt(0));
            }
            return null;
        }
    }
}
