using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xaml;
using Rees.UserInteraction.Contracts;
using Rees.Wpf.Annotations;

namespace Rees.Wpf.ApplicationState
{
    public class PersistApplicationStateAsXaml : IPersistApplicationState
    {
        private const string FileName = "BudgetAnalyserAppState.xml";
        private readonly IUserMessageBox userMessageBox;

        private string doNotUseFullFileName;

        public PersistApplicationStateAsXaml([NotNull] IUserMessageBox userMessageBox)
        {
            if (userMessageBox == null)
            {
                throw new ArgumentNullException("userMessageBox");
            }

            this.userMessageBox = userMessageBox;
        }

        protected virtual string FullFileName
        {
            get
            {
                if (string.IsNullOrEmpty(this.doNotUseFullFileName))
                {
                    string location = Path.GetDirectoryName(GetType().Assembly.Location);
                    this.doNotUseFullFileName = Path.Combine(location, FileName);
                }

                return this.doNotUseFullFileName;
            }
        }

        public IEnumerable<IPersistent> Load()
        {
            if (!File.Exists(FullFileName))
            {
                return new List<IPersistent>();
            }

            try
            {
                object serialised = XamlServices.Load(FullFileName); // Will always succeed without exceptions even if bad file format, but will return null.
                var correctFormat = serialised as List<IPersistent>;
                if (correctFormat == null)
                {
                    throw new BadApplicationStateFileFormatException(
                        string.Format(CultureInfo.InvariantCulture, "The file used to store application state ({0}) is not in the correct format. It may have been tampered with.", FullFileName));
                }

                return correctFormat;
            }
            catch (IOException ex)
            {
                this.userMessageBox.Show(ex, ex.Message);
                return new List<IPersistent>();
            }
        }

        public void Persist(IEnumerable<IPersistent> modelsToPersist)
        {
            var data = new List<IPersistent>(modelsToPersist.ToList());
            string serialised = XamlServices.Save(data);
            try
            {
                File.WriteAllText(FullFileName, serialised);
            }
            catch (IOException ex)
            {
                this.userMessageBox.Show(ex, "Unable to save recently used files.");
            }
        }
    }
}