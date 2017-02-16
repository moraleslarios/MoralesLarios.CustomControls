using System;
using System.Collections;
using System.Windows;
using MoralesLarios.Utilities.Helper.Excel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MoralesLarios.Utilities.Helper.Excel
{
    internal class ItemsSourceIntems : IItemsSourceItems
    {
        private IBuilderObjects builderObjects;


        public ItemsSourceIntems(IBuilderObjects builderObjects)
        {
            this.builderObjects = builderObjects;
        }

        public void Insert(DependencyObject d, string dataStr, bool showErrorMessages, bool cancelWithErrors)
        {
            IList items = GetItemsFormDependencyProperty(d);

            var type = items[0].GetType();

            var newItems = builderObjects.BuildObject(dataStr, type, showErrorMessages, cancelWithErrors);

            InsertData(items, newItems);
        }

        private IList GetItemsFormDependencyProperty(DependencyObject dependencyObject)
        {
            var itemsControl = dependencyObject as ItemsControl;

            var result = itemsControl.ItemsSource as IList;

            if(result == null)
            {
                MessageBox.Show($"Paste from excel has compatibility only with ItemsSource implementing IList", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }


        private void InsertData(IList items, IEnumerable<object> newItems)
        {
            foreach (var newItem in newItems)
            {
                items.Add(newItem);
            }
        }
    }
}