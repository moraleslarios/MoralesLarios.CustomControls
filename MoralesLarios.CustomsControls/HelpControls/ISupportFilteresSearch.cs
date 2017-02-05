using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoralesLarios.CustomsControls.HelpControls
{
    public interface ISupportFilteresSearch
    {
        void PopulateSubResults(object objFilter, Func<object, object, bool> filter, int count, IEnumerable<string> strFieldsSearchNames, bool IsDebugMode = false);
        void FilterItemsSource(object objFilter, Func<object, object, bool> filter, IEnumerable<string> fieldsSearchNames);
        Task FilterItemsSourceAsync(object objFilter, Func<object, object, bool> filter, IEnumerable<string> fieldsSearchNames);
        IEnumerable<object> GetFilteredAllItemSource(IEnumerable<object> itemsSource);
        IEnumerable<object> GetFilteredItemSource(IEnumerable<object> itemsSource);

        event EventHandler ErrorAction;
    }
}