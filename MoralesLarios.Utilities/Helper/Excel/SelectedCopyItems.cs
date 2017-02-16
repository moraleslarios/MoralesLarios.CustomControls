using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal class SelectedCopyItems : IClipBoardCopier
    {
        public bool CanCopy(DependencyObject d)
        {
            return true;
        }
        
        public void Copy(DependencyObject d, bool paintHeader = true)
        {
            dynamic control = d;

            var data = control.SelectedItems;

            IEnumerable<object> datosEnumerable = ToIEnumerable(data);

            datosEnumerable.ToClipBoardCSV(pintarCabecera: paintHeader);
        }

        

        private IEnumerable<object> ToIEnumerable(dynamic source)
        {
            foreach(var s in source)
            {
                yield return (object)s;
            }
        }


    }
}
