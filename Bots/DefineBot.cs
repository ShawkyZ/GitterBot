using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitterBot.Bots
{
    class DefineBot
    {
        public static void GetWordDefine(string messageText,string username)
        {
            WebClient wb = new WebClient();
            string after = messageText.Substring(messageText.IndexOf("define")).Replace("define", "").Replace(" ", ""); //TODO: Get the word from the sent message
            string response = "";
            try
            {
                response = wb.DownloadString("http://www.merriam-webster.com/dictionary/" + after);
            }
            catch
            {
                Messages.postMessage("Sorry,There is no definition for this world. " + "@" + username);
                return;
            }
            int cutIndex = response.IndexOf("<div class=\"ld_on_collegiate\">");
            if (cutIndex < 0)
            {
                Messages.postMessage("Sorry,There is no definition for this world. " + "@" + username);
                return;
            }
            //TODO:Get the definition of the word and remove any additional text.
            string raw1 = response.Substring(cutIndex);
            string definition = raw1.Remove(raw1.IndexOf("<p class=\"bottom_entry\">")).Replace("<div class=\"ld_on_collegiate\">", "").Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");
            Messages.postMessage("The definition for " + after + " is " + definition + "\nSource:http://www.merriam-webster.com/dictionary/" + after + " @" + username);
        }
    }
}
