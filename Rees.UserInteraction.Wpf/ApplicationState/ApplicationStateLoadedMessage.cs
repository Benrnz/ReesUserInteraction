using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace Rees.Wpf.ApplicationState
{
    public class ApplicationStateLoadedMessage : MessageBase
    {
        public ApplicationStateLoadedMessage(IEnumerable<IPersistent> rehydratedModels)
        {
            IEnumerable<IPersistent> removeDuplicates = rehydratedModels
                .GroupBy(model => model.GetType(), model => model)
                .Where(group => group.Key != null)
                .Select(group => group.First());

            RehydratedModels = new ReadOnlyDictionary<Type, IPersistent>(removeDuplicates.ToDictionary(m => m.GetType(), m => m));
        }

        public IReadOnlyDictionary<Type, IPersistent> RehydratedModels { get; private set; }
    }
}