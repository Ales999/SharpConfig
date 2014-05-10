SharpConfig
===========

Simple and easy user-persistent configuration library for .NET

Capable of saving and loading nearly any object onto the disk. Designed to be as easy to use as possible via the use of the *dynamic* keyword, JSON and a constant striving for slimness.


## Basic usage
```C#
using System;
using SimpleConfig;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //The namespace defines 'which' configuration should be loaded
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
```
## Requirements
Uses the Json.NET package from Newtonsoft for JSON serialization. 
JSON.NET's nuget package can be retrieved with

    Install-Package Newtonsoft.Json
    
in the nuget command line.
## Further configuration

### Autosave
Autosave can be enabled within the Config constructor to automatically save changes every time an item is changed.
This may have performance impacts on large configurations.
```C#
bool autoSave = true;

Config cfg = new Config("test",false,autoSave);
```
### Save Location
SharpConfig saves by default to the namespace designated folder within the Environment.SpecialFolder.LocalApplicationData folder.
It is possible to change this so as to save to the application executable path like so:
```C#
bool localSave = true;

Config cfg = new Config("test",localSave);
```
