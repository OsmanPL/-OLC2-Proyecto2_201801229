using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Elemento_Heap
    {
        int valor;
        int referenciaHeap;
        Elemento_Heap siguiente;

        public Elemento_Heap(int valor,int referenciaHeap, Elemento_Heap siguiente)
        {
            this.valor = valor;
            this.ReferenciaHeap = referenciaHeap;
            this.siguiente = siguiente;
        }

        public int Valor { get => valor; set => valor = value; }
        public int ReferenciaHeap { get => referenciaHeap; set => referenciaHeap = value; }
        internal Elemento_Heap Siguiente { get => siguiente; set => siguiente = value; }

    }
}
