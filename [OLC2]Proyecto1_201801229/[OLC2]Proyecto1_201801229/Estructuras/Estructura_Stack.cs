using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Estructura_Stack
    {
        Elemento_Stack top;

        public bool existe(String id)
        {
            Elemento_Stack temp = top;
            if (temp != null)
            {
                while (temp != null)
                {
                    if (temp.Identificador.ToLower().Equals(id.ToLower()))
                    {
                        return true;
                    }
                    temp = temp.Siguiente;
                }
            }
            return false;
        }

        public bool stackVacia()
        {
            return top != null ? false : true;
        }

        public void agregarStack(Elemento_Stack nuevo)
        {
            if (stackVacia())
            {
                top = nuevo;
            }
            else
            {
                nuevo.Siguiente = top;
                top = nuevo;
            }
        }

        public Elemento_Stack buscarElementoStack(String id)
        {
            Elemento_Stack temp = top;
            if (temp != null)
            {
                while (temp != null)
                {
                    if (temp.Identificador.ToLower().Equals(id.ToLower()))
                    {
                        return temp;
                    }
                    temp = temp.Siguiente;
                }
            }
            return null;
        }

        public void vaciarStack()
        {
            top = null;
        }
    }
}
