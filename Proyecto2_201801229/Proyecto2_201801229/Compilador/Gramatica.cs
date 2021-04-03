using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Proyecto2_201801229.Compilador
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
            //Inicio
            NonTerminal NT_Inicio = new NonTerminal("NT_Inicio");
            NonTerminal NT_Instrucciones = new NonTerminal("NT_Instrucciones");
            NonTerminal NT_Sentencias = new NonTerminal("NT_Sentencias");
            NonTerminal NT_Instruccion = new NonTerminal("NT_intruccion");
            NonTerminal NT_Sentencia = new NonTerminal("NT_Sentencia");

            //Program
            NonTerminal NT_Program = new NonTerminal("NT_Program");

            //Sentencias


            //Instrucciones


            #endregion

            #region Gramatica

            //Inicio
            NT_Inicio.Rule = NT_Program + NT_Instrucciones + TK_BEGIN + NT_Sentencias + TK_END + TK_PUNTO;
            NT_Inicio.ErrorRule = SyntaxError + TK_PYCOMA;

            //Program
            NT_Program.Rule = TK_PROGRAM + IDENTIFICADOR + TK_PYCOMA
                |Empty;

            //Instrucciones
            NT_Instrucciones.Rule = NT_Instrucciones + NT_Instruccion
                | NT_Instruccion
                |Empty;

            //Sentencias
            NT_Sentencias.Rule = NT_Sentencias + NT_Sentencia
                | NT_Sentencia
                | Empty;

            #endregion

            #region Preferencias
            this.Root = NT_Inicio;
            #endregion
        }
    }
}
