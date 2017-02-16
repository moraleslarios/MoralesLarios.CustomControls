using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MoralesLarios.Utilities.Helper.BrushesHelper;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal class ExcelActionsCommandGenerator
    {
        private readonly IClipBoardCopier  allCopier;
        private readonly IItemsSourceItems itemsSourceInserts;
        private readonly IClipBoardCopier  selectedCopier;
        private readonly Colorator         colorator;



        public ExcelActionsCommandGenerator(IClipBoardCopier allCopier, IClipBoardCopier selectedCopier, IItemsSourceItems itemsSourceInserts, Colorator colorator)
        {
            this.allCopier          = allCopier;
            this.selectedCopier     = selectedCopier;
            this.itemsSourceInserts = itemsSourceInserts;
            this.colorator          = colorator;
        }


        public ExcelActionsCommandInfo GenerateCommands(DependencyObject d, bool getContainsHeader, bool getShowErrorMessages, bool getCancelWithErrors)
        {
            ICommand commandCopyAll = new RelayCommands(ActionWithAnimation( a => allCopier    .Copy(d, getContainsHeader), d), a => allCopier.CanCopy(d));
            ICommand commandCopy    = new RelayCommands(ActionWithAnimation(a => selectedCopier.Copy(d, getContainsHeader), d), a => selectedCopier.CanCopy(d));
            ICommand pasteCommand   = new RelayCommands(ActionWithAnimation(a => itemsSourceInserts.Insert(d,
                                                                                     Clipboard.GetText(),
                                                                                     getShowErrorMessages,
                                                                                     getCancelWithErrors),d),
                                                      a => Clipboard.ContainsText());

            var result = new ExcelActionsCommandInfo { CopyAllCommmand = commandCopyAll, CopyCommand = commandCopy, PasteCommand = pasteCommand };

            return result;
        }


        private Action<Object> ActionWithAnimation(Action<object> action, DependencyObject d)
        {
            return a =>
            {
                action(null);

                var control = (Control)d;

                colorator.FlashColor(control, Colors.Black);
            };

        }




    }
}
