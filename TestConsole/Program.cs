using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using SimpleConfig;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Config cfg = new Config("Fuck",true,true);

            cfg["hello"] = "world";



            cfg.Save();
        }
    }
}
