using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace SharpConfig
{
    /// <summary>
    /// The basis of the SharpConfig configuration system.</summary>
    public class Config
    {
        private Dictionary<string, object> Values = new Dictionary<string, object>();
        /// <summary>
        /// Changes which namespace the configuration saves to. You must create a new object to load from a new namespace.</summary>
        public readonly string Namespace;
        /// <summary>
        /// Whether or not the configuration will be automatically saved every time a value is modified. </summary>
        public readonly bool AutoSave;

        private readonly string _dataFile;
        private readonly string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// The constructor. Loads the configuration from the specified namespace on construction. </summary>
        /// <param name="nameSpace">Designates the configuration that will be loaded from and later saved to.</param>
        /// <param name="saveLocal">Designates whether the configuration will be saved in the executable's directory or not.</param>
        /// <param name="autoSave">Designates whether the configuration will be saved everytime it is changed.</param>
        public Config(string nameSpace, bool saveLocal = false, bool autoSave = false)
        {
            Namespace = nameSpace;
            AutoSave = autoSave;

            if (!saveLocal)
            {
                Directory.CreateDirectory(Path.Combine(appData, Namespace));

                _dataFile = Path.Combine(appData, Namespace, "config.json");
            }
            else
            {
                //find where the exe of the application is stored
                //rather then using the working directory which may change
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //but if exeDirectory fails, we use the working directory.
                _dataFile = Path.Combine(exeDirectory ?? Environment.CurrentDirectory, nameSpace + ".json");
            }

            Load();
        }

        /// <summary>
        /// Indexer for the configuration, providing dynamic access to the collection of values in the configuration.</summary>
        /// <param name="key">Designates what configuration item will be changed or retrieved.</param>
        public dynamic this[string key]
        {
            get => Values[key];
            set
            {
                Values[key] = value;

                if (AutoSave)
                    Save();
            }
        }

        /// <summary>
        /// Saves the configuration to the disk.</summary>
        public void Save()
        {
            var json = JsonConvert.SerializeObject(Values,Formatting.Indented);
            File.WriteAllText(_dataFile,json);
        }

        /// <summary>
        /// Removes an item from the collection.</summary>
        public void Delete(string key)
        {
            Values.Remove(key);

            if (AutoSave)
                Save();
        }

        /// <summary>
        /// Return true if config file exist
        /// </summary>
        /// <returns></returns>
        public bool IsConfigFileExists()
        {
            return File.Exists(_dataFile);
        }

        /// <summary>
        /// Return true if value, by key, is exist, and set value 
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">out value if exist</param>
        /// <returns>true if value is exist</returns>
        public bool TryGetValue(string key, out dynamic value)
        {
            return Values.TryGetValue(key, out value);
        }

        /// <summary>
        /// Reloads the configuration from the disk.</summary>
        private void Load()
        {
            if (IsConfigFileExists())
            {
                var json = File.ReadAllText(_dataFile);
                Values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
        }
    }
}