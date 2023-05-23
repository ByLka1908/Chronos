using ChronosBeta.View;
using FontAwesome.Sharp;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ChronosBeta.BL
{
     class FunctionsWindow
     {
        public static void OpenErrorWindow(string text)
        {
            InfoView errorView = new InfoView(text, "Текст ошибки:", "Ошибка", IconChar.Xmark, Brushes.Red);
            errorView.Show();
        }

        public static void OpenGoodWindow(string text)
        {
            InfoView errorView = new InfoView(text, "Выполнено действие:", "Успешно", IconChar.Check, Brushes.Green);
            errorView.Show();
        }

        public static void OpenConfrumWindow(string text)
        {
            InfoView errorView = new InfoView(text, "Текст предупреждения:", "Предупреждение", IconChar.Exclamation, Brushes.Yellow);
            errorView.Show();
        }

        public static bool OpenDialogWindow(string text)
        {
            DialogView errorView = new DialogView(text);
            return (bool)errorView.ShowDialog();
        }

        public static bool OpenFilterWindow(Window newDialogWindow)
        {
            return (bool)newDialogWindow.ShowDialog();
        }
     }
}