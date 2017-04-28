using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public class LineaPedido
    {
        private int _numero;
        private int _cantidad;

        private Articulo _articulo;

        public int Numero
        {
            get
            {
                return _numero;
            }
            set
            {
                _numero = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {
                _cantidad = value;
            }
        }

        public Articulo PArticulo
        {
            get
            {
                return _articulo;
            }
            set
            {
                _articulo = value;
            }
        }

        public LineaPedido()
            :this (0, 0, new Articulo())
        { 
        
        }

        public LineaPedido(int numero, int cantidad, Articulo articulo)
        {
            Numero = numero;
            Cantidad = cantidad;
            PArticulo = articulo;
        }

        public override string ToString()
        {
            return "Numero: " + Numero + "Cantidad: " + Cantidad + "Articulo: " + PArticulo.Nombre;
        }
    }
}
