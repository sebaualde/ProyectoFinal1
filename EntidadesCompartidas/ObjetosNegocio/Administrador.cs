using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.ObjetosNegocio
{
    public class Administrador : Usuario
    {
        private string _cargo;

        public string Cargo
        {
            get
            {
                return _cargo;
            }
            set
            {
                _cargo = value;
            }
        }

        public Administrador()
        {
            Cargo = "Webmaster";
        }

        public Administrador(int cedula, string nombreCompleto, string nombreUsuario, string contrasenia, string imagen, string cargo)
            : base(cedula, nombreCompleto, nombreUsuario, contrasenia, imagen)
        {
            Cargo = cargo;
        }

        public override string ToString()
        {
            return base.ToString() + ", Cargo: " + Cargo;
        }
    }
}
