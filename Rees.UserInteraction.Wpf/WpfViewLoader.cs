using System.Windows;                                                    
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf
{
    public class WpfViewLoader<T> : IViewLoader where T : Window, new()
    {
        private T window;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Clean up code. The window is orphaned or has been instructed to close.")]
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

            this.window = new T { DataContext = context };
            this.window.Show();
        }

        public virtual void Close()
        {
            if (this.window != null)
            {
                this.window.Close();
                this.window = null;
            }
        }

        public virtual bool? ShowDialog(object context)
        {
            this.window = new T { DataContext = context, Owner = Application.Current.MainWindow };
            var result = this.window.ShowDialog();
            this.window = null;
            return result;
        }
    }
}
