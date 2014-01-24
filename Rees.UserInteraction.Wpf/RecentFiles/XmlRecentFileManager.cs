using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Rees.Wpf.Annotations;
using Rees.Wpf.ApplicationState;

namespace Rees.Wpf.RecentFiles
{
    public class XmlRecentFileManager : IRecentFileManager
    {
        private readonly Func<object, IPersistent> persistentModelFactory;

        private Dictionary<string, RecentFileV1> files = new Dictionary<string, RecentFileV1>();

        public XmlRecentFileManager(
            [NotNull] Func<object, IPersistent> persistentModelFactory,
            [NotNull] IMessenger messenger)
        {
            if (persistentModelFactory == null)
            {
                throw new ArgumentNullException("persistentModelFactory");
            }

            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }

            this.persistentModelFactory = persistentModelFactory;
            messenger.Register<ApplicationStateRequestedMessage>(this, OnPersistentDataRequested);
            messenger.Register<ApplicationStateLoadedMessage>(this, OnApplicationStateLoaded);
        }

        public IEnumerable<KeyValuePair<string, string>> AddFile(string fullFileName)
        {
            if (this.files.ContainsKey(fullFileName))
            {
                this.files[fullFileName].When = DateTime.Now;
            }
            else
            {
                var newFile = new RecentFileV1 { FullFileName = fullFileName, Name = GetName(fullFileName), When = DateTime.Now };
                this.files.Add(fullFileName, newFile);
            }

            return ConvertAndReturnRecentFiles();
        }

        public IEnumerable<KeyValuePair<string, string>> Files()
        {
            return ConvertAndReturnRecentFiles();
        }

        public IPersistent GetPersistentData()
        {
            return this.persistentModelFactory(this.files);
        }

        public IEnumerable<KeyValuePair<string, string>> Remove(string fullFileName)
        {
            if (!this.files.ContainsKey(fullFileName))
            {
                return ConvertAndReturnRecentFiles();
            }

            this.files.Remove(fullFileName);
            return ConvertAndReturnRecentFiles();
        }

        public IEnumerable<KeyValuePair<string, string>> UpdateFile(string fullFileName)
        {
            if (!this.files.ContainsKey(fullFileName))
            {
                return ConvertAndReturnRecentFiles();
            }

            this.files[fullFileName].When = DateTime.Now;
            return ConvertAndReturnRecentFiles();
        }

        protected virtual string GetName(string fullFileName)
        {
            return Path.GetFileName(fullFileName);
        }

        private IEnumerable<KeyValuePair<string, string>> ConvertAndReturnRecentFiles()
        {
            List<KeyValuePair<string, string>> results = this.files
                                                             .OrderByDescending(f => f.Value.When)
                                                             .Select(f => new KeyValuePair<string, string>(f.Key, f.Value.Name))
                                                             .ToList();
            return results;
        }

        private void OnApplicationStateLoaded(ApplicationStateLoadedMessage message)
        {
            IPersistent emptyModel = this.persistentModelFactory(this.files);
            if (message.RehydratedModels.ContainsKey(emptyModel.GetType()))
            {
                this.files = message.RehydratedModels[emptyModel.GetType()].AdaptModel<Dictionary<string, RecentFileV1>>();
            }
        }

        private void OnPersistentDataRequested(ApplicationStateRequestedMessage message)
        {
            message.PersistThisModel(GetPersistentData());
        }
    }
}