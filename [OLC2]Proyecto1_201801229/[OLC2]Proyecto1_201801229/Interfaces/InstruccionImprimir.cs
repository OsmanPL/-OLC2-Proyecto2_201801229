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
                                Form1.Consola.Text += valor.ToString();
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
                                Form1.Consola.Text += valor.ToString();
                            }
                        }
                    }
                    Form1.Consola.Text += "\n";
                    break;
            }
            return null;
        }
    }
}
