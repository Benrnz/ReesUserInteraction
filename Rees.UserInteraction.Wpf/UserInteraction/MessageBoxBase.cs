using System;
using System.Diagnostics.CodeAnalysis;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf.UserInteraction
{
    public abstract class MessageBoxBase : IUserMessageBox
    {
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed",
            Justification = "Optional parameters are prefered unless theres a reason not to.")]
        public abstract void Show(string message, string headingCaption = "");

        public abstract void Show(string headingCaption, string format, object argument1, params object[] args);
        public abstract void Show(string format, object argument1, params object[] args);
        public abstract void Show(Exception ex, string message);
        public abstract void Show(Exception ex, string format, object argument1, params object[] args);

        protected static object[] PrependElement(object newElement, object[] existingArray)
        {
            if (existingArray == null)
            {
                throw new ArgumentNullException("existingArray");
            }

            object[] stringArgs;
            if (existingArray.Length > 0)
            {
                stringArgs = new object[existingArray.Length + 1];
                Array.Copy(existingArray, 0, stringArgs, 1, existingArray.Length);
                stringArgs[0] = newElement;
            }
            else
            {
                stringArgs = new[] {newElement};
            }
            return stringArgs;
        }
    }
}