using System;
using System.Collections.Generic;
using System.Text;
using _OLC2_Proyecto1_201801229.Interfaces;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Elemento_Stack
    {
        String identificador;
        Simbolo.TipoDato tipo;
        int referenciaStack,referenciaHeap;
        Elemento_Stack siguiente;
        bool fu;

        public Elemento_Stack(String identificador, Simbolo.TipoDato tipo, int referenciaStack, int referenciaHeap,Elemento_Stack siguiente, bool fu)
        {
            this.identificador = identificador;
            this.tipo = tipo;
            this.referenciaStack = referenciaStack;
            this.ReferenciaHeap = referenciaHeap;
            this.siguiente = siguiente;
            this.Fu = fu;
        }

        public string Identificador { get => identificador; set => identificador = value; }
        public int ReferenciaStack { get => referenciaStack; set => referenciaStack = value; }
        public int ReferenciaHeap { get => referenciaHeap; set => referenciaHeap = value; }
        public bool Fu { get => fu; set => fu = value; }
        internal Simbolo.TipoDato Tipo { get => tipo; set => tipo = value; }
        internal Elemento_Stack Siguiente { get => siguiente; set => siguiente = value; }
    }
}
