using System;
using System.Windows.Threading;

namespace Rees.Wpf
{
    public static class DispatcherExtension
    {
        public static DispatcherOperation BeginInvoke(this Dispatcher instance, DispatcherPriority priority,
            Action lambdaToInvoke)
        {
            return instance.BeginInvoke(priority, new Action(lambdaToInvoke));
        }
    }
}