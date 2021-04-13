using System;
using System.Collections.Generic;
using System.Text;

namespace _OLC2_Proyecto1_201801229.Interfaces
{
    class TablaSimbolos : LinkedList<Simbolo>
    {
        String entorno;

        public string Entorno { get => entorno; set => entorno = value; }

        public TablaSimbolos(String id) : base()
        {
            this.Entorno = id;
        }

        public Object getValor(String buscarSimbolo)
        {
            foreach (Simbolo simbolo in this)
            {
                if (simbolo.Id.ToLower().Equals(buscarSimbolo.ToLower()))
                {
                    return simbolo.Valor;
                }
            }
            Console.WriteLine("La variable " + buscarSimbolo + " no se encuentra en este ambito.");
            return "Desconocido";
        }

        public Boolean existe(String buscarSimbolo)
        {
            foreach (Simbolo simbolo in this)
            {
                if (simbolo.Id.ToLower().Equals(buscarSimbolo.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public Simbolo getSimbolo(String buscarSimbolo)
        {
            foreach (Simbolo simbolo in this)
            {
                if (simbolo.Id.ToLower().Equals(buscarSimbolo.ToLower()))
                {
                    return simbolo;
                }
            }
            return null;
        }

        public void setValor(String buscarSimbolo, Object valor)
        {
            foreach (Simbolo simbolo in this)
            {
                if (simbolo.Id.ToLower().Equals(buscarSimbolo.ToLower()))
                {
                    simbolo.Valor=valor;
                    return;
                }
            }
            Console.WriteLine("La variable " + buscarSimbolo + " no se encuentra en este ambito.");
        }
    }
}
