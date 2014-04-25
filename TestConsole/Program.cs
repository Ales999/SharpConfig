using System;
using SimpleConfig;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //The namespace defines which configuration is loaded and saved to
            //This should be adequately unique and should only have filesystem safe characters
            string testNamespace = "TestingNamespace";


            //Make a new Config object. Previous saved items are automatically loaded from the disk
            Config cfg = new Config(testNamespace);


            //An example of saving a setting
            //In this, "hello" is the key and "world" is the object being saved
            cfg["hello"] = "world";

            //The key (in this, "test") must always be a string
            cfg["test"] = "Many testings.";

            //However, the item you save can be any JSON-serializable object
            cfg["number"] = 500;

            //You can retrieve an item using similar syntax
            Console.WriteLine(cfg["hello"]);

            //Items are returned as 'dynamic', meaning no casting is necessary
            int ourNumber = cfg["number"];

            //Call the Save method on your Config object to save your items to the disk
            //This isn't necessary if your Config object has autosave enabled
            //You can enable autosave in the Config constructor
            cfg.Save();
        }
    }
}
