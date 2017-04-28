using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public class CategoriaArticulo
    {
        private string _nombre;
        private string _descripcion;
        private bool _eliminado;

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

        public CategoriaArticulo()
            :this("N/D", "S/D", false)
        { 
        }

        public CategoriaArticulo(string nombre, string descripcion, bool eliminado)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Eliminado = eliminado;
        }

        public override string ToString()
        {
            return "Nombre: " + Nombre + ", Descripción: " + Descripcion + ", Eliminado: " + (Eliminado ? "SI" : "NO");
        }

    }
}
