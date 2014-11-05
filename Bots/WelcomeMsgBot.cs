using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GitterBot.Bots
{
    class WelcomeMsgBot
    {
        public static void WelcomeUsers()
        {
            List<User> oldusers = new List<User>();
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                List<User> myusers;
                using (var wb = new WebClient())
                {
                    //TODO:Send HTTP Get Request To Get The Current Users.
                    wb.Headers.Add("Authorization", Authentication.token_type + " " + Authentication.access_token); //This is the Access Token I Got.
                    wb.Headers.Add("Host", "api.gitter.im");
                    var response = wb.DownloadString("https://api.gitter.im/v1/rooms/54577284db8155e6700d0a73/users");
                    //TODO:Deserialize the Json Response To List Of Users.
                    DataContractJsonSerializer contract = new DataContractJsonSerializer(typeof(List<User>));
                    MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response));
                    myusers = (List<User>)contract.ReadObject(stream);
                }
                bool newUser = false;
                //Check There are any new users with this O(n^2) horrible algorithm.
                for (int i = 0; i < myusers.Count; i++)
                {
                    newUser = false;
                    for (int j = 0; j < oldusers.Count; j++)
                    {
                        if (myusers[i].id != oldusers[j].id)
                        {
                            newUser = true;
                        }
                        else
                        {
                            newUser = false;
                            break;
                        }
                    }
                    if (newUser)
                    {
                        //If There is a new user post a welcome Message
                       Messages.postMessage("Welcome @" + myusers[i].username + "\n `You can ask me to say a quote, define a word or compile some code. Just mention me`");
                    }
                }
                oldusers = myusers;

            }
        }
    }
}
