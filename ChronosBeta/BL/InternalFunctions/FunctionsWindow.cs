using ChronosBeta.View;
using FontAwesome.Sharp;

using System.Windows;
using System.Windows.Media;

namespace ChronosBeta.BL
{
     class FunctionsWindow
     {
        /// <summary>
        /// Запуск окна с ошибкой
        /// </summary>
        /// <param name="text">Текст ошибки</param>
        public static void OpenErrorWindow(string text)
        {
            InfoView errorView = new InfoView(text, "Текст ошибки:", "Ошибка", IconChar.Xmark, Brushes.Red);
            errorView.Show();
        }

        /// <summary>
        /// Запуск окна подтверждения
        /// </summary>
        /// <param name="text">Текст подтверждения</param>
        public static void OpenGoodWindow(string text)
        {
            InfoView errorView = new InfoView(text, "Выполнено действие:", "Успешно", IconChar.Check, Brushes.Green);
            errorView.Show();
        }

        /// <summary>
        /// Запуск окна предупреждения
        /// </summary>
        /// <param name="text">Текст предупреждения</param>
        public static void OpenConfrumWindow(string text)
        {
            InfoView errorView = new InfoView(text, "Текст предупреждения:", "Предупреждение", IconChar.Exclamation, Brushes.Yellow);
            errorView.Show();
        }

        /// <summary>
        /// Запуск окна диалога
        /// </summary>
        /// <param name="text">Текст диалога</param>
        /// <returns></returns>
        public static bool OpenDialogWindow(string text)
        {
            DialogView errorView = new DialogView(text);
            return (bool)errorView.ShowDialog();
        }

        /// <summary>
        /// Запуск окна фильтрации
        /// </summary>
        /// <param name="newDialogWindow">Окно фильтрации</param>
        /// <returns></returns>
        public static bool OpenFilterWindow(Window newDialogWindow)
        {
            return (bool)newDialogWindow.ShowDialog();
        }
     }
}