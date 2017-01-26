using MoralesLarios.CustomsControls.HelpControls.EventArgs;
using System;
using System.Collections.Generic;

namespace MoralesLarios.CustomsControls.HelpControls
{
    public interface IPopulateStackPanelControls
    {
        void PopulateWithLinks(IEnumerable<string> source);
        void RemoveControls();

        event EventHandler<ElementPopulateEventArgs> InsertingSugerentControl;
    }
}