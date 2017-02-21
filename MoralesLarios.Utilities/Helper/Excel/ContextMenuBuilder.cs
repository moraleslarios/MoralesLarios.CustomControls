using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using MoralesLarios.Utilities.Helper.BrushesHelper;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal class ContextMenuBuilder
    {

        public void CreateContextMenuCopies(FrameworkElement control, ICommand commandCopyAll, ICommand commandCopy)
        {
            if(control.ContextMenu == null) control.ContextMenu = new ContextMenu();

            MenuItem menuItemCopyAll = new MenuItem { Header = ConstStrings.COPY_ALL_MENU_HEADER, Command = commandCopyAll };
            control.ContextMenu.Items.Add(menuItemCopyAll);
            MenuItem menuItemCopy    = new MenuItem { Header = ConstStrings.COPY_MENU_HEADER, Command = commandCopy };
            control.ContextMenu.Items.Add(menuItemCopy);
        }

        public void CreateContextMenuPaste(FrameworkElement control, ICommand commandPaste)
        {
            if (control.ContextMenu == null) control.ContextMenu = new ContextMenu();

            MenuItem menuItemPaste = new MenuItem { Header = ConstStrings.PASTE_ALL_MENU_HEADER, Command = commandPaste };
            control.ContextMenu.Items.Add(menuItemPaste);
        }

        public void DeleteMenusCopies(FrameworkElement control)
        {
            var headers = new string[] { ConstStrings.COPY_ALL_MENU_HEADER, ConstStrings.COPY_MENU_HEADER };

            DeleteMenu(control, headers);
        }

        public void DeleteMenusPaste(FrameworkElement control)
        {
            var headers = new string[] { ConstStrings.PASTE_ALL_MENU_HEADER };

            DeleteMenu(control, headers);
        }


        private void DeleteMenu(FrameworkElement control, string[] headers)
        {
            if (control.ContextMenu == null) return;

            foreach (var menuItem in control.ContextMenu.Items)
            {
                var menuItemCast = menuItem as MenuItem;

                if (headers.Contains(menuItemCast?.Header.ToString()))
                {
                    menuItemCast.Visibility = Visibility.Collapsed;
                }
            }
        }


        public void CreateContextMenuCopiesForCondition(FrameworkElement control, ICommand commandCopyAll, ICommand commandCopy, bool condition, bool actionCondition)
        {
            DeleteMenusCopies(control);

            if (condition && actionCondition)
            {
                CreateContextMenuCopies(control, commandCopyAll, commandCopy);
            }

        }

        public void CreateContextMenuPasteForCondition(FrameworkElement control, ICommand commandPaste, bool condition, bool actionCondition)
        {
            DeleteMenusPaste(control);

            if (condition && actionCondition)
            {
                CreateContextMenuPaste(control, commandPaste);
            }

        }



    }
}
