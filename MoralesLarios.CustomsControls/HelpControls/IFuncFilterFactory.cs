using System;

namespace MoralesLarios.CustomsControls.HelpControls
{
    public interface IFuncFilterFactory
    {
        Func<object, object, bool> GetFuncFilter(FilterType filterType, bool isKeySensitive = false);

        Func<object, object, bool> Custom { get; set; }
    }
}