using System.Collections.Generic;

namespace Rees.Wpf
{
    public interface IRecentFileManager
    {
        IEnumerable<KeyValuePair<string, string>> AddFile(string fullFileName);

        IEnumerable<KeyValuePair<string, string>> Files();

        IEnumerable<KeyValuePair<string, string>> Remove(string fullFileName);

        IEnumerable<KeyValuePair<string, string>> UpdateFile(string fullFileName);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Method will do significant work to create and return a object for persistence.")]
        IPersistent GetPersistentData();
    }
}