using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class GraficarTS : Instruccion
    {
        public GraficarTS()
        {

        }
        public Object ejecutar(TablaSimbolos ts)
        {
            Form1.Consola.Text += "\n";
            Form1.Consola.Text += "-----------------------------------------------------------\n";
            Form1.Consola.Text += "|   Identificador  | Tipo de Dato  | Tipo de Simbolo |   Valor   |    Entorno    |\n";
            foreach (Simbolo sim in ts)
            {
                Form1.Consola.Text += "|"+sim.Id+"|"+sim.Tipo.ToString()+"|"+sim.TipoVar.ToString()+"|"+sim.Valor.ToString()+"|"+sim.Entorno+"|\n";
            }
            Form1.Consola.Text += "-----------------------------------------------------------\n";
            Form1.Consola.Text += "\n";
            return null;
        }
    }
}
