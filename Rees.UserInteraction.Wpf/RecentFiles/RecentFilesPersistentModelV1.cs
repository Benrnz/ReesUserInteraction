using System;
using System.Collections.Generic;

namespace Rees.Wpf.RecentFiles
{
    /// <summary>
    ///     A <see cref="IPersistent" /> implementation to store the recently used files list.
    /// </summary>
    public class RecentFilesPersistentModelV1 : IPersistent
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RecentFilesPersistentModelV1" /> class.
        /// </summary>
        public RecentFilesPersistentModelV1()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RecentFilesPersistentModelV1" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentNullException">model</exception>
        /// <exception cref="System.ArgumentException">
        /// Will be thrown if the <paramref name="model"/> is not castable to Dictionary&lt;string, RecentFileV1&gt;
        /// </exception>
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

        /// <summary>
        ///     Gets the sequence number for this implementation. This is used to broadcast more crucial higher priority persistent
        ///     data out first, if any.
        /// </summary>
        public int Sequence
        {
            get { return 100; }
        }

        /// <summary>
        /// Gets or sets the model to persist to permentant storage.
        /// </summary>
        public object Model { get; set; }

        /// <summary>
        /// Adapts the model into the given type. This is a convience method to get the model and cast it in one tidy call.
        /// </summary>
        /// <typeparam name="T">The type to attempt to cast to.</typeparam>
        public T AdaptModel<T>()
        {
            return (T) Model;
        }
    }
}