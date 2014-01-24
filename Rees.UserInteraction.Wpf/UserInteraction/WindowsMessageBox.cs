using System;
using System.Globalization;
using System.Windows;

namespace Rees.Wpf.UserInteraction
{
    public class WindowsMessageBox : MessageBoxBase
    {
        private static string RationaliseMessage(string message)
        {
            if (message.Length > 1024)
            {
                message = message.Substring(0, 1024) + "\n---TRUNCATED---";
            }

            return message;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Optional parameters are prefered unless theres a reason not to.")]
        public override void Show(string message, string headingCaption = "")
        {
            // Ensure on the UI thread.
            string content = string.Format(
                CultureInfo.CurrentCulture,
                "{0}{1}{2}",
                headingCaption,
                string.IsNullOrWhiteSpace(headingCaption) ? string.Empty : "\n\n",
                RationaliseMessage(message));
            if (Application.Current.MainWindow != null)
            {
                MessageBox.Show(Application.Current.MainWindow, content, headingCaption);
            }
            else
            {
                MessageBox.Show(content);
            }
        }

        public override void Show(Exception ex, string message)
        {
            if (ex == null)
            {
                this.Show(message);
                return;
            }

            string exText = ex.ToString();
            if (exText.Length > 400)
            {
                exText = exText.Substring(0, 400);
            }

            this.Show(message + "\n\n" + exText);
        }

        public override void Show(string format, object argument1, params object[] args)
        {
            this.Show(string.Format(CultureInfo.CurrentCulture, format, PrependElement(argument1, args)));
        }

        public override void Show(string headingCaption, string format, object argument1, params object[] args)
        {
            this.Show(string.Format(CultureInfo.CurrentCulture, format, PrependElement(argument1, args)), headingCaption);
        }

        public override void Show(Exception ex, string format, object argument1, params object[] args)
        {
            this.Show(ex, string.Format(CultureInfo.CurrentCulture, format, PrependElement(argument1, args)));
        }
    }
}