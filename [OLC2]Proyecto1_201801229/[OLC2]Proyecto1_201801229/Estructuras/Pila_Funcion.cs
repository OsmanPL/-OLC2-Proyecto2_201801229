using System;
using System.Collections.Generic;
using System.Text;
using _OLC2_Proyecto1_201801229.Interfaces;

namespace _OLC2_Proyecto1_201801229.Estructuras
{
    class Pila_Funcion
    {
        Elemento_Funcion top;

        public bool pilaVacia()
        {
            return top != null ? false : true;
        }

        public void push(Elemento_Funcion nuevo)
        {
            if (pilaVacia())
            {
                top = nuevo;
            }
            else
            {
                nuevo.Siguiente = top;
                top = nuevo;
            }
        }

        public Elemento_Funcion buscar(String id)
        {
            Elemento_Funcion temp = top;
            while (temp!=null)
            {
                if (temp.Funcion.GetType() == typeof(Funcion))
                {
                    Funcion func = (Funcion)temp.Funcion;
                    if (func.Id.ToLower().Equals(id.ToLower()))
                    {
                        return temp;
                    }
                }
                else if (temp.Funcion.GetType() == typeof(Procedimiento))
                {
                    Procedimiento func = (Procedimiento)temp.Funcion;
                    if (func.Id.ToLower().Equals(id.ToLower()))
                    {
                        return temp;
                    }
                }

                temp = temp.Siguiente;
            }
            return null;
        }
        
        public Elemento_Funcion pull()
        {
            if (!pilaVacia())
            {
                Elemento_Funcion temp = top;
                top = top.Siguiente;
                return temp;
            }
            return null;
        } 

        public void vaciarPila()
        {
            top = null;
        }
    }
}
