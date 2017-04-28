using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesCompartidas.Excepciones
{
    public class ExcepcionLogica : ExcepcionEmpresa
    {
        public ExcepcionLogica()
        { 
        }

        public ExcepcionLogica(string mensaje)
            : base(mensaje)
        { 
        }
    }
}
