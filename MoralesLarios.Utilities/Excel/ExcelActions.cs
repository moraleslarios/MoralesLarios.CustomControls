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



        static ExcelActions()
        {
            allCopier                    = new AllCopyItems();
            selectedCopier               = new SelectedCopyItems();
            colorator                    = new Colorator();
            contextMenuBuilder           = new ContextMenuBuilder();
            builderObjects               = new BuilderObjects();
            itemsSourceInserts           = new ItemsSourceIntems(builderObjects);
            excelActionsCommandGenerator = new ExcelActionsCommandGenerator(allCopier, selectedCopier, itemsSourceInserts, colorator);
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
            if ((bool)e.NewValue == false) return;

            var itemsControl = d as ItemsControl;

            var commands = excelActionsCommandGenerator.GenerateCommands(d, GetContainsHeader(d), GetShowErrorMessages(d), GetCancelWithErrors(d));

            contextMenuBuilder.CreateContextMenuCopiesForCondition(d as FrameworkElement, commands.CopyAllCommmand, commands.CopyCommand, GetCreateContextMenu(d));

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
            var frameworkElement = d as FrameworkElement;

            var commands = excelActionsCommandGenerator.GenerateCommands(d, GetContainsHeader(d), GetShowErrorMessages(d), GetCancelWithErrors(d));

            contextMenuBuilder.CreateContextMenuPasteForCondition(d as FrameworkElement, commands.PasteCommand, GetCreateContextMenu(d));

            frameworkElement.InputBindings.Add(
                    new KeyBinding { Key = Key.V, Modifiers = ModifierKeys.Control, Command = commands.PasteCommand }
                );
        }



        #endregion




        #region Control AttachProperties



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

            //var frammeworkElement = d as FrameworkElement;

            //if (frammeworkElement.ContextMenu == null) return baseValue;

            //var headers = new string[] { "Copy All       (Ctrl + A)", "Copy Selecteds (Ctrl + A)", "Paste          (Ctrl + V)" };

            //bool allHiden = true;

            //foreach (var menuItem in frammeworkElement.ContextMenu.Items)
            //{
            //    var menuItemCast = menuItem as MenuItem;

            //    if (headers.Contains(menuItemCast?.Header.ToString()))
            //    {
            //        menuItemCast.Visibility = change ? Visibility.Visible : Visibility.Collapsed;
            //    }
            //    else
            //    {
            //        allHiden = false;
            //    }
            //}

            //frammeworkElement.ContextMenu.Visibility = allHiden ? Visibility.Collapsed : Visibility.Visible;

            return baseValue;
        }

        private static void OnCreateContextMenuPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool change = (bool)e.NewValue;

            var frammeworkElement = d as FrameworkElement;

            var commands = excelActionsCommandGenerator.GenerateCommands(d, GetContainsHeader(d), GetShowErrorMessages(d), GetCancelWithErrors(d));

            contextMenuBuilder.CreateContextMenuCopiesForCondition(d as FrameworkElement, commands.CopyAllCommmand, commands.CopyCommand, GetCreateContextMenu(d));
            contextMenuBuilder.CreateContextMenuPasteForCondition (d as FrameworkElement, commands.PasteCommand   , GetCreateContextMenu(d));
        }





        #endregion



    }
}
