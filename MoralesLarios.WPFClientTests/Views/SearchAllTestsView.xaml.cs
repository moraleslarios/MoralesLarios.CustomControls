using System.Windows;
using System.Windows.Media;
using System.Linq;
using System;
using System.Windows.Controls;

namespace MoralesLarios.WPFClientTests
{
    /// <summary>
    /// Description for SearchAllTestsView.
    /// </summary>
    public partial class SearchAllTestsView : Window
    {
        /// <summary>
        /// Initializes a new instance of the SearchAllTestsView class.
        /// </summary>
        public SearchAllTestsView()
        {
            InitializeComponent();

            searchAll.BorderBrush = Brushes.Red;

            //searchAll.FieldsSugerenciesSearch = new string[] { "Name", "City", "ID" };
            //searchAll.FieldsSearch = new string[] { "City", "ID" };

            //searchAll.CustomFuncAction = customMethods;

            searchAll.InsertingSugerentControl += SearchAll_InsertingSugerentControl;
        }

        private void SearchAll_InsertingSugerentControl(object sender, CustomsControls.HelpControls.EventArgs.ElementPopulateEventArgs e)
        {
            if (e.Text?.Contains("Ren") ?? false)
            {
                if (e.ControlAdd != null)
                {
                    var label = new Label();
                    label.Content = $"Cambiado ----> {e.Text}";
                    label.Foreground = Brushes.Red;

                    e.ControlAdd = label;

                }
            }
        }

        private bool customMethods(object arg1, object arg2)
        {
            bool result = false;

            if(arg1?.ToString()?.Length > 1 && arg2?.ToString()?.Length > 1)
            {
                result = arg1.ToString()[1] == arg2.ToString()[1];
            }

            return result;
        }
    }
}