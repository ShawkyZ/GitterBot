using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitterBot.Bots
{
    class CompileCodeBot
    {
        public static void GetCodeCompile(string messageText,string username)
        {
            string[] langandcode = messageText.ToLower().Split(' '); //Split the sent message to get the programming language and the code
            var data = new NameValueCollection();
            try
            {
                string code = messageText.Replace(langandcode[0] + " " + langandcode[1] + " " + langandcode[2] + " " + langandcode[3], "").Remove(0, 1);
                data["code"] = code;
            }
            catch { return; }
            
            #region switch case to Put the programming language name in the form that eval.in accepts
            switch (langandcode[3])
            {
                case "c":
                    data["lang"] = "c/gcc-4.9.1";
                    break;
                case "c++":
                    data["lang"] = "c++/c++11-gcc-4.9.1";
                    break;
                case "coffescript":
                    data["lang"] = "coffeescript/node-0.10.29-coffee-1.7.1";
                    break;
                case "fortran":
                    data["lang"] = "fortran/f95-4.4.3";
                    break;
                case "haskell":
                    data["lang"] = "haskell/hugs98-sep-2006";
                    break;
                case "io":
                    data["lang"] = "io/io-20131204";
                    break;
                case "javascript":
                    data["lang"] = "javascript/node-0.10.29";
                    break;
                case "lua":
                    data["lang"] = "lua/lua-5.2.3";
                    break;
                case "ocaml":
                    data["lang"] = "ocaml/ocaml-4.01.0";
                    break;
                case "php":
                    data["lang"] = "php/php-5.5.14";
                    break;
                case "pascal":
                    data["lang"] = "pascal/fpc-2.6.4";
                    break;
                case "perl":
                    data["lang"] = "perl/perl-5.20.0";
                    break;
                case "python":
                    data["lang"] = "python/cpython-3.4.1";
                    break;
                case "ruby":
                    data["lang"] = "ruby/mri-2.1";
                    break;
                case "slash":
                    data["lang"] = "slash/slash-head";
                    break;
                case "assembly":
                    data["lang"] = "assembly/nasm-2.07";
                    break;
            }
            #endregion
            WebClient wb = new WebClient();
            data["execute"] = "on";
            data["utf8"] = "%CE%BB";
            data["input"] = "";
            wb.Headers.Add("Host", "eval.in");
            wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:33.0) Gecko/20100101 Firefox/33.0");
            wb.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            wb.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            wb.Headers.Add("Referer", "https://eval.in/");
            byte[] response;
            try
            {
                response = wb.UploadValues("https://eval.in", "POST", data);
            }
            catch { return; }
            string htmlraw = System.Text.Encoding.UTF8.GetString(response);
            //TODO:Get the compile result only and remove any additional text.
            string res1 = htmlraw.Substring(htmlraw.IndexOf("<h2>Program Output</h2>"));
            string res2 = res1.Remove(res1.IndexOf("<h2>Fork</h2>"));
            string finalres = Regex.Replace(res2, "<.*?>", string.Empty);
            Messages.postMessage(finalres + "@" + username);
        }
    }
}
