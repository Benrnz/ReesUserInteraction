using System;
using System.Windows;
using Microsoft.Win32;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf.UserInteraction
{
    public class WindowsSaveFileDialog : IUserPromptSaveFile
    {
        public bool? AddExtension { get; set; }
        public bool? CheckPathExists { get; set; }
        public string DefaultExt { get; set; }
        public string FileName { get; private set; }
        public string Filter { get; set; }
        public int? FilterIndex { get; set; }
        public string Title { get; set; }

        public bool? ShowDialog()
        {
            var dialog = new SaveFileDialog();
            if (AddExtension != null)
            {
                dialog.AddExtension = AddExtension.Value;
            }

            if (CheckPathExists != null)
            {
                dialog.CheckPathExists = CheckPathExists.Value;
            }

            if (DefaultExt != null)
            {
                dialog.DefaultExt = DefaultExt;
            }

            if (Title != null)
            {
                dialog.Title = Title;
            }

            if (Filter != null)
            {
                dialog.Filter = Filter;
            }

            if (FilterIndex != null)
            {
                dialog.FilterIndex = FilterIndex.Value;
            }

            bool? result;
            try
            {
                Window owner = Application.Current.MainWindow;
                result = dialog.ShowDialog(owner);
            }
            catch (InvalidOperationException)
            {
                result = dialog.ShowDialog();
            }

            FileName = dialog.FileName;
            return result;
        }
    }
}