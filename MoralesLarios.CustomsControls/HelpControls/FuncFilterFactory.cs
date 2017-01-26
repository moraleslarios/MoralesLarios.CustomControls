using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.HelpControls
{
    public class FuncFilterFactory : IFuncFilterFactory
    {

        private Func<object, object, bool> StarWithKeySensitive = (a, b) => a?.ToString()?.StartsWith(b?.ToString()) ?? false;
        private Func<object, object, bool> ContainsKeySensitive = (a, b) => a?.ToString()?.Contains  (b?.ToString()) ?? false;
        private Func<object, object, bool> EndWithKeySensitive  = (a, b) => a?.ToString()?.EndsWith  (b?.ToString()) ?? false;
        private Func<object, object, bool> EqualsKeySensitive   = (a, b) => a?.ToString()?.Equals(b?.ToString())     ?? false;

        private Func<object, object, bool> StarWith = (a, b) => a?.ToString()?.StartsWith(b?.ToString(), StringComparison.CurrentCultureIgnoreCase) ?? false;
        private Func<object, object, bool> Contains = (a, b) => a?.ToString()?.ToUpper()?.Contains(b?.ToString()?.ToUpper())                        ?? false;
        private Func<object, object, bool> EndWith  = (a, b) => a?.ToString()?.EndsWith  (b?.ToString(), StringComparison.CurrentCultureIgnoreCase) ?? false;
#pragma warning disable CS0108 // 'FuncFilterFactory.Equals' hides inherited member 'object.Equals(object)'. Use the new keyword if hiding was intended.
        private Func<object, object, bool> Equals   = (a, b) => a?.ToString()?.Equals    (b?.ToString(), StringComparison.CurrentCultureIgnoreCase) ?? false;
#pragma warning restore CS0108 // 'FuncFilterFactory.Equals' hides inherited member 'object.Equals(object)'. Use the new keyword if hiding was intended.

        public Func<object, object, bool> Custom { get; set; }



        public Func<object, object, bool> GetFuncFilter(FilterType filterType, bool isKeySensitive = false)
        {
            Func<object, object, bool> result = null;

            switch (filterType)
            {
                case FilterType.StarWith: result = isKeySensitive ? StarWithKeySensitive : StarWith;
                    break;
                case FilterType.EndWith:  result = isKeySensitive ? EndWithKeySensitive : EndWith;
                    break;
                case FilterType.Contains: result = isKeySensitive ? ContainsKeySensitive : Contains;
                    break;
                case FilterType.Equals:   result = isKeySensitive ? EqualsKeySensitive : Equals;
                    break;
                case FilterType.Custom:
                    if (Custom == null) throw new ArgumentNullException(nameof(filterType), $"You don't use filterType custom, without FilterAction null");
                    result = Custom;
                    break;
            }


            return result;
        }

    }


    public enum FilterType
    {
        StarWith,
        EndWith,
        Contains,
        Equals,
        Custom
    }

}
