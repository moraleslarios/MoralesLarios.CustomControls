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

            var newType = items.GetType();

            var type = GetElementType(newType);

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

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }

        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }
    }

}