﻿using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf
{
    /// <summary>
    ///     Used to abstract away from instantiating and interacting with real windows within Controller and ViewModel code.
    /// </summary>
    /// <typeparam name="T">The view load.</typeparam>
    public class WpfViewLoader<T> : IViewLoader where T : Window, new()
    {
        public double? Height { get; set; }
        public double? MinHeight { get; set; }
        public double? MinWidth { get; set; }
        public double? Width { get; set; }
        protected T TargetWindow { get; set; }

        public virtual void Close()
        {
            if (TargetWindow != null)
            {
                TargetWindow.Close();
                TargetWindow = null;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Clean up code. The window is orphaned or has been instructed to close.")]
        public virtual void Show(object context)
        {
            if (TargetWindow != null)
            {
                try
                {
                    TargetWindow.Close();
                }
// ReSharper disable EmptyGeneralCatchClause
                catch 
// ReSharper restore EmptyGeneralCatchClause
                {
                    // Swallow any exception trying to close the orphaned window.
                }
            }
            
            TargetWindow = CreateWindow();
            ConfigureWindow(context);
            TargetWindow.Show();
        }

        protected virtual void ConfigureWindow(object context)
        {
            TargetWindow.DataContext = context;
            if (Height != null) TargetWindow.Height = Height.Value;
            if (Width != null) TargetWindow.Width = Width.Value;
            if (MinHeight != null) TargetWindow.MinHeight = MinHeight.Value;
            if (MinWidth != null) TargetWindow.MinWidth = MinWidth.Value;
        }

        protected virtual T CreateWindow()
        {
            return new T();
        }

        public virtual bool? ShowDialog(object context)
        {
            TargetWindow = new T {DataContext = context, Owner = Application.Current.MainWindow};
            bool? result = TargetWindow.ShowDialog();
            TargetWindow = null;
            return result;
        }
    }
}