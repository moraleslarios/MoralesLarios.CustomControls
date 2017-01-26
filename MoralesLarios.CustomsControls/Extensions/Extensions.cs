using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using MoralesLarios.CustomsControls.Exceptions;

namespace MoralesLarios.CustomsControls.Extensions
{





    public static class Extensions
    {

        #region LinQ
        public static IEnumerable<T> WhereAll<T>(this IEnumerable<T> source, object objSearch)
        {
            /// validar null objSearch

            var properties = GetPropertiesObj(source);

            foreach (var item in source)
            {
                foreach (var property in properties)
                {
                    object value = property.GetValue(item);

                    if (value != null && value?.ToString()?.Trim() == objSearch.ToString().Trim())
                    {
                        yield return item;
                    }
                }
            }


        }


        public static IEnumerable<T> WhereAllForFuncInFields<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, IEnumerable<string> FieldsSearch)
        {
            var exceptionFields = GetExceptionsFieldsByAceptFields<T>(source, FieldsSearch).ToArray();

            var result = source.WhereAllForFunc(objSearch, comp, int.MaxValue, exceptionFields);

            return result;
        }


        public static IEnumerable<T> WhereAllForFunc<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, params string[] exceptionFields)
        {
            var result = source.WhereAllForFunc(objSearch, comp, int.MaxValue, exceptionFields);

            return result;
        }






        public static IEnumerable<T> WhereAllForFunc<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, string strFieldName)
        {
            var result = source.WhereAllForFunc(objSearch, comp, int.MaxValue, strFieldName);

            return result;
        }


        public static IEnumerable<T> WhereAllForFunc<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, int count, string strFieldName)
        {
            var exceptionFields = GetExceptionsFieldsByAceptFields<T>(source,  strFieldName).ToArray();

            var result = source.WhereAllForFunc(objSearch, comp, count, exceptionFields);

            return result;
        }

        public static IEnumerable<string> WhereAllForFuncDistinct<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, int count, string strFieldName)
        {
            var exceptionFields = GetExceptionsFieldsByAceptFields<T>(source, strFieldName).ToArray();

            var result = source.WhereAllForFunc(objSearch, comp, count, exceptionFields).Select(strFieldName).Distinct();

            return result;
        }


        public static IEnumerable<string> WhereAllForFuncInOrder<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, int count, IEnumerable<string> orderStrFieldNames)
        {
            List<string> result = new List<string>();

            var orderStrFileNamesDef = orderStrFieldNames?.Any() ?? false ?
                                        orderStrFieldNames :
                                        GetPropertiesObj(source).Select(a => a.Name);

            foreach (var strName in orderStrFileNamesDef)
            {
                int contElements = count - result.Count();

                if (contElements == 0) return result;

                var partialResult = source.WhereAllForFuncDistinct(objSearch, comp, contElements, strName);

                result.AddRange(partialResult);
            }

            return result;
        }


        public static IEnumerable<T> WhereAllForFunc<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, int cont, params string[] exceptionFields)
        {
            var properties = GetPropertiesWithExceptionsFields(source);

            List<T> itemsReturn = new List<T>();

            int i = 0;

            foreach (var item in source)
            {
                if (IsGoodCondition(properties, item, objSearch, comp, exceptionFields))
                {
                    i++;

                    if (i > cont) break;

                    yield return item;
                }
            }


        }



        public static IEnumerable<string> Select<T>(this IEnumerable<T> source, string strFieldName)
        {
            var sourceList = source.ToList();

            if (!source.Any()) return Enumerable.Empty<string>();

            var properties = GetPropertiesObj(source);

            string strFieldDef = strFieldName ?? properties.First().Name;

            var property = properties.SingleOrDefault(a => a.Name == strFieldDef);

            if (property == null) throw new ArgumentException($"No se ha encontrado la propiedad {strFieldName}, dentro del tipo {typeof(T).Name}", nameof(strFieldName));

            var result = source.Select(a => property.GetValue(a)?.ToString());

            return result;
        }


        public static IEnumerable<string> GetExceptionsFieldsByAceptFields<T>(IEnumerable<T> source, string strFieldsName)
        {
            IEnumerable<string> result = Enumerable.Empty<string>();

            if (! string.IsNullOrWhiteSpace(strFieldsName))
            {
                result = GetPropertiesObj(source).Select(a => a.Name).Where(a => a != strFieldsName);
            }

            return result;
        }

        public static IEnumerable<string> GetExceptionsFieldsByAceptFields<T>(IEnumerable<T> source, IEnumerable<string> strFieldsNames)
        {
            IEnumerable<string> result = Enumerable.Empty<string>();

            if (strFieldsNames?.Any() ?? false)
            {
                result = GetPropertiesObj(source).Select(a => a.Name).Where(a => ! strFieldsNames.Contains(a));
            }

            return result;
        }


        public static bool IsGoodCondition<T>(IEnumerable<PropertyInfo> properties, T item, object objSearch, Func<object, object, bool> comp, params string[] exceptionFields)
        {
            bool result = false;

            var columnsDef = properties.Select(a => a.Name).Distinct().Except(exceptionFields).ToList();
            var propertiesDef = properties.Where(a => columnsDef.Contains(a.Name)).ToList();

            foreach (var property in propertiesDef)
            {
                try
                {
                    if (property.GetIndexParameters().Any()) continue;

                    object value = property.GetValue(item, null);

                    if (property.PropertyType.IsValueType || property.PropertyType.Name.ToUpper() == "STRING")
                    {
                        result = value != null && comp(value, objSearch);
                    }

                    if (result) break;
                }
                catch (Exception ex)
                {
                    string message = $"The property {property.Name} it isn't supported. View InnerException for more info.{Environment.NewLine}Set up FieldsSugerenciesSearch and FieldsSearch without this property.";

                    throw new PropertyNotSupportedException(message, property.Name, ex);
                }


            }

            return result;

        }


        public static IEnumerable<PropertyInfo> GetPropertiesWithExceptionsFields(Type type, params string[] exceptionFields)
        {
            IEnumerable<PropertyInfo> result = type.GetProperties().Where(a => !a.PropertyType.Name.StartsWith("IColl")).ToList();

            if (!exceptionFields?.Any() ?? false) return result;

            result = result.Where(a => !exceptionFields?.Contains(a.Name) ?? false).ToArray();

            return result;
        }


        public static IEnumerable<PropertyInfo> GetPropertiesWithExceptionsFields<T>(IEnumerable<T> source, params string[] exceptionFields)
        {
            IEnumerable<PropertyInfo> result = GetPropertiesObj(source).Where(a => !a.PropertyType.Name.StartsWith("IColl")).ToList();

            if (!exceptionFields?.Any() ?? false) return result;

            result = result.Where(a => !exceptionFields?.Contains(a.Name) ?? false).ToArray();

            return result;
        }


        public static PropertyInfo[] GetPropertiesObj<T>(IEnumerable<T> source)
        {
            PropertyInfo[] result = null;

            if (typeof(T).Name == "Object" && (source?.Any() ?? false))
            {
                result = source.First().GetType().GetProperties();
            }
            else
            {
                result = typeof(T).GetProperties();
            }


            return result;
        }





        public static bool IsTypeEnumerable(Type type)
        {
            return type.GetInterfaces()
                    .Any(t => t.IsGenericType
                    && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }



        #endregion







        #region WPF


        public static void FuncFilter<T>(this IEnumerable<T> source, Func<T, bool> filter) where T : class
        {
            var view = CollectionViewSource.GetDefaultView(source);

            view.Filter += a =>
            {
                return filter((T)a);
            };

        }

        public static void FuncFilterAll<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, params string[] excepciones) where T : class
        {
            var filterOK = source.WhereAllForFunc(objSearch, comp, excepciones).ToList();

            var view = CollectionViewSource.GetDefaultView(source);

            view.Filter += a =>
            {
                return filterOK.Any(b => b == a);
            };

        }


        public static void FuncFilterAllInFields<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, IEnumerable<string> fieldsSearch) where T : class
        {
            var filterOK = source.WhereAllForFuncInFields(objSearch, comp, fieldsSearch).ToList();

            var view = CollectionViewSource.GetDefaultView(source);

            view.Filter += a =>
            {
                return filterOK.Any(b => b == a);
            };

        }


        public static Task FuncFilterAllInFieldsAsync<T>(this IEnumerable<T> source, ICollectionView view, object objSearch, Func<object, object, bool> comp, IEnumerable<string> fieldsSearch) where T : class
        {
            return Task.Run(() =>
           {
               var filterOK = source.WhereAllForFuncInFields(objSearch, comp, fieldsSearch).ToList();

                //view.Filter += a => filterOK.Any(b => b == a);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    view.Filter += a => filterOK.Any(b => b == a);
                });


           });

        }



        #endregion


    }
}
