using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.Excepciones
{
    public class ExcepcionPresentacion : ExcepcionEmpresa
    {
        public ExcepcionPresentacion()
        { 
        }

        public ExcepcionPresentacion(string mensaje)
            : base(mensaje)
        { 
        }
    }
}
