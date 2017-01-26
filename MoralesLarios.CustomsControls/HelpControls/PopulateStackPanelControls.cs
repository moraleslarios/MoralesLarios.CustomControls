using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MoralesLarios.CustomsControls.HelpControls.EventArgs;

namespace MoralesLarios.CustomsControls.HelpControls
{
    public class PopulateStackPanelControls : IPopulateStackPanelControls
    {

        private readonly StackPanel stackPanel;
        private readonly Action<string> actionClickSearch;

        public event EventHandler<ElementPopulateEventArgs> InsertingSugerentControl;
        



        public PopulateStackPanelControls(StackPanel stackPanel, Action<string> actionClickSearch)
        {
            this.stackPanel        = stackPanel;
            this.actionClickSearch = actionClickSearch;
        }


        public void RemoveControls()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                stackPanel.Children.Clear();
            });
        }


        public void PopulateWithLinks(IEnumerable<string> source)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in source)
                {
                    var txt = new TextBlock
                    {
                        Text = item,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                    };

                    var evargs = new ElementPopulateEventArgs(item, txt, false);
                    OnInsertingSugerentControl(evargs);

                    if ( ! evargs.Cancel)
                    {
                        evargs.ControlAdd.MouseDown += (sender, e) => actionClickSearch(txt.Text);

                        stackPanel.Children.Add(evargs.ControlAdd);
                    }

                }
            });


        }








        protected virtual void OnInsertingSugerentControl(ElementPopulateEventArgs evargs) => InsertingSugerentControl?.Invoke(this, evargs);


    }
}
