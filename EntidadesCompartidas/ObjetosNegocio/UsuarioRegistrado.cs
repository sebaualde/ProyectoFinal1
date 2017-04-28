using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public class UsuarioRegistrado : Usuario
    {
        private string _direccionEnvio;
        private long _numeroTarjeta;
        private int _telefono;
        private bool _eliminado;

        public string DireccionEnvio
        {
            get
            {
                return _direccionEnvio;
            }
            set
            {
                _direccionEnvio = value;
            }
        }

        public long NumeroTarjeta
        {
            get
            {
                return _numeroTarjeta;
            }
            set
            {
                _numeroTarjeta = value;
            }
        }

        public int Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
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

        public UsuarioRegistrado()
            :this (0, "N/D", "N/D", "N/D", "N/D", "N/D", 0, 0, false)
        {
  
        }

        public UsuarioRegistrado(int cedula, string nombreCompleto, string nombreUsuario, string contrasenia, string imagen, string direccionEnvio, long numeroTarjeta, int telefono, bool eliminado)
            : base(cedula, nombreCompleto, nombreUsuario, contrasenia, imagen)
        {
            DireccionEnvio = direccionEnvio;
            NumeroTarjeta = numeroTarjeta;
            Telefono = telefono;
            Eliminado = eliminado;
        }

        public override string ToString()
        {
            return base.ToString() + ", Dirección de envío: " + DireccionEnvio + ", Nº Tarjeta de crédito: " + NumeroTarjeta + ", Teléfono: " + Telefono + "Eliminado: " + (Eliminado ? "Si" : "No");
        }

    }
}
