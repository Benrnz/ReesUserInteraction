using System.Collections.Generic;
using System.Data;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace Rees.Wpf.ApplicationState
{
    public class ApplicationStateRequestedMessage : MessageBase
    {
        private readonly List<IPersistent> modelsToPersist = new List<IPersistent>();

        public IReadOnlyCollection<IPersistent> PersistentData
        {
            get { return this.modelsToPersist; }
        }

        public void PersistThisModel(IPersistent model)
        {
            if (this.modelsToPersist.Any(m => m.GetType() == model.GetType()))
            {
                throw new DuplicateNameException(
                    "Attempt to save application state with a model that has already been saved.");
            }

            this.modelsToPersist.Add(model);
        }
    }
}