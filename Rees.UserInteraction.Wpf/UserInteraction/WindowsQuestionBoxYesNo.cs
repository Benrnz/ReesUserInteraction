using System.Globalization;
using System.Windows;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf.UserInteraction
{
    public class WindowsQuestionBoxYesNo : IUserQuestionBoxYesNo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Optional parameters are prefered unless theres a reason not to.")]
        public bool? Show(string question, string heading = "")
        {
            var result = MessageBox.Show(question, heading, MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return null;
            }
        }

        public bool? Show(string heading, string questionFormat, object argument1, params object[] args)
        {
            return Show(string.Format(CultureInfo.CurrentCulture, questionFormat, argument1, args), heading);
        }
    }
}
