using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GitterBot.Bots;

namespace GitterBot
{
    class Messages
    {
        public static void postMessage(string msg)
        {
            WebClient wb = new WebClient();
            var data = new NameValueCollection();
            wb.Headers.Add("Authorization", Authentication.token_type + " " + Authentication.access_token);
            wb.Headers.Add("Accept", "application/json");
            data["text"] = msg;
            wb.UploadValues("https://api.gitter.im/v1/rooms/54577284db8155e6700d0a73/chatMessages", "POST", data);
        }
        public static void GetMessages()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                List<Message> mymessages = new List<Message>();
                using (var wb = new WebClient())
                {
                    //TODO:Send HTTP Get Request To Get The Last Messages.
                    wb.Headers.Add("Authorization", Authentication.token_type + " " + Authentication.access_token); //This is the Access Token I Got.
                    wb.Headers.Add("Host", "api.gitter.im");
                    var response = wb.DownloadString("https://api.gitter.im/v1/rooms/54577284db8155e6700d0a73/chatMessages?limit=1"); //I set the limit to 1 to get the last message ony , you can remove it and get the last 50 messages.
                    //TODO:Deserialize the Json Response To List Of Messages.
                    DataContractJsonSerializer contract = new DataContractJsonSerializer(typeof(List<Message>));
                    MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
                    mymessages = (List<Message>)contract.ReadObject(stream);
                }
                ReplyMessages(mymessages);
            }
        }

        static void ReplyMessages(List<Message> mymessages)
        {
            List<Message> postedids = new List<Message>();
            for (int i = 0; i < mymessages.Count; i++)
            {
                //Check if there are any mentions in this message with the screenName "testbot23" which is the bot screen name.
                if (mymessages[i].mentions.Count > 0)
                    if (mymessages[i].mentions[0].screenName == "testbot23")
                    {
                        //Check if I posted this message before to not post it again.
                        bool posted = postedids.Any(x => x.id == mymessages[i].id);
                        if (!posted)
                        {
                            //Check if the user wanted the bot to tell him a quote,compile code , say a joke or define a word , if he didn't the bot will just say "Yo".
                            if (mymessages[i].text.ToLower().Contains("compile"))
                                CompileCodeBot.GetCodeCompile(mymessages[i].text, mymessages[i].fromUser.username);
                            else if (mymessages[i].text.ToLower().Contains("define"))
                                DefineBot.GetWordDefine(mymessages[i].text, mymessages[i].fromUser.username);
                            else if (mymessages[i].text.ToLower().Contains("quote"))
                                QuoteBot.GetQuote(mymessages[i].text, mymessages[i].fromUser.username);
                            else if (mymessages[i].text.ToLower().Contains("joke"))
                                JokesBot.GetJoke(mymessages[i].fromUser.username);
                            else
                            {
                                postMessage("Yo, @" + mymessages[i].fromUser.username);
                            }
                        }
                        //Mark this message as posted.
                        postedids.Add(mymessages[i]);
                    }
            }
        }


    }
}
