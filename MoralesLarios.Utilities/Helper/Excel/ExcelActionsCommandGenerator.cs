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
using MoralesLarios.Utilities.Excel;

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


        public ExcelActionsCommandInfo GenerateCommands(DependencyObject d)
        {
            ICommand commandCopyAll = new RelayCommands(ActionWithAnimation(a => allCopier     .Copy(d, ExcelActions.GetContainsHeader(d)), d), a => allCopier.CanCopy(d)     );
            ICommand commandCopy    = new RelayCommands(ActionWithAnimation(a => selectedCopier.Copy(d, ExcelActions.GetContainsHeader(d)), d), a => selectedCopier.CanCopy(d));
            ICommand pasteCommand   = new RelayCommands(ActionWithAnimation(a => itemsSourceInserts.Insert(d,
                                                                                     Clipboard.GetText(),
                                                                                     ExcelActions.GetShowErrorMessages(d),
                                                                                     ExcelActions.GetCancelWithErrors(d)),d),
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

                if(ExcelActions.GetPaintFlash(d)) colorator.FlashColor(control, ExcelActions.GetColorFlash(d).Color);
            };

        }




    }
}
