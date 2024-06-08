using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System.Windows.Controls;

namespace ManagamentCompanyDiplom.Classes
{
    public static class ImageHelper
    {
        public static void UpdateProfileImageOnAllForms(string imagePath)
        {
            foreach (Window window in Application.Current.Windows)
            {
                UpdateProfileImageInWindow(window, imagePath);
            }
        }

        public static void UpdateProfileImageInWindow(Window window, string imagePath)
        {
            // Обновляем изображение на всех формах
            var imageControls = FindVisualChildren<Image>(window);
            foreach (var img in imageControls)
            {
                if (img.Name == "ProfileImage" || img.Name == "ProfileImageMenu")
                {
                    img.Source = new BitmapImage(new Uri(imagePath));
                }
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
