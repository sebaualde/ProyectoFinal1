using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.Excepciones
{
    public class ExcepcionPersistencia : ExcepcionEmpresa
    {
        public ExcepcionPersistencia()
        { 
        }

        public ExcepcionPersistencia(string mensaje)
            : base(mensaje)
        { 
        }
    }
}
