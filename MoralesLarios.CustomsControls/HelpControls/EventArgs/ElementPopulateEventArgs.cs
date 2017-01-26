namespace MoralesLarios.CustomsControls.HelpControls.EventArgs
{
    using System;
    using System.Windows;

    public class ElementPopulateEventArgs : EventArgs
    {

        public string Text { get; set; }
        public bool Cancel { get; set; }
        public UIElement ControlAdd { get; set; }



        public ElementPopulateEventArgs(string text, UIElement controlAdd, bool cancel)
        {
            Text       = text;
            Cancel     = cancel;
            ControlAdd = controlAdd;
        }
    }
}