using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShopProject.Helpers
{
    internal class ContextMenuHelper
    {
        public static readonly DependencyProperty OpenOnLeftClickProperty =
            DependencyProperty.RegisterAttached(
                "OpenOnLeftClick",
                typeof(bool),
                typeof(ContextMenuHelper),
                new PropertyMetadata(false, OnOpenOnLeftClickChanged));


        public static void SetOpenOnLeftClick(
            DependencyObject element,
            bool value)
        {
            element.SetValue(OpenOnLeftClickProperty, value);
        }


        public static bool GetOpenOnLeftClick(
            DependencyObject element)
        {
            return (bool)element.GetValue(OpenOnLeftClickProperty);
        }


        private static void OnOpenOnLeftClickChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is not Button button)
                return;

            if ((bool)e.NewValue)
            {
                button.Click += Button_Click;
            }
            else
            {
                button.Click -= Button_Click;
            }
        }


        private static void Button_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            if (button.ContextMenu == null)
                return;

            button.ContextMenu.PlacementTarget = button;
            button.ContextMenu.IsOpen = true;

            e.Handled = true;
        }
    }
} 
