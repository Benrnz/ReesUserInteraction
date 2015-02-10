using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Rees.UserInteraction.Contracts;

namespace Rees.Wpf.ApplicationState
{
    /// <summary>
    /// A <see cref="MessageBase"/> message object that contains recently loaded user data to be broadcast to subscribing components.
    /// This message will be broadcast with the <see cref="IMessenger"/> after the <see cref="IPersistApplicationState"/> implementation has read the data from persistent storage.
    /// </summary>
    public class ApplicationStateLoadedMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationStateLoadedMessage"/> class.
        /// </summary>
        /// <param name="rehydratedModels">The recently loaded user data from persistent storage that this message should carry to all subscribers.</param>
        public ApplicationStateLoadedMessage(IEnumerable<IPersistent> rehydratedModels)
        {
            IEnumerable<IPersistent> removeDuplicates = rehydratedModels
                .GroupBy(model => model.GetType(), model => model)
                .Where(group => group.Key != null)
                .Select(group => group.First());

            RehydratedModels =
                new ReadOnlyDictionary<Type, IPersistent>(removeDuplicates.ToDictionary(m => m.GetType(), m => m));
        }

        /// <summary>
        /// Gets the recently loaded user data from persistent storage that this message should carry to all subscribers.
        /// </summary>
        public IReadOnlyDictionary<Type, IPersistent> RehydratedModels { get; private set; }
    }
}