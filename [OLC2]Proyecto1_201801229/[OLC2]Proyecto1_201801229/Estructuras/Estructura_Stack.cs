using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Estructura_Stack
    {
        Elemento_Stack top;

        internal Elemento_Stack Top { get => top; set => top = value; }

        public bool existe(String id)
        {
            Elemento_Stack temp = Top;
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
            return Top != null ? false : true;
        }

        public void agregarStack(Elemento_Stack nuevo)
        {
            if (stackVacia())
            {
                Top = nuevo;
            }
            else
            {
                nuevo.Siguiente = Top;
                Top = nuevo;
            }
        }

        public void borrar(int refe)
        {
            Elemento_Stack temp = Top;
            if (temp != null)
            {
                while (temp != null)
                {
                    if (temp.ReferenciaStack == refe)
                    {
                        temp.Siguiente = null;
                        break;
                    }
                    temp = temp.Siguiente;
                }
            }
        }

        public Elemento_Stack buscarElementoStack(String id)
        {
            Elemento_Stack temp = Top;
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
            Top = null;
        }
    }
}
