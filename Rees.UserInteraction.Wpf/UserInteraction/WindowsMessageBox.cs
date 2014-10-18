using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;

namespace Rees.Wpf.UserInteraction
{
    public class WindowsMessageBox : MessageBoxBase
    {
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed",
            Justification = "Optional parameters are prefered unless theres a reason not to.")]
        public override void Show(string message, string headingCaption = "")
        {
            string heading;
            bool unowned = false;
            try
            {
                heading = string.IsNullOrWhiteSpace(headingCaption)
                    ? Application.Current.MainWindow.Title ?? string.Empty
                    : headingCaption;
            }
            catch (InvalidOperationException)
            {
                // This will occur if another thread accesses Application.Current.MainWindow other than the main thread.
                heading = string.Empty;
                unowned = true;
            }

            string content = RationaliseMessage(message);


            if (unowned)
            {
                MessageBox.Show(content, heading);
                return;
            }

            try
            {
                if (Application.Current.MainWindow != null)
                {
                    MessageBox.Show(Application.Current.MainWindow, content, heading);
                }
                else
                {
                    MessageBox.Show(content);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(content, heading);
            }
        }

        public override void Show(Exception ex, string message)
        {
            if (ex == null)
            {
                Show(message);
                return;
            }

            string exText = ex.ToString();
            if (exText.Length > 400)
            {
                exText = exText.Substring(0, 400);
            }

            Show(message + "\n\n" + exText);
        }

        public override void Show(string format, object argument1, params object[] args)
        {
            Show(string.Format(CultureInfo.CurrentCulture, format, PrependElement(argument1, args)));
        }

        public override void Show(string headingCaption, string format, object argument1, params object[] args)
        {
            Show(string.Format(CultureInfo.CurrentCulture, format, PrependElement(argument1, args)), headingCaption);
        }

        public override void Show(Exception ex, string format, object argument1, params object[] args)
        {
            Show(ex, string.Format(CultureInfo.CurrentCulture, format, PrependElement(argument1, args)));
        }

        private static string RationaliseMessage(string message)
        {
            if (message.Length > 1024)
            {
                message = message.Substring(0, 1024) + "\n---TRUNCATED---";
            }

            return message;
        }
    }
}