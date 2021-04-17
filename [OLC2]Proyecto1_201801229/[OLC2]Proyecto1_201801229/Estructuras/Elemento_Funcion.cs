using System;
using System.Collections.Generic;
using System.Text;
using _OLC2_Proyecto1_201801229.Interfaces;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Elemento_Funcion
    {
        Instruccion funcion;
        Elemento_Funcion siguiente;

        public Elemento_Funcion(Instruccion funcion, Elemento_Funcion siguiente)
        {
            this.funcion = funcion;
            this.siguiente = siguiente;
        }

        internal Instruccion Funcion { get => funcion; set => funcion = value; }
        internal Elemento_Funcion Siguiente { get => siguiente; set => siguiente = value; }
    }
}
