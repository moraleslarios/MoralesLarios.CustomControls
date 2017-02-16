using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal interface IClipBoardCopier
    {
        void Copy(DependencyObject d, bool paintHeader = true);
        bool CanCopy(DependencyObject d);
    }
}
