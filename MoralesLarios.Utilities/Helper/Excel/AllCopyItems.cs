using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal class AllCopyItems : IClipBoardCopier
    {
        public bool CanCopy(DependencyObject d)
        {
            return true;
        }

        public void Copy(DependencyObject d, bool paintHeader = true)
        {
            var itemsControl = d as ItemsControl;

            itemsControl.ItemsSource.ToClipBoardCSV(pintarCabecera: paintHeader);
        }
    }
}
