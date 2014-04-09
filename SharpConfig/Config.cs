using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace SimpleConfig
{
    public class Config
    {
        public Dictionary<string,object> Values = new Dictionary<string, object>();
        public string Namespace;
        public bool AutoSave;

        private string DataFile;
        private string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public Config(string nameSpace,bool saveLocal = false,bool autoSave = false)
        {
            Namespace = nameSpace;
            AutoSave = autoSave;

            if (!saveLocal)
            {
                Directory.CreateDirectory(Path.Combine(appData, Namespace));

                DataFile = Path.Combine(appData, Namespace, "config.json");
            }
            else
            {
                //find where the exe of the application is stored
                //rather then using the working directory which may change
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //but if exeDirectory fails, we use the working directory.
                DataFile = Path.Combine(exeDirectory ?? Environment.CurrentDirectory, nameSpace + ".json");
            }

            Load();
        }

        public dynamic this[string s]
        {
            get
            {
                return Values[s];
            }
            set
            {
                Values[s] = value;

                if (AutoSave)
                    Save();
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Values,Formatting.Indented);
            File.WriteAllText(DataFile,json);
        }

        public void Load()
        {
            if (File.Exists(DataFile))
            {
                string json = File.ReadAllText(DataFile);
                Values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
        }
    }
}