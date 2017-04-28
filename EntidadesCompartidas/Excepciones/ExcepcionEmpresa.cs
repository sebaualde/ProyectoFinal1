using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.Excepciones
{
    public abstract class ExcepcionEmpresa : ApplicationException
    {
        public ExcepcionEmpresa()
        {        
        }

        public ExcepcionEmpresa(string mensaje)
            : base(mensaje)
        { 
        }
    }
}
