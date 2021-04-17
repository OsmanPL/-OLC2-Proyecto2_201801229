using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Estructura_Heap
    {
        Elemento_Heap top;

        public bool heapVacia()
        {
            return top != null ? false : true;
        }

        public void agregarHeap(Elemento_Heap nuevo)
        {
            if (heapVacia())
            {
                top = nuevo;
            }
            else
            {
                nuevo.Siguiente = top;
                top = nuevo;
            }
        }

        public Elemento_Heap buscarElementoHeap(int id)
        {
            Elemento_Heap temp = top;
            if (temp != null)
            {
                while (temp != null)
                {
                    if (temp.ReferenciaHeap == id)
                    {
                        return temp;
                    }
                    temp = temp.Siguiente;
                }
            }
            return null;
        }

        public void vaciarHeap()
        {
            top = null;
        }
    }
}
