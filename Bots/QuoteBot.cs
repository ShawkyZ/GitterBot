using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitterBot.Bots
{
    class QuoteBot
    {
        public static void GetQuote(string messageText,string username)
        {
            WebClient wb = new WebClient();
            //Get Random quote from all-famous-quotes.com/quotes.generator.html
            string response = wb.DownloadString("http://www.all-famous-quotes.com/quotes_generator.html");
            //TODO:Get the quote only and remove any additional text.
            string raw1 = response.Substring(response.IndexOf("<blockquote class=\"new\">"));
            string quote = raw1.Remove(raw1.IndexOf("- <a href=\"http://www.all-famous-quotes.com")).Replace("<blockquote class=\"new\">", "");
            Messages.postMessage(quote + "@" + username);
        }
    }
}
