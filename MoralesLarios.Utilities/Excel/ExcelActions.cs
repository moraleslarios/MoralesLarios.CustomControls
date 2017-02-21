using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoralesLarios.Utilities.Helper.Excel;
using MoralesLarios.Utilities.Helper.BrushesHelper;
using System.Windows.Media;

namespace MoralesLarios.Utilities.Excel
{
    public class ExcelActions
    {

        private static ContextMenuBuilder           contextMenuBuilder;
        private static ExcelActionsCommandGenerator excelActionsCommandGenerator;
        private static Colorator                    colorator;

        private static IClipBoardCopier  allCopier;
        private static IClipBoardCopier  selectedCopier;
        private static IBuilderObjects   builderObjects;
        private static IItemsSourceItems itemsSourceInserts;
        //private static IKeyBindingInputs keyBindingsInputCopies;
        //private static IKeyBindingInputs keyBindingsInputPaste;
        //private static IKeyBindingWorker keyBindingWorker;



        static ExcelActions()
        {
            allCopier                    = new AllCopyItems();
            selectedCopier               = new SelectedCopyItems();
            colorator                    = new Colorator();
            contextMenuBuilder           = new ContextMenuBuilder();
            builderObjects               = new BuilderObjects();
            itemsSourceInserts           = new ItemsSourceIntems(builderObjects);
            excelActionsCommandGenerator = new ExcelActionsCommandGenerator(allCopier, selectedCopier, itemsSourceInserts, colorator);
            //keyBindingsInputCopies       = new KeyBindingInputsCopies();
            //keyBindingsInputPaste        = new KeyBindingInputsPaste();
            //keyBindingWorker             = new KeyBindingWorker();
        }








        #region EnabledCopyExcel



        public static bool GetEnabledCopyExcel(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledCopyExcelProperty);
        }

        public static void SetEnabledCopyExcel(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledCopyExcelProperty, value);
        }

        public static readonly DependencyProperty EnabledCopyExcelProperty =
            DependencyProperty.RegisterAttached("EnabledCopyExcel", typeof(bool), typeof(ExcelActions), new PropertyMetadata(false, OnEnabledCopyExcelChanged));

        private static void OnEnabledCopyExcelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = d as ItemsControl;

            var commands = CreateCommands(d);

            contextMenuBuilder.CreateContextMenuCopiesForCondition(d as FrameworkElement, commands.CopyAllCommmand, commands.CopyCommand, GetCreateContextMenu(d), GetEnabledCopyExcel(d));

            itemsControl.InputBindings.Add(
                    new KeyBinding { Key = Key.A, Modifiers = ModifierKeys.Control, Command = commands.CopyAllCommmand }
                );

            itemsControl.InputBindings.Add(
                    new KeyBinding { Key = Key.C, Modifiers = ModifierKeys.Control, Command = commands.CopyCommand }
                );
        }


        #endregion


        #region EnabledPasteExcel


        public static bool GetEnabledPasteExcel(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnabledPasteExcelProperty);
        }


        public static void SetEnabledPasteExcel(DependencyObject obj, bool value)
        {
            obj.SetValue(EnabledPasteExcelProperty, value);
        }

        public static readonly DependencyProperty EnabledPasteExcelProperty =
            DependencyProperty.RegisterAttached("EnabledPasteExcel", typeof(bool), typeof(ExcelActions), new PropertyMetadata(false, OnEnabledPasteExcelChanged));

        private static void OnEnabledPasteExcelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (GetEnabledPasteExcel(d) == false) return;

            var frameworkElement = d as FrameworkElement;

            var commands = CreateCommands(d);

            contextMenuBuilder.CreateContextMenuPasteForCondition(d as FrameworkElement, commands.PasteCommand, GetCreateContextMenu(d), GetEnabledPasteExcel(d));

            frameworkElement.InputBindings.Add(
                    new KeyBinding { Key = Key.V, Modifiers = ModifierKeys.Control, Command = commands.PasteCommand }
                );
        }



        #endregion




        #region Control AttachProperties




        public static bool GetPaintFlash(DependencyObject obj)
        {
            return (bool)obj.GetValue(PaintFlashProperty);
        }

        public static void SetPaintFlash(DependencyObject obj, bool value)
        {
            obj.SetValue(PaintFlashProperty, value);
        }

        // Using a DependencyProperty as the backing store for PaintFlash.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaintFlashProperty =
            DependencyProperty.RegisterAttached("PaintFlash", typeof(bool), typeof(ExcelActions), new PropertyMetadata(true, OnPaintFlashChanged));

        private static void OnPaintFlashChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComponentChange(d);
        }

        public static SolidColorBrush GetColorFlash(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(ColorFlashProperty);
        }

        public static void SetColorFlash(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(ColorFlashProperty, value);
        }

        // Using a DependencyProperty as the backing store for ColorFlash.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorFlashProperty =
            DependencyProperty.RegisterAttached("ColorFlash", typeof(SolidColorBrush), typeof(ExcelActions), new PropertyMetadata(Brushes.Gray, OnColorFlashChanged));

        private static void OnColorFlashChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComponentChange(d);
        }

        public static bool GetContainsHeader(DependencyObject obj)
        {
            return (bool)obj.GetValue(ContainsHeaderProperty);
        }

        public static void SetContainsHeader(DependencyObject obj, bool value)
        {
            obj.SetValue(ContainsHeaderProperty, value);
        }

        public static readonly DependencyProperty ContainsHeaderProperty =
            DependencyProperty.RegisterAttached("ContainsHeader", typeof(bool), typeof(ExcelActions), new PropertyMetadata(true));







        public static bool GetShowErrorMessages(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowErrorMessagesProperty);
        }

        public static void SetShowErrorMessages(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowErrorMessagesProperty, value);
        }

        public static readonly DependencyProperty ShowErrorMessagesProperty =
            DependencyProperty.RegisterAttached("ShowErrorMessages", typeof(bool), typeof(ExcelActions), new PropertyMetadata(true));







        public static bool GetCancelWithErrors(DependencyObject obj)
        {
            return (bool)obj.GetValue(CancelWithErrorsProperty);
        }

        public static void SetCancelWithErrors(DependencyObject obj, bool value)
        {
            obj.SetValue(CancelWithErrorsProperty, value);
        }

        public static readonly DependencyProperty CancelWithErrorsProperty =
            DependencyProperty.RegisterAttached("CancelWithErrors", typeof(bool), typeof(ExcelActions), new PropertyMetadata(true));






        public static bool GetCreateContextMenu(DependencyObject obj)
        {
            return (bool)obj.GetValue(CreateContextMenuProperty);
        }

        public static void SetCreateContextMenu(DependencyObject obj, bool value)
        {
            obj.SetValue(CreateContextMenuProperty, value);
        }

        // Using a DependencyProperty as the backing store for CreateContextMenu.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreateContextMenuProperty =
            DependencyProperty.RegisterAttached("CreateContextMenu", typeof(bool), typeof(ExcelActions), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnCreateContextMenuPropertyChanged), new CoerceValueCallback(OnCreateContextMenuCoerce)));

        private static object OnCreateContextMenuCoerce(DependencyObject d, object baseValue)
        {
            bool change = (bool)baseValue;

            return baseValue;
        }

        private static void OnCreateContextMenuPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ComponentChange(d);
        }





        #endregion


        private static void ComponentChange(DependencyObject d)
        {
            var frammeworkElement = d as FrameworkElement;

            var commands = CreateCommands(d);

            contextMenuBuilder.CreateContextMenuCopiesForCondition(d as FrameworkElement, commands.CopyAllCommmand, commands.CopyCommand, GetCreateContextMenu(d), GetEnabledCopyExcel(d));
            contextMenuBuilder.CreateContextMenuPasteForCondition (d as FrameworkElement, commands.PasteCommand, GetCreateContextMenu(d), GetEnabledPasteExcel(d));

            OnEnabledCopyExcelChanged (d, new DependencyPropertyChangedEventArgs());
            OnEnabledPasteExcelChanged(d, new DependencyPropertyChangedEventArgs());
        }


        private static ExcelActionsCommandInfo CreateCommands(DependencyObject d)
        {
            var parameters = BuildActionsParameters(d);

            var result = excelActionsCommandGenerator.GenerateCommands(d);

            return result;
        }



        private static ExcelActionsCommandsParameters BuildActionsParameters(DependencyObject d)
        {
            var result = new ExcelActionsCommandsParameters
            {
                ContainsHeader    = GetContainsHeader(d),
                ShowErrorMessages = GetShowErrorMessages(d),
                CancelWithErrors  = GetCancelWithErrors(d),
                PaintFlash        = GetPaintFlash(d),
                ColorFlash        = GetColorFlash(d)
            };

            return result;
        }



    }
}
