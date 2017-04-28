using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public abstract class Usuario
    {
        private int _cedula;
        private string _nombreCompleto;
        private string _nombreUsuario;
        private string _contrasenia;
        private string _imagen;

        public int Cedula
        {
            get
            {
                return _cedula;
            }
            set
            {
                _cedula = value;
            }
        }

        public string NombreCompleto
        {
            get
            {
                return _nombreCompleto;
            }
            set
            {
                _nombreCompleto = value;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return _nombreUsuario;
            }

            set
            {
                _nombreUsuario = value;
            }
        }

        public string Contrasenia
        {
            get
            {
                return _contrasenia;
            }

            set
            {
                _contrasenia = value;
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

        public Usuario()
        :this(1, "N/D", "N/D", "12345", "~/Imagenes/avatarAdminH.png")
        {
        }

        public Usuario(int cedula, string nombreCompleto, string nombreUsuario, string contrasenia, string imagen)
        {
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
            Imagen = imagen;
        }

        public override string ToString()
        {
            return "Cédula: " + Cedula + ", Nombre Completo: " + NombreCompleto + ", Nombre de Usuario: " + NombreUsuario + ", Contraseña: " + Contrasenia + ", Imagen de avatar: " + Imagen;
        }
    }
}
