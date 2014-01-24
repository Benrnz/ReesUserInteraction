using System;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf.UserInteraction
{
    public class WindowsInputBox : IUserInputBox 
    {
        private readonly IViewLoader viewLoader;

        public WindowsInputBox(IViewLoader viewLoader)
        {
            if (viewLoader == null)
            {
                throw new ArgumentNullException("viewLoader");
            }

            this.viewLoader = viewLoader;
        }

        public string Show(string heading, string question, string defaultInput = "")
        {
            Heading = heading;
            Question = question;
            Input = defaultInput;
            var result = this.viewLoader.ShowDialog(this);

            if (result == null || result == false)
            {
                Input = null;
                return null;
            }

            Heading = string.Empty;
            Question = string.Empty;
            return Input ?? string.Empty;
        }

        public string Heading { get; set; }

        public string Question { get; set; }

        public string Input { get; set; }
    }
}
