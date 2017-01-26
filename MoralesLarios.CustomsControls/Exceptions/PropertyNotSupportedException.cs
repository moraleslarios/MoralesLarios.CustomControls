using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.Exceptions
{
    public class PropertyNotSupportedException : Exception
    {
        public string PropertyNotSupported { get; private set; }

        public PropertyNotSupportedException(string message, string propertyNotSupported, Exception innerException) : base(message, innerException)
        {
            PropertyNotSupported = propertyNotSupported;
        }
    }
}
