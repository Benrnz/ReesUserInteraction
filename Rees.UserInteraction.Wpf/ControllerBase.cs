using System.Windows.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Rees.Wpf
{
    public class ControllerBase : ViewModelBase
    {
        private readonly Dispatcher doNotUseDispatcher;
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        // Required for testing
        // ReSharper restore FieldCanBeMadeReadOnly.Local

        public ControllerBase()
        {
            // This relies on the Xaml being responsible for instantiating the controller.
            // Or at least the main UI thread.
            this.doNotUseDispatcher = Dispatcher.CurrentDispatcher;
        }

        protected Dispatcher Dispatcher
        {
            get { return this.doNotUseDispatcher; }
        }
    }
}