using System.Collections.Generic;

namespace Rees.Wpf
{
    public interface IPersistApplicationState
    {
        IEnumerable<IPersistent> Load();
        void Persist(IEnumerable<IPersistent> modelsToPersist);
    }
}
