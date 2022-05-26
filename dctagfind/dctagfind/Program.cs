using System;
using System.IO;
using System.Net;
using System.Text;

namespace dctagfind
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                string discordtoken = ""; // discord token here
                string username = ""; // username of guy who's tag you want to find
                int tag = 0001; // don't touch this
                var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me/relationships");
                var postData = "{\"username\":" +
                     "\"" + username + "\"" + "," +
                     "\"discriminator\":" +
                     "\"" + tag + "\"" + "}";

                var data = Encoding.UTF8.GetBytes(postData);
                request.Headers.Add("Authorization", discordtoken);
                request.Method = "POST";
                request.Accept = "application/json";
                request.ContentType = "application/json";



                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse httpRes = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(httpRes.GetResponseStream()).ReadToEnd();
                Console.WriteLine(responseString);
                if (tag == 9999)
                {
                    Console.WriteLine("Couldn't find");
                }
                if (httpRes.StatusCode != HttpStatusCode.NoContent)
                {
                    Console.WriteLine("Couldn't find" + username + tag);
                }
                if (httpRes.StatusCode == HttpStatusCode.NoContent)
                {
                    Console.WriteLine("Found" + username + tag);
                }
                tag++;
            }
        }
    }
}