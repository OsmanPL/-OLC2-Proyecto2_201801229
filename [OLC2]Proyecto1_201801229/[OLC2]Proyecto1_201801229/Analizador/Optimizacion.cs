using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Analizador
{
    class Optimizacion
    {
        public enum TipoOptimizacion
        {
            MIRILLA,
            BLOQUES
        }
        public enum ReglaOptimizacion
        {
            REGLA_1,
            REGLA_2,
            REGLA_3,
            REGLA_4,
            REGLA_5,
            REGLA_6,
            REGLA_7,
            REGLA_8,
            REGLA_9,
            REGLA_10,
            REGLA_11,
            REGLA_12,
            REGLA_13,
            REGLA_14,
            REGLA_15,
            REGLA_16
        }

        int id, fila;
        String cod_agregado, cod_eliminado, cod_entrada, cod_salida;
        ReglaOptimizacion regla;
        TipoOptimizacion tipo;

        public int Id { get => id; set => id = value; }
        public int Fila { get => fila; set => fila = value; }
        public string Cod_agregado { get => cod_agregado; set => cod_agregado = value; }
        public string Cod_eliminado { get => cod_eliminado; set => cod_eliminado = value; }
        public string Cod_entrada { get => cod_entrada; set => cod_entrada = value; }
        public string Cod_salida { get => cod_salida; set => cod_salida = value; }
        internal ReglaOptimizacion Regla { get => regla; set => regla = value; }
        internal TipoOptimizacion Tipo { get => tipo; set => tipo = value; }

        public Optimizacion(int id, int fila, String cod_agregado, String cod_eliminado, String cod_entrada, String cod_salida, ReglaOptimizacion regla, TipoOptimizacion tipo)
        {
            this.Id = id;
            this.Fila = fila;
            this.Cod_agregado = cod_agregado;
            this.Cod_eliminado = cod_eliminado;
            this.Cod_entrada = cod_entrada;
            this.Cod_salida = cod_salida;
            this.Regla = regla;
            this.Tipo = tipo;
        }
        public Optimizacion()
        {

        }
    }
}
