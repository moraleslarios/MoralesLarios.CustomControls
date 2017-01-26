using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoralesLarios.CustomsControls.HelpControls.EventArgs
{
    public class RoutedElementPopulateEventArgs : RoutedEventArgs
    {
        public string    Text       { get; set; }
        public bool      Cancel     { get; set; }
        public UIElement ControlAdd { get; set; }



        public RoutedElementPopulateEventArgs(string text, UIElement controlAdd, bool cancel)
        {
            Text       = text;
            Cancel     = cancel;
            ControlAdd = controlAdd;
        }
    }
}
