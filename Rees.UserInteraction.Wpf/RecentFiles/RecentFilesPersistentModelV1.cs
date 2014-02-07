using System;
using System.Collections.Generic;

namespace Rees.Wpf.RecentFiles
{
    public class RecentFilesPersistentModelV1 : IPersistent
    {
        public RecentFilesPersistentModelV1()
        {
        }

        public RecentFilesPersistentModelV1(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var dictionaryOfFiles = model as Dictionary<string, RecentFileV1>;
            if (dictionaryOfFiles == null)
            {
                throw new ArgumentException(
                    "model argument should be a Dictionary<string, RecentFileV1> and its " + model.GetType(), "model");
            }

            Model = dictionaryOfFiles;
        }

        public object Model { get; set; }

        public T AdaptModel<T>()
        {
            return (T) Model;
        }
    }
}