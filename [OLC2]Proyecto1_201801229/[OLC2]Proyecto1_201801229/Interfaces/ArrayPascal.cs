using System;
using System.Collections.Generic;
using System.Text;
using _OLC2_Proyecto1_201801229.Analizador;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class ArrayPascal:Instruccion
    {
        Operacion limInferior,limSuperior;
        Object[] arreglo;
        Simbolo.TipoDato tipo;
        String type1;
        String id;
        int limi, lims;

        public string Id { get => id; set => id = value; }
        internal Simbolo.TipoDato Tipo { get => tipo; set => tipo = value; }
        public object[] Arreglo { get => arreglo; set => arreglo = value; }
        internal Operacion LimInferior { get => limInferior; set => limInferior = value; }
        internal Operacion LimSuperior { get => limSuperior; set => limSuperior = value; }
        public string Type1 { get => type1; set => type1 = value; }
        public int Limi { get => limi; set => limi = value; }
        public int Lims { get => lims; set => lims = value; }

        public ArrayPascal(String id, Operacion limInferior, Operacion limSuperior, Simbolo.TipoDato tipo, String type1)
        {
            this.Id = id;
            this.LimInferior = limInferior;
            this.LimSuperior = limSuperior;
            this.Tipo = tipo;
            this.Type1 = type1;
        }
        public ArrayPascal(String id, Operacion limInferior, Operacion limSuperior,int limi,int lims, Simbolo.TipoDato tipo, String type1, Object[] arreglo)
        {
            this.Id = id;
            this.LimInferior = limInferior;
            this.LimSuperior = limSuperior;
            this.Tipo = tipo;
            this.Type1 = type1;
            this.Arreglo = arreglo;
            this.limi = limi;
            this.lims = lims;
        }
        public Object buscarValor(int posicion)
        {
            try 
            {

                int pos = posicion - Limi;
                return Arreglo[pos];
            } catch (Exception e)
            {
                GeneradorAST.listaErrores.AddLast(new Error("Fuera del rango del array",Error.TipoError.SEMANTICO,0,0));
                return null;
            }

               
        }
        private Object valorDefecto(Simbolo.TipoDato tipo)
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
                    return "";
            }
        }
        public Object ejecutar(TablaSimbolos ts)
        {
            bool existe = true;
            foreach (ArrayPascal ap in GeneradorAST.arrays)
            {
                if (Id.Equals(ap.Id))
                {
                    existe = false;
                }
            }
            foreach (InstruccionType ap in GeneradorAST.type)
            {
                if (Id.Equals(ap.Id))
                {
                    existe = false;
                }
            }
            if (existe)
            {
                try
                {
                    this.Limi = int.Parse(LimInferior.ejecutar(ts).ToString());
                    this.Lims = int.Parse(LimSuperior.ejecutar(ts).ToString());
                    if (Lims > Limi)
                    {
                        int pos = Lims - Limi +1;
                        Arreglo = new Object[pos];
                        Object valor = valorDefecto(Tipo);
                        for (int i = 0; i < Arreglo.Length; i++)
                        {
                            Arreglo[i] = valor;
                        }
                        GeneradorAST.arrays.AddLast(new ArrayPascal(Id, LimInferior, LimSuperior, limi,lims, Tipo, Type1, Arreglo));
                    }
                    else
                    {
                        GeneradorAST.listaErrores.AddLast(new Error("Limite inferior es mayor o igual a limite superios", Error.TipoError.SEMANTICO, 0, 0));
                    }
                }
                catch (Exception e)
                {
                    GeneradorAST.listaErrores.AddLast(new Error("Limites no retornan un entero", Error.TipoError.SEMANTICO, 0, 0));
                }
            }
            else
            {
                GeneradorAST.listaErrores.AddLast(new Error("Ya existe el nombre como arreglo o type" + Id, Error.TipoError.SEMANTICO, 0, 0));
            }
            
            

            return null;
        }
    }
}
