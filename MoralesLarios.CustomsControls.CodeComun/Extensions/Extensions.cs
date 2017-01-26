using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MoralesLarios.CustomsControls.Extensions
{





    public static class Extensions
    {

        #region LinQ
        public static IEnumerable<T> WhereAll<T>(this IEnumerable<T> source, object objSearch)
        {
            /// validar null objSearch

            var properties = typeof(T).GetProperties();

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

        public static IEnumerable<T> WhereAllForFunc<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, params string[] exceptionFields)
        {
            var result = source.WhereAllForFunc(objSearch, comp, int.MaxValue, exceptionFields);

            return result;
        }


        public static IEnumerable<T> WhereAllForFunc<T>(this IEnumerable<T> source, object objSearch, Func<object, object, bool> comp, int cont, params string[] exceptionFields)
        {
            /// validar null objSearch

            var properties = GetPropertiesWithExceptionsFields(typeof(T));

            int i = 0;

            foreach (var item in source)
            {
                if (IsGoodCondition(properties, item, objSearch, comp, exceptionFields))
                {
                    i++;

                    if (i > cont) break; ;

                    yield return item;
                }
            }


        }



        public static bool IsGoodCondition<T>(IEnumerable<PropertyInfo> properties, T item, object objSearch, Func<object, object, bool> comp, params string[] exceptionFields)
        {
            bool result = false;

            foreach (var property in properties)
            {
                if (property.GetIndexParameters().Any()) continue;

                object value = property.GetValue(item, null);

                if (property.PropertyType.IsValueType || property.PropertyType.Name.ToUpper() == "STRING")
                {
                    result = value != null && comp(value, objSearch);
                }

                if (value != null && property.PropertyType.IsClass && property.PropertyType.Name.ToUpper() != "STRING")
                {
                    var newProperties = GetPropertiesWithExceptionsFields(value.GetType(), exceptionFields);

                    result = IsGoodCondition(newProperties, value, objSearch, comp, exceptionFields);
                }

                if (result) break;
            }

            return result;

        }


        public static IEnumerable<PropertyInfo> GetPropertiesWithExceptionsFields(Type type, params string[] exceptionFields)
        {
            IEnumerable<PropertyInfo> result = type.GetProperties().Where(a => !a.PropertyType.Name.StartsWith("IColl") && (!exceptionFields?.Contains(a.Name) ?? false)).ToArray();

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


        #endregion


    }
}
