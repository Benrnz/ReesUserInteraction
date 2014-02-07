using System;

namespace Rees.UserInteraction.Contracts
{
    /// <summary>
    ///     Represents a means to show a wait cursor to the user. It is intended to be used with a using block.
    /// </summary>
    public interface IWaitCursor : IDisposable
    {
    }
}