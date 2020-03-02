using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace SPLab.Styles
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            if (((FrameworkElement)templateFrameworkElement).TemplatedParent is Window window)
                action(window);
        }
    }
    public partial class MacStyle
    {
        private Border border;
        private StackPanel stackPanel;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Window).StateChanged += Window_StateChanged;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => SystemCommands.CloseWindow(w));
        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => SystemCommands.MinimizeWindow(w));
        }
        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w =>
            {
                if (w.WindowState == WindowState.Maximized)
                {
                    SystemCommands.RestoreWindow(w);
                }
                else
                {
                    SystemCommands.MaximizeWindow(w);
                }
            });
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            Window window = sender as Window;
            if (border == null || stackPanel == null)
            {
                border = (Border)window.Template.FindName("gridMove", window);
                stackPanel = (StackPanel)window.Template.FindName("buttonsStackPanel", window);
            }

            if (window.WindowState == WindowState.Maximized)
            {
                border.Height = 30;
                stackPanel.Margin = new Thickness(10, 5, 0, 0);
            }
            else if (window.WindowState == WindowState.Normal)
            {
                border.Height = 25;
                stackPanel.Margin = new Thickness(10, 0, 0, 0);
            }
        }


    }
}
