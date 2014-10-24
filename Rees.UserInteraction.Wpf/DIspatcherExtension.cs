using System;
using System.Windows.Threading;

namespace Rees.Wpf
{
    public static class DispatcherExtension
    {
        /// <summary>
        /// An extension method to allow queueing a call to the UI dispatcher using a lambda expression.
        /// </summary>
        public static DispatcherOperation BeginInvoke(this Dispatcher instance, DispatcherPriority priority,
            Action lambdaToInvoke)
        {
            return instance.BeginInvoke(priority, new Action(lambdaToInvoke));
        }
    }
}