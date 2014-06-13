using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf
{
    /// <summary>
    /// Used to abstract away from instantiating and interacting with real windows within Controller and ViewModel code.
    /// </summary>
    /// <typeparam name="T">The view load.</typeparam>
    public class WpfViewLoader<T> : IViewLoader where T : Window, new()
    {
        private T window;

        public virtual void Close()
        {
            if (this.window != null)
            {
                this.window.Close();
                this.window = null;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Clean up code. The window is orphaned or has been instructed to close.")]
        public virtual void Show(object context)
        {
            if (this.window != null)
            {
                try
                {
                    this.window.Close();
                }
// ReSharper disable EmptyGeneralCatchClause
                catch 
// ReSharper restore EmptyGeneralCatchClause
                {
                    // Swallow any exception trying to close the orphaned window.
                }
            }

            this.window = new T {DataContext = context};
            this.window.Show();
        }

        public virtual bool? ShowDialog(object context)
        {
            this.window = new T {DataContext = context, Owner = Application.Current.MainWindow};
            bool? result = this.window.ShowDialog();
            this.window = null;
            return result;
        }
    }
}