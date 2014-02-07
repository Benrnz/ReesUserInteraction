using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Threading;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf.UserInteraction
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
        Justification = "Used for coding style only.")]
    public sealed class WpfWaitCursor : IWaitCursor
    {
        private readonly Dispatcher dispatcher;

        public WpfWaitCursor()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.dispatcher = Dispatcher.CurrentDispatcher;
        }

        [SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly",
            Justification = "Used for coding style only.")]
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly",
            Justification = "Used for coding style only.")]
        public void Dispose()
        {
            this.dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle,
                new Action(() => Mouse.OverrideCursor = null));
        }
    }
}