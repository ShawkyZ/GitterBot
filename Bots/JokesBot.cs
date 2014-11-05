using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitterBot.Bots
{
    class JokesBot
    {
        public static void GetJoke(string username)
        {
            WebClient wb = new WebClient();
            string rawhtml = wb.DownloadString("http://www.ajokeaday.com/ChisteAlAzar.asp");
            string raw1 = rawhtml.Substring(rawhtml.IndexOf(("<div class=\"chiste\">")));
            string raw2 = raw1.Remove(raw1.IndexOf("<div class=\"opciones\">"));
            string joke = Regex.Replace(raw2, "<.*?>", string.Empty);
            Messages.postMessage(joke + " @" + username);

        }
    }
}
