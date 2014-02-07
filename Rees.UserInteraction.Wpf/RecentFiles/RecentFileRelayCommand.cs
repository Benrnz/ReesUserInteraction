using System;
using GalaSoft.MvvmLight.Command;

namespace Rees.Wpf.RecentFiles
{
    public class RecentFileRelayCommand : RelayCommand<string>
    {
        public RecentFileRelayCommand(string name, string fullFileName, Action<string> execute) : base(execute)
        {
            Name = name;
            FullFileName = fullFileName;
        }

        public RecentFileRelayCommand(string name, string fullFileName, Action<string> execute,
            Func<string, bool> canExecute) : base(execute, canExecute)
        {
            Name = name;
            FullFileName = fullFileName;
        }

        public string FullFileName { get; private set; }
        public string Name { get; private set; }
    }
}