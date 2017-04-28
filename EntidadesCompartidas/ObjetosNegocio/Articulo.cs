using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public class Articulo
    {
        private long _codigoBarras;
        private string _nombre;
        private double _precio;
        private int _stock;
        private string _descripcion;
        private string _imagen;
        private bool _eliminado;

        private CategoriaArticulo _categoria;

        public long CodigoBarras
        {
            get
            { 
                return _codigoBarras;
            }

            set
            { 
                _codigoBarras = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _nombre;
            }

            set
            {
                _nombre = value;
            }
        }

        public double Precio
        {
            get
            {
                return _precio;
            }

            set
            {
                _precio = value;
            }
        }


        public int Stock
        {
            get
            {
                return _stock;
            }

            set
            {
                _stock = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }

            set
            {
                _descripcion = value;
            }
        }


        public string Imagen 
        {
            get
            {
                return _imagen;
            }

            set
            {
                _imagen = value;
            }
        }


        public CategoriaArticulo Categoria
        {
            get
            {
                return _categoria;
            }

            set
            {
                _categoria = value;
            }
        }

        public bool Eliminado
        {
            get
            {
                return _eliminado;
            }

            set
            {
                _eliminado = value;
            }
        }

        public Articulo()
            : this(0, "S/N", 0, 0, "S/D", "No disponible", new CategoriaArticulo(), false)
        { 
        }

        public Articulo(long codigoBarras, string nombre, double precio, int stock, string descripcion, string imagen, CategoriaArticulo categoria, bool eliminado)
        {
            CodigoBarras = codigoBarras;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Descripcion = descripcion;
            Imagen = imagen;
            Categoria = categoria;
            Eliminado = eliminado;
        }

        public override string ToString()
        {
            return "Codigo de Barras: " + CodigoBarras + ", Nombre: " + Nombre + ", Precio: " + Precio + ", Stock: " + Stock + ", Descripción: " + Descripcion + ", Imagen: " + Imagen + ", Categoria: " + Categoria.Nombre + ", Eliminado: " + (Eliminado ? "SI" : "NO") ;
        }

    }
}
