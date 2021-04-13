using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace _OLC2_Proyecto1_201801229.Analizador
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            IdentifierTerminal IDENTIFICADOR = new IdentifierTerminal("ID");
            StringLiteral CADENA = new StringLiteral("cadena", "\'");
            var DECIMAL = new RegexBasedTerminal("decimal", "[0-9]+'.'[0-9]+");
            var NUMERO = new NumberLiteral("numero");

            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "{*", "*}");
            CommentTerminal comentarioMultiple = new CommentTerminal("comentarioMultiple", "{", "}");
            #endregion

            #region Terminales
            //Simbolos Numericos
            var TK_SUM = ToTerm("+");
            var TK_RES = ToTerm("-");
            var TK_DIV = ToTerm("/");
            var TK_MUL = ToTerm("*");
            var TK_MOD = ToTerm("%");

            //Simbolos Agrupacion
            var TK_PARIZQ = ToTerm("(");
            var TK_PARDER = ToTerm(")");
            var TK_CORIZQ = ToTerm("[");
            var TK_CORDER = ToTerm("]");

            //Simbolos Relacionales
            var TK_MAY = ToTerm(">");
            var TK_MEN = ToTerm("<");
            var TK_IGUAL = ToTerm("=");
            var TK_MAYIGUAL = ToTerm(">=");
            var TK_MENIGUAL = ToTerm("<=");
            var TK_DIFERENTE = ToTerm("<>");

            //Simbolos Logicos
            var TK_AND = ToTerm("and");
            var TK_OR = ToTerm("or");
            var TK_NOT = ToTerm("not");

            //Simbolos de Puntuacion
            var TK_PUNTO = ToTerm(".");
            var TK_PYCOMA = ToTerm(";");
            var TK_COMA = ToTerm(",");
            var TK_DOSPUNTOS = ToTerm(":");
            var TK_IGUALAR = ToTerm(":=");

            //Tipos de Datos
            var TK_INTEGER = ToTerm("integer");
            var TK_REAL = ToTerm("real");
            var TK_BOOLEAN = ToTerm("boolean");
            var TK_STRING = ToTerm("string");

            //Palabras Reservadas
            var TK_TRUE = ToTerm("true");
            var TK_FALSE = ToTerm("false");
            var TK_VAR = ToTerm("var");
            var TK_TYPE = ToTerm("type");
            var TK_BEGIN = ToTerm("begin");
            var TK_END = ToTerm("end");
            var TK_PROGRAM = ToTerm("program");
            var TK_OBJECT = ToTerm("object");
            var TK_ARRAY = ToTerm("array");
            var TK_OF = ToTerm("of");
            var TK_TO = ToTerm("to");
            var TK_DOWNTO = ToTerm("downto");
            var TK_IF = ToTerm("if");
            var TK_CONST = ToTerm("const");
            var TK_THEN = ToTerm("then");
            var TK_ELSE = ToTerm("else");
            var TK_CASE = ToTerm("case");
            var TK_WHILE = ToTerm("while");
            var TK_DO = ToTerm("do");
            var TK_REPEAT = ToTerm("repeat");
            var TK_UNTIL = ToTerm("until");
            var TK_FOR = ToTerm("for");
            var TK_BREAK = ToTerm("break");
            var TK_CONTINUE = ToTerm("continue");
            var TK_FUNCTION = ToTerm("function");
            var TK_PROCEDURE = ToTerm("procedure");
            var TK_WRITE = ToTerm("write");
            var TK_WRITELN = ToTerm("writeln");
            var TK_EXIT = ToTerm("exit");
            var TK_GRAFICAR_TS = ToTerm("graficar_ts");

            RegisterOperators(1, TK_IGUAL, TK_DIFERENTE, TK_MAY, TK_MAYIGUAL, TK_MEN, TK_MENIGUAL);
            RegisterOperators(2, TK_SUM, TK_RES, TK_OR);
            RegisterOperators(3, TK_MUL, TK_DIV, TK_MOD, TK_AND);
            RegisterOperators(4, TK_NOT);

            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioMultiple);
            NonGrammarTerminals.Add(comentarioBloque);
            #endregion

            #region NoTerimnales
            NonTerminal inicio = new NonTerminal("inicio");
            NonTerminal NT_instrucciones = new NonTerminal("NT_instrucciones");
            NonTerminal NT_instruccion = new NonTerminal("NT_instruccion");
            NonTerminal NT_sentencia = new NonTerminal("NT_sentencia");
            NonTerminal NT_sentencias = new NonTerminal("NT_sentencias");
            NonTerminal NT_vp = new NonTerminal("NT_vp");
            NonTerminal NT_PYC = new NonTerminal("NT_PYC");

            //Instrucciones
            NonTerminal NT_program = new NonTerminal("NT_program");
            NonTerminal NT_declaracionVar = new NonTerminal("NT_declaracionVar");
            NonTerminal NT_declaracionConst = new NonTerminal("NT_declaracionConst");
            NonTerminal NT_type = new NonTerminal("NT_type");
            NonTerminal NT_funcion = new NonTerminal("NT_funcion");
            NonTerminal NT_procedimiento = new NonTerminal("NT_procedimiento");
            NonTerminal NT_operacion = new NonTerminal("NT_operacion");
            NonTerminal NT_asignacion = new NonTerminal("NT_asignacion");

            //Declaracion
            NonTerminal NT_tipo = new NonTerminal("NT_tipo");
            NonTerminal NT_listaVariables = new NonTerminal("NT_listaVariables");
            NonTerminal NT_inicializarVariable = new NonTerminal("NT_inicializarVariable");
            NonTerminal NT_valorvacio = new NonTerminal("NT_valorvacio");
            NonTerminal NT_listDeclaVar = new NonTerminal("NT_listDeclaVar");
            NonTerminal NT_listaDeclaraciones = new NonTerminal("NT_listaDeclaraciones");
            NonTerminal NT_listDeclaConst = new NonTerminal("NT_listDeclaConst");

            //Type
            NonTerminal NT_objeto = new NonTerminal("NT_objeto");

            //Objeto
            NonTerminal NT_declaracionObjeto = new NonTerminal("NT_declaracionObjeto");
            NonTerminal NT_camposObjeto = new NonTerminal("NT_camposObjeto");
            NonTerminal NT_campos = new NonTerminal("NT_campos");

            //Operaciones
            NonTerminal NT_operacionRelacional = new NonTerminal("NT_operacionRelacional");
            NonTerminal NT_expresion = new NonTerminal("NT_expresion");

            //Expresion
            NonTerminal NT_valor = new NonTerminal("NT_valor");
            NonTerminal NT_valores = new NonTerminal("NT_valores");

            //Arrays
            NonTerminal NT_array = new NonTerminal("NT_array");

            //If
            NonTerminal NT_if = new NonTerminal("NT_if");
            NonTerminal NT_else = new NonTerminal("NT_else");
            NonTerminal NT_elseif = new NonTerminal("NT_elseif");
            NonTerminal NT_lista_else_if = new NonTerminal("NT_lista_else_if");

            //Case
            NonTerminal NT_sentenciaCase = new NonTerminal("NT_sentenciaCase");
            NonTerminal NT_case = new NonTerminal("NT_case");
            NonTerminal NT_listaCase = new NonTerminal("NT_listaCase");
            NonTerminal NT_listaExpresionesCase = new NonTerminal("NT_listaExpresionesCase");

            //While
            NonTerminal NT_while = new NonTerminal("NT_while");

            //For
            NonTerminal NT_for = new NonTerminal("NT_for");
            NonTerminal NT_asigfor = new NonTerminal("NT_asifor");

            //Repeat
            NonTerminal NT_repeat = new NonTerminal("NT_repeat");

            //Sentencias Ciclo
            NonTerminal NT_sentenciasCiclo = new NonTerminal("NT_sentenciasCiclo");
            NonTerminal NT_sentenciaCiclo = new NonTerminal("NT_sentenciaCiclo");

            //Funcion Procedimiento
            NonTerminal NT_parametros = new NonTerminal("NT_parametros");
            NonTerminal NT_parametro = new NonTerminal("NT_parametro");
            NonTerminal NT_param = new NonTerminal("NT_param");
            NonTerminal NT_instruccionesFP = new NonTerminal("NT_instruccionesFP");
            NonTerminal NT_instruccionFP = new NonTerminal("NT_instruccionFP");

            //Nativos
            NonTerminal NT_write = new NonTerminal("NT_write");
            NonTerminal NT_writeln = new NonTerminal("NT_writeln");
            NonTerminal NT_exit = new NonTerminal("NT_exit");
            NonTerminal NT_graficar_ts = new NonTerminal("NT_graficar_ts");
            NonTerminal NT_imprimir = new NonTerminal("NT_imprimir");
            NonTerminal NT_opExit = new NonTerminal("NT_opExit");

            //Llamada
            NonTerminal NT_llamadaFuncion = new NonTerminal("NT_llamadaFuncion");
            NonTerminal NT_llamadaProdecimiento = new NonTerminal("NT_llamadaProcedimiento");
            #endregion

            #region Gramatica
            //Inicio Gramatica
            inicio.Rule = NT_program;
            inicio.ErrorRule = SyntaxError + TK_PYCOMA;

            NT_PYC.Rule = TK_PYCOMA
                | Empty;

            //Program
            NT_program.Rule = NT_vp + NT_instrucciones + TK_BEGIN + NT_sentencias + TK_END + TK_PUNTO;

            //VP
            NT_vp.Rule = TK_PROGRAM + IDENTIFICADOR + TK_PYCOMA
                | Empty;

            //Instrucciones
            NT_instrucciones.Rule = NT_instrucciones + NT_instruccion
                | NT_instruccion
                | Empty;

            //Sentencias
            NT_sentencias.Rule = NT_sentencias + NT_sentencia
                | NT_sentencia
                | Empty;

            //Sentencia
            NT_sentencia.Rule = NT_asignacion
                | NT_llamadaFuncion + NT_PYC
                | NT_llamadaProdecimiento+ NT_PYC
                | NT_if
                | NT_sentenciaCase
                | NT_while
                | NT_for
                | NT_repeat
                | NT_write
                | NT_writeln
                | NT_exit
                | NT_graficar_ts
                | TK_CONTINUE + NT_PYC
                | TK_BREAK + NT_PYC;

            //Llamda Funcion
            NT_llamadaFuncion.Rule = IDENTIFICADOR + TK_PARIZQ + NT_valores + TK_PARDER;

            //LlamdaProcedimiento
            NT_llamadaProdecimiento.Rule = IDENTIFICADOR + TK_PARIZQ + TK_PARDER;

            //Instruccion
            NT_instruccion.Rule = NT_type
                | NT_listaDeclaraciones
                | NT_funcion
                | NT_procedimiento;

            //Array
            NT_array.Rule = TK_CORIZQ + NT_operacion + TK_PUNTO + TK_PUNTO + NT_operacion + TK_CORDER + TK_OF + NT_tipo + TK_PYCOMA;

            //ListaDeclaraciones
            NT_listaDeclaraciones.Rule = TK_VAR + NT_listDeclaVar
                | TK_CONST + NT_listDeclaConst;

            //List Decla
            NT_listDeclaVar.Rule = NT_listDeclaVar + NT_declaracionVar
                |NT_declaracionVar;

            NT_listDeclaConst.Rule = NT_listDeclaConst + NT_declaracionConst
               | NT_declaracionConst;

            NT_declaracionConst.Rule = IDENTIFICADOR + TK_IGUAL + NT_operacion + TK_PYCOMA
                | IDENTIFICADOR + TK_DOSPUNTOS + NT_tipo + TK_IGUAL + NT_operacion + TK_PYCOMA;

            //Declaracion
            NT_declaracionVar.Rule =  NT_listaVariables + TK_DOSPUNTOS + NT_tipo + TK_PYCOMA
                | NT_inicializarVariable;

            //Inicializar Variable
            NT_inicializarVariable.Rule = IDENTIFICADOR + TK_DOSPUNTOS + NT_tipo + NT_valorvacio + TK_PYCOMA;

            //Valor Vacio
            NT_valorvacio.Rule = TK_IGUAL + NT_operacion
                | Empty;

            //Lista de Variables
            NT_listaVariables.Rule = NT_listaVariables + TK_COMA + IDENTIFICADOR
                | IDENTIFICADOR;

            //Asignacion
            NT_asignacion.Rule = IDENTIFICADOR + TK_IGUALAR + NT_operacion + NT_PYC
                | IDENTIFICADOR + TK_PUNTO + IDENTIFICADOR + TK_IGUALAR + NT_operacion + NT_PYC
                | IDENTIFICADOR + TK_CORIZQ + NT_operacion + TK_CORDER + TK_IGUALAR + NT_operacion + NT_PYC;

            //Tipo
            NT_tipo.Rule = TK_STRING
                | TK_INTEGER
                | TK_REAL
                | TK_BOOLEAN
                | TK_OBJECT
                | IDENTIFICADOR;

            //Type
            NT_type.Rule = TK_TYPE + NT_objeto;

            //Objeto 
            NT_objeto.Rule = NT_declaracionObjeto + TK_VAR + NT_campos + TK_END + TK_PYCOMA
                | IDENTIFICADOR + TK_IGUAL + TK_ARRAY + NT_array;

            //Campos
            NT_campos.Rule = NT_campos + NT_camposObjeto
                | NT_camposObjeto;

            //Campos Objeto
            NT_camposObjeto.Rule = NT_listaVariables + TK_DOSPUNTOS + NT_tipo + TK_PYCOMA;

            //Declaracion Objeto
            NT_declaracionObjeto.Rule = IDENTIFICADOR + TK_IGUAL + TK_OBJECT + TK_PYCOMA;

            //Operacion Logica
            NT_operacion.Rule = NT_operacion + TK_AND + NT_operacion
                | NT_operacion + TK_OR + NT_operacion
                | TK_NOT + NT_operacion
                | TK_PARIZQ + NT_operacion + TK_PARDER
                | NT_operacionRelacional;

            //Operacion Relacional
            NT_operacionRelacional.Rule = NT_operacionRelacional + TK_IGUAL + NT_operacionRelacional
                | NT_operacionRelacional + TK_DIFERENTE + NT_operacionRelacional
                | NT_operacionRelacional + TK_MAY + NT_operacionRelacional
                | NT_operacionRelacional + TK_MEN + NT_operacionRelacional
                | NT_operacionRelacional + TK_MAYIGUAL + NT_operacionRelacional
                | NT_operacionRelacional + TK_MENIGUAL + NT_operacionRelacional
                | TK_PARIZQ + NT_operacionRelacional + TK_PARDER
                | NT_expresion;

            //Expresion
            NT_expresion.Rule = NT_expresion + TK_SUM + NT_expresion
                | NT_expresion + TK_RES + NT_expresion
                | NT_expresion + TK_MUL + NT_expresion
                | NT_expresion + TK_DIV + NT_expresion
                | NT_expresion + TK_MOD + NT_expresion
                | TK_RES + NT_expresion
                | TK_PARIZQ + NT_expresion + TK_PARDER
                | IDENTIFICADOR + TK_PARIZQ + TK_PARDER
                | IDENTIFICADOR + TK_PARIZQ + NT_valores + TK_PARDER
                | NT_valor;

            //Valores
            NT_valores.Rule = NT_valores + TK_COMA + NT_operacion
                | NT_operacion;

            //Valor
            NT_valor.Rule = NUMERO
                | DECIMAL
                | CADENA
                | TK_TRUE
                | TK_FALSE
                | IDENTIFICADOR
                | IDENTIFICADOR + TK_PUNTO + IDENTIFICADOR
                | IDENTIFICADOR + TK_CORIZQ + NT_operacion + TK_CORDER;

            //If
            NT_if.Rule = TK_IF + NT_operacion + TK_THEN + TK_BEGIN + NT_sentencias + TK_END + NT_PYC
                | TK_IF + NT_operacion + TK_THEN + TK_BEGIN + NT_sentencias + TK_END + NT_PYC + NT_else
                | TK_IF + NT_operacion + TK_THEN + TK_BEGIN + NT_sentencias + TK_END + NT_PYC + NT_lista_else_if
                | TK_IF + NT_operacion + TK_THEN + TK_BEGIN + NT_sentencias + TK_END + NT_PYC + NT_lista_else_if + NT_else
                | TK_IF + NT_operacion + TK_THEN + NT_sentencia
                | TK_IF + NT_operacion + TK_THEN + NT_sentencia + NT_else
                | TK_IF + NT_operacion + TK_THEN + NT_sentencia + NT_lista_else_if
                | TK_IF + NT_operacion + TK_THEN + NT_sentencia + NT_lista_else_if + NT_else;

            //Lista Else If
            NT_lista_else_if.Rule = NT_lista_else_if + NT_elseif
                | NT_elseif
                | Empty;

            //Else If
            NT_elseif.Rule = TK_ELSE + TK_IF + NT_operacion + TK_THEN + TK_BEGIN + NT_sentencias + TK_END + NT_PYC
                | TK_ELSE + TK_IF + NT_operacion + TK_THEN + NT_sentencia;

            //Else
            NT_else.Rule = TK_ELSE + TK_BEGIN + NT_sentencias + TK_END + NT_PYC
                | TK_ELSE + NT_sentencia
                | Empty;

            //Sentencia Case
            NT_sentenciaCase.Rule = TK_CASE + NT_operacion + TK_OF + NT_listaCase + NT_else + TK_END + TK_PYCOMA;

            //Lista Case
            NT_listaCase.Rule = NT_listaCase + NT_case
                | NT_case;

            //Case
            NT_case.Rule = NT_listaExpresionesCase + TK_DOSPUNTOS + TK_BEGIN + NT_sentencias + TK_END + TK_PYCOMA
                | NT_listaExpresionesCase + TK_DOSPUNTOS + NT_sentencia;

            //Lista Expresiones Case
            NT_listaExpresionesCase.Rule = NT_listaExpresionesCase + TK_COMA + NT_operacion
                | NT_operacion;

            //While
            NT_while.Rule = TK_WHILE + NT_operacion + TK_DO + TK_BEGIN + NT_sentenciasCiclo + TK_END + TK_PYCOMA
                | TK_WHILE + NT_operacion + TK_DO + NT_sentenciaCiclo;

            //For
            NT_for.Rule = TK_FOR + NT_asigfor + TK_TO + NT_operacion + TK_DO + TK_BEGIN + NT_sentenciasCiclo + TK_END + TK_PYCOMA
                | TK_FOR + NT_asigfor + TK_TO + NT_operacion + TK_DO + NT_sentenciaCiclo
                | TK_FOR + NT_asigfor + TK_DOWNTO + NT_operacion + TK_DO + TK_BEGIN + NT_sentenciasCiclo + TK_END + TK_PYCOMA
                | TK_FOR + NT_asigfor + TK_DOWNTO + NT_operacion + TK_DO + NT_sentenciaCiclo;

            //Asigncion For
            NT_asigfor.Rule = IDENTIFICADOR + TK_IGUALAR + NT_operacion;

            //Repat-Until
            NT_repeat.Rule = TK_REPEAT + NT_sentenciasCiclo + TK_UNTIL + NT_operacion + TK_PYCOMA;

            //Sentencias
            NT_sentenciasCiclo.Rule = NT_sentenciasCiclo + NT_sentenciaCiclo
                | NT_sentenciaCiclo
                | Empty;

            //Sentencia
            NT_sentenciaCiclo.Rule = NT_asignacion
                | NT_llamadaFuncion
                | NT_llamadaProdecimiento
                | NT_if
                | NT_sentenciaCase
                | NT_while
                | NT_for
                | NT_repeat
                | NT_write
                | NT_writeln
                | NT_exit
                | NT_graficar_ts
                | TK_CONTINUE + TK_PYCOMA
                | TK_BREAK + TK_PYCOMA;

            //Funcion
            NT_funcion.Rule = TK_FUNCTION + IDENTIFICADOR + NT_param + TK_DOSPUNTOS + NT_tipo + TK_PYCOMA + NT_instruccionesFP + TK_BEGIN + NT_sentencias + TK_END + TK_PYCOMA;

            //Param
            NT_param.Rule = TK_PARIZQ + NT_parametros + TK_PARDER
                | Empty;

            //Parametros
            NT_parametros.Rule = NT_parametros + TK_PYCOMA + NT_parametro
                | NT_parametro
                | Empty;

            //Parametro
            NT_parametro.Rule = NT_listaVariables + TK_DOSPUNTOS + NT_tipo
                | TK_VAR + NT_listaVariables + TK_DOSPUNTOS + NT_tipo;

            //Intrucciones FP
            NT_instruccionesFP.Rule = NT_instruccionesFP + NT_instruccionFP
                | NT_instruccionFP
                | Empty;

            //Instruccion FP
            NT_instruccionFP.Rule = NT_type
                | NT_listaDeclaraciones;

            //Procedimiento
            NT_procedimiento.Rule = TK_PROCEDURE + IDENTIFICADOR + NT_param + TK_PYCOMA + NT_instruccionesFP + TK_BEGIN + NT_sentencias + TK_END + TK_PYCOMA;

            //Write
            NT_write.Rule = TK_WRITE + TK_PARIZQ + NT_imprimir + TK_PARDER + NT_PYC;

            //Writeln
            NT_writeln.Rule = TK_WRITELN + TK_PARIZQ + NT_imprimir + TK_PARDER + NT_PYC;

            //Imprimir
            NT_imprimir.Rule = NT_imprimir + TK_COMA + NT_operacion
                | NT_operacion
                | Empty;

            //Exit
            NT_exit.Rule = TK_EXIT + TK_PARIZQ + NT_opExit + TK_PARDER + NT_PYC;

            //OpExit
            NT_opExit.Rule = NT_operacion
                | Empty;

            //Graficar TS
            NT_graficar_ts.Rule = TK_GRAFICAR_TS + TK_PARIZQ + TK_PARDER + NT_PYC;
            #endregion

            #region Preferencias
            this.Root = inicio;
            #endregion
        }
    }
}
