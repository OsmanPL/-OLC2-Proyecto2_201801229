using System;
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
using System.Diagnostics;

namespace _OLC2_Proyecto1_201801229.Analizador
{
    class Reporte
    {
        int i = 0;
        public void graficarArbol(ParseTreeNode raiz)
        {
            String arbolAST = "digraph ArbolAST{\n";
            arbolAST += ast(i,raiz);
            arbolAST += "}";
            Graficador graficar = new Graficador();
            graficar.graficar(arbolAST);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\arbolAST.svg")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        private String ast(int j, ParseTreeNode actual)
        {
            String grafica="id"+j +"[label=\""+actual.ToString()+"\"];\n";
            i++;
            for (int n=0;n<actual.ChildNodes.Count;n++)
            {
                grafica += "id" + j + " -> id" + i+";\n";
                grafica += ast(i,actual.ChildNodes.ElementAt(n));
            }
            return grafica;
        }

        public void Html_Errores(LinkedList<Error> listaError)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>ERRORES ENCONTRADOS</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Descripcion" +
            "</strong></td>" +

            "<td><strong>Tipo Error" +
            "</strong></td>" +

            "<td><strong>Fila" +
            "</strong></td>" +

            "<td><strong>Columna" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;

            for (int i = 0; i < listaError.Count; i++)
            {
                Error sen_pos = listaError.ElementAt(i);

                tempo_tokens = "";
                tempo_tokens = "<tr>" +

                "<td>" + sen_pos.Err +
                "</td>" +

                "<td>" + sen_pos.Tipo +
                "</td>" +

                "<td>" + sen_pos.Linea +
                "</td>" +

                "<td>" + sen_pos.Columna +
                "</td>" +

                "</tr>";
                Cad_tokens = Cad_tokens + tempo_tokens;

            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Reporte_de_Errores.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Reporte_de_Errores.html")
            {
                UseShellExecute = true
            };
            p.Start();


        }

        public void HTML_ts(TablaSimbolos ts)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>Tabla de Simbolos</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Id" +
            "</strong></td>" +

            "<td><strong>Tipo Dato" +
            "</strong></td>" +

             "<td><strong>Tipo Simbolo" +
            "</strong></td>" +


            "<td><strong>Valor" +
            "</strong></td>" +

            "<td><strong>Entorno" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens;
            foreach (Simbolo sim in ts)
            {

                tempo_tokens = "";
                if (sim.TipoVar == Simbolo.TipoVarariable.CONST)
                {
                    tempo_tokens = "<tr>" +

                    "<td>" + sim.Id +
                    "</td>" +

                    "<td>" + sim.Tipo.ToString() +
                    "</td>" +

                    "<td>" + sim.TipoVar.ToString() +
                    "</td>" +

                    "<td>" + sim.Valor.ToString() +
                    "</td>" +

                    "<td>" + sim.Entorno +
                    "</td>" +

                    "</tr>";
                }
                else
                {
                    if (sim.Tipo == Simbolo.TipoDato.IDENTIFICADOR)
                    {
                        tempo_tokens = "<tr>" +

                        "<td>" + sim.Id +
                        "</td>" +

                        "<td>" + sim.Type+
                        "</td>" +

                        "<td>" + sim.TipoVar.ToString() +
                        "</td>" +

                        "<td>" + sim.Valor.ToString() +
                        "</td>" +

                        "<td>" + sim.Entorno +
                        "</td>" +

                        "</tr>";
                    }
                    else
                    {
                        tempo_tokens = "<tr>" +

                       "<td>" + sim.Id +
                       "</td>" +

                       "<td>" + sim.Tipo.ToString() +
                       "</td>" +

                       "<td>" + sim.TipoVar.ToString() +
                       "</td>" +

                       "<td>" + sim.Valor.ToString() +
                       "</td>" +

                       "<td>" + sim.Entorno +
                       "</td>" +

                       "</tr>";
                    }
                }
                Cad_tokens = Cad_tokens + tempo_tokens;
            }

            Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";

            File.WriteAllText("C:\\compiladores2\\Tabla_Simbolos.html", Contenido_html);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Tabla_Simbolos.html")
            {
                UseShellExecute = true
            };
            p.Start();


        }

    }
}
