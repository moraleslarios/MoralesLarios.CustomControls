using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal class Converters
    {
        public static object ConverterTo(string valor, PropertyInfo propiedad)
        {

            string fullNameTipe = propiedad.PropertyType.FullName.Split('`')[0];

            object resultado = null;

            if (valor != null)
            {
                try
                {
                    switch (fullNameTipe)
                    {
                        case "System.DateTime": resultado = DateTime.Parse(valor); break;
                        case "System.Int32"   : resultado = int.Parse(valor); break;
                        case "System.Int16"   : resultado = short.Parse(valor); break;
                        case "System.String"  : resultado = valor.ToString(); break;
                        case "System.Char"    : resultado = char.Parse(valor); break;
                        case "System.Double"  : resultado = double.Parse(valor); break;
                        case "System.Decimal" : resultado = decimal.Parse(valor); break;
                        case "System.Int64"   : resultado = long.Parse(valor); break;
                        case "System.Boolean": resultado = bool.Parse(valor); break;
                        case "System.Nullable":
                            string tipoDelNulable = propiedad.PropertyType.FullName.Split('`')[1];
                            if (tipoDelNulable.Contains("System.DateTime")) resultado = string.IsNullOrEmpty(valor) ? null : (DateTime?)DateTime.Parse(valor);
                            if (tipoDelNulable.Contains("System.Int32")) resultado = string.IsNullOrEmpty(valor) ? null : (int?)int.Parse(valor);
                            if (tipoDelNulable.Contains("System.Int16")) resultado = string.IsNullOrEmpty(valor) ? null : (short?)short.Parse(valor);
                            if (tipoDelNulable.Contains("System.String")) resultado = valor;
                            if (tipoDelNulable.Contains("System.Char")) resultado = string.IsNullOrEmpty(valor) ? null : (char?)char.Parse(valor);
                            if (tipoDelNulable.Contains("System.Double")) resultado = string.IsNullOrEmpty(valor) ? null : (double?)double.Parse(valor);
                            if (tipoDelNulable.Contains("System.Decimal")) resultado = string.IsNullOrEmpty(valor) ? null : (decimal?)decimal.Parse(valor);
                            if (tipoDelNulable.Contains("System.Int64")) resultado = string.IsNullOrEmpty(valor) ? null : (long?)long.Parse(valor);
                            if (tipoDelNulable.Contains("System.Boolean")) resultado = string.IsNullOrEmpty(valor) ? null : (bool?)bool.Parse(valor);
                            break;

                        default: throw new Exception(string.Format("The type {0} is not sopported for this assembly.", fullNameTipe));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("General error fro the type {0}. For more info see InnerException", fullNameTipe), ex);
                }

            }


            return resultado;
        }
    }
}
