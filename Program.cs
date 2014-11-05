using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using GitterBot.Bots;

namespace GitterBot
{
    class Program
    {
        static void Main(string[] args)
        {
           System.Threading.Thread th=new System.Threading.Thread (WelcomeMsgBot.WelcomeUsers);
           th.Start();
           System.Threading.Thread th2 = new System.Threading.Thread(Messages.GetMessages);
           th2.Start();
        }
    }
}
