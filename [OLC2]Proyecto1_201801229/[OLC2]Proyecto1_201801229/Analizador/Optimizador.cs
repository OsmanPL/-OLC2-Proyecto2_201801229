using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Analizador
{
    class Optimizador
    {
        LinkedList<Optimizacion> listaoptimizacion = new LinkedList<Optimizacion>();
        LinkedList<String> salida = new LinkedList<String>();

        public Optimizador()
        {

        }

        public LinkedList<string> Salida { get => salida; set => salida = value; }
        internal LinkedList<Optimizacion> Listaoptimizacion { get => listaoptimizacion; set => listaoptimizacion = value; }

        public String Optimizacion(String codigoEntrada)
        {
            Listaoptimizacion = new LinkedList<Optimizacion>();
            Salida = new LinkedList<string>();
            String codigoSalida = "";
            String[] lineas = codigoEntrada.Split("\n");
            int id = 0;
            for (int i = 0; i < lineas.Length; i++)
            {
                String lineaActual = lineas[i];
                Optimizacion optimizacion = new Optimizacion();
                bool op = false;
                if (lineaActual.StartsWith("T") && lineaActual.Contains("="))
                {
                    String[] separar = lineaActual.Split("=");
                    String temporal = separar[0].Trim();
                    String valor = separar[1].Trim().TrimEnd(';');
                    if (valor.Contains("+"))
                    {
                        String[] valores = valor.Split("+");
                        String val1 = valores[0];
                        String val2 = valores[1];
                        if ((val1.Equals("0") || val2.Equals("0")) && !(val1.Equals("SP") || val2.Equals("SP")))
                        {
                            if (temporal.Equals(val1) || temporal.Equals(val2))
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = lineaActual;
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Cod_salida = "";
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_6;
                                op = true;
                                lineaActual = "";

                            }
                            else
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = (val1.Equals("0") ? "0+" : "+0");
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_10;
                                lineaActual = temporal + "=" + (val1.Equals("0") ? val2 : val1) + ";";
                                optimizacion.Cod_salida = lineaActual;
                                op = true;
                            }
                        }
                    }
                    else if (valor.Contains("-"))
                    {
                        String[] valores = valor.Split("-");
                        String val1 = valores[0];
                        String val2 = valores[1];
                        if ((val1.Equals("0") || val2.Equals("0")) && !(val1.Equals("SP") || val2.Equals("SP")))
                        {
                            if (temporal.Equals(val1) || temporal.Equals(val2))
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = lineaActual;
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Cod_salida = "";
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_7;
                                op = true;
                                lineaActual = "";

                            }
                            else
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = (val1.Equals("0") ? "0-" : "-0");
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_11;
                                lineaActual = temporal + "=" + (val1.Equals("0") ? val2 : val1) + ";";
                                optimizacion.Cod_salida = lineaActual;
                                op = true;
                            }
                        }
                    }
                    else if (valor.Contains("*"))
                    {
                        String[] valores = valor.Split("*");
                        String val1 = valores[0];
                        String val2 = valores[1];
                        if ((val1.Equals("1") || val2.Equals("1")))
                        {
                            if (temporal.Equals(val1) || temporal.Equals(val2))
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = lineaActual;
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Cod_salida = "";
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_8;
                                op = true;
                                lineaActual = "";

                            }
                            else
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = (val1.Equals("1") ? "1*" : "*1");
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_12;
                                lineaActual = temporal + "=" + (val1.Equals("1") ? val2 : val1) + ";";
                                optimizacion.Cod_salida = lineaActual;
                                op = true;
                            }
                        }else if ((val1.Equals("2") || val2.Equals("2")))
                        {
                            optimizacion.Id = id;
                            id++;
                            optimizacion.Fila = i + 1;
                            optimizacion.Cod_agregado =  (val1.Equals("2") ? val2+"+" : "+"+val1);
                            optimizacion.Cod_eliminado = (val1.Equals("2") ? "2*" : "*2");
                            optimizacion.Cod_entrada = lineaActual;
                            optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                            optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_14;
                            lineaActual = temporal + "=" + (val1.Equals("2") ? val2 : val1) + "+" + (val1.Equals("2") ? val2 : val1) + ";";
                            optimizacion.Cod_salida = lineaActual;
                            op = true;
                        }
                        else if ((val1.Equals("0") || val2.Equals("0")))
                        {
                            optimizacion.Id = id;
                            id++;
                            optimizacion.Fila = i + 1;
                            optimizacion.Cod_agregado = "0";
                            optimizacion.Cod_eliminado = val1+"*"+val2;
                            optimizacion.Cod_entrada = lineaActual;
                            optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                            optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_15;
                            lineaActual = temporal + "=0;";
                            optimizacion.Cod_salida = lineaActual;
                            op = true;
                        }
                    }
                    else if (valor.Contains("/"))
                    {
                        String[] valores = valor.Split("/");
                        String val1 = valores[0];
                        String val2 = valores[1];
                        if (val2.Equals("1"))
                        {
                            if (temporal.Equals(val1))
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado = lineaActual;
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Cod_salida = "";
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_9;
                                op = true;
                                lineaActual = "";

                            }
                            else
                            {
                                optimizacion.Id = id;
                                id++;
                                optimizacion.Fila = i + 1;
                                optimizacion.Cod_agregado = "";
                                optimizacion.Cod_eliminado =  "/1";
                                optimizacion.Cod_entrada = lineaActual;
                                optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                                optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_13;
                                lineaActual = temporal + "=" +  val1 + ";";
                                optimizacion.Cod_salida = lineaActual;
                                op = true;
                            }
                        }
                        else if ((val1.Equals("0")))
                        {
                            optimizacion.Id = id;
                            id++;
                            optimizacion.Fila = i + 1;
                            optimizacion.Cod_agregado = "0";
                            optimizacion.Cod_eliminado = val1 + "/" + val2;
                            optimizacion.Cod_entrada = lineaActual;
                            optimizacion.Tipo = Analizador.Optimizacion.TipoOptimizacion.MIRILLA;
                            optimizacion.Regla = Analizador.Optimizacion.ReglaOptimizacion.REGLA_16;
                            lineaActual = temporal + "=0;";
                            optimizacion.Cod_salida = lineaActual;
                            op = true;
                        }
                    }
                }
                if (op)
                {
                    Listaoptimizacion.AddLast(optimizacion);
                }
                Salida.AddLast(lineaActual + "\n");
            }

            foreach (String linea in Salida)
            {
                codigoSalida += linea;
            }
            return codigoSalida;
        }
    }
}
