using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal static class Extensions
    {
    
        public static void ToClipBoardCSV<T>(this IEnumerable<T> source, string separador = "\t", bool pintarColumnasEnterasNull = true, bool pintarCabecera = false)
        {
            var vista = CollectionViewSource.GetDefaultView(source);

            var datos = vista.OfType<T>().ToList();

            IEnumerable<PropertyInfo> colsReales = null;
            StringBuilder sb = new StringBuilder();

            if (datos.Count() > 0)
            {
                colsReales = pintarColumnasEnterasNull ? typeof(T).GetProperties() : DevolverColunasConDatos<T>(datos);

                if (pintarCabecera)
                    sb.AppendLine(DevolverLineaCSV(colsReales, separador));

                foreach (var s in datos)
                    sb.AppendLine(DevolverLineaCSV(colsReales, separador, s));
            }

            Clipboard.SetText(sb.ToString());

        }


        public static void ToClipBoardCSV(this IEnumerable source, string separador = "\t", bool pintarColumnasEnterasNull = true, bool pintarCabecera = false)
        {
            var datos = CollectionViewSource.GetDefaultView(source);

            IEnumerable<PropertyInfo> colsReales = null;
            StringBuilder sb = new StringBuilder();

            var tieneDatos = source.ToListObj().Any();

            if (tieneDatos)
            {
                colsReales = pintarColumnasEnterasNull ? source.ToListObj().First().GetType().GetProperties() : DevolverColunasConDatos(datos);

                if (pintarCabecera)
                    sb.AppendLine(DevolverLineaCSV(colsReales, separador));

                foreach (var s in datos)
                {
                    try
                    {
                        sb.AppendLine(DevolverLineaCSV(colsReales, separador, s));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            Clipboard.SetText(sb.ToString());

        }





        public static void ToClipBoardCSV(this IEnumerable<object> source, string separador = "\t", bool pintarColumnasEnterasNull = true, bool pintarCabecera = false)
        {
            var vista = CollectionViewSource.GetDefaultView(source);

            //var datos = vista.OfType<T>().ToList();

            IEnumerable<PropertyInfo> colsReales = null;
            StringBuilder sb = new StringBuilder();

            if (source.Count() > 0)
            {
                colsReales = pintarColumnasEnterasNull ? source.First().GetType().GetProperties() : null;

                if (pintarCabecera)
                    sb.AppendLine(DevolverLineaCSV(colsReales, separador));

                foreach (var s in source)
                {
                    try
                    {
                        sb.AppendLine(DevolverLineaCSV(colsReales, separador, s));
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                    
            }

            Clipboard.SetText(sb.ToString());

        }


        private static string DevolverLineaCSV(IEnumerable<PropertyInfo> columnas, string separador, object objetoValor = null)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                columnas.ToList().ForEach(c => sb.Append(string.Format("{0}{1}",
                                                                            (objetoValor == null ? c.Name : c.GetValue(objetoValor, null) ?? string.Empty)?.ToString().Replace("\n", " - "),
                                                                            separador)));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            sb.Remove(sb.ToString().Length - 1, 1); // borro el último caracter separador

            return sb.ToString();
        }


        private static IEnumerable<PropertyInfo> DevolverColunasConDatos<T>(IEnumerable<T> source)
        {
            PropertyInfo[] propiedades = typeof(T).GetProperties();

            foreach (PropertyInfo propiedad in propiedades)
            {
                int resultado = 0;
                try
                {
                    //var datos = source.Select(p => propiedad.GetValue(p, null) != null).ToList();
                    resultado = source.Where(p => propiedad.GetValue(p, null) != null).Count();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Se ha detectado un problema al comprobar las columnas con datos nulos, error: {0}", ex.Message));
                }
                if (resultado > 0) yield return propiedad;
            }
        }


        private static IEnumerable<PropertyInfo> DevolverColunasConDatos(IEnumerable source)
        {
            PropertyInfo[] propiedades = source.GetEnumerator().Current.GetType().GetProperties();

            var sourceObj = source.ToListObj();

            foreach (PropertyInfo propiedad in propiedades)
            {
                int resultado = 0;
                try
                {
                    resultado = sourceObj.Where(p => propiedad.GetValue(p, null) != null).Count();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Se ha detectado un problema al comprobar las columnas con datos nulos, error: {0}", ex.Message));
                }
                if (resultado > 0) yield return propiedad;
            }
        }




        private static IEnumerable<object> ToListObj(this IEnumerable source)
        {
            foreach(var s in source)
            {
                yield return (object)s;
            }
        }






    }
}
