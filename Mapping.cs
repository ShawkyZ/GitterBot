using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterBot
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string url { get; set; }
        public string avatarUrlSmall { get; set; }
        public string avatarUrlMedium { get; set; }
        public string role { get; set; }
    }
    public class Mention
    {
        public string screenName { get; set; }
        public string userId { get; set; }
    }
    public class issue
    {
        public string number { get; set; }
    }
    public class URL
    {
        public string url { get; set; }
    }
    public class Message
    {
        public string id { get; set; }
        public string text { get; set; }
        public string html { get; set; }
        public string sent { get; set; }
        public string editedAt { get; set; }
        public User fromUser { get; set; }
        public bool unread { get; set; }
        public int readBy { get; set; }
        public List<URL> urls { get; set; }
        public List<Mention> mentions { get; set; }
        public List<issue> issues { get; set; }
        public string meta { get; set; }
        public string v { get; set; }
    }
}
