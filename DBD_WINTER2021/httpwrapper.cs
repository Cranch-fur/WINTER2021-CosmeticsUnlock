using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net;

namespace DBD_WINTER2021
{
    public static class httpwrapper
    {
        public static string HTTP_Subdomain, HTTP_Platform, HTTP_PlayerRole = "survivor", HTTP_ActionKey = "survivorSnowmanDestroyed";
        private static string RandomMatch()
        {
            var random = new System.Random();
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" + new string(Enumerable.Repeat(chars, 4)
                                                                       .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" + new string(Enumerable.Repeat(chars, 4)
                                                                                                                              .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" + new string(Enumerable.Repeat(chars, 4)
                                                                                                                                                                                     .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" + new string(Enumerable.Repeat(chars, 12)
                                                                                                                                                                                                                                            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static int WEBREQUEST_POST(string bhvrsession, int progression)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://{HTTP_Subdomain}.live.bhvrdbd.com/api/v1/extensions/objectives/updateObjectiveProgression");
            request.Method = "POST";
            request.Headers.Add($"x-kraken-client-platform: {HTTP_Platform}");
            request.Headers.Add($"x-kraken-client-provider: {HTTP_Platform}");
            request.Headers.Add("x-kraken-client-os: 10.0.22000.1.768.64bit");
            request.Headers.Add($"x-kraken-client-version: 5.4.1");
            request.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-506030 Windows/10.0.19044.1.256.64bit";
            request.Headers.Add($"Cookie: bhvrSession={bhvrsession}");
            request.ContentType = "application/json";


            JObject Progression = 
                new JObject(
                    new JProperty("data",
                        new JObject(
                            new JProperty("eventId", "Winter2021"),
                            new JProperty("inGameProgress",
                                new JArray(
                                    new JObject(
                                        new JProperty("amount", 1),
                                        new JProperty("key", HTTP_ActionKey)))),
                            new JProperty("matchId", RandomMatch()),
                            new JProperty("objectiveId", "Snowmen"),
                            new JProperty("objectiveVersion", progression),
                            new JProperty("platformVersion", HTTP_Platform),
                            new JProperty("playerRole", HTTP_PlayerRole))));

#if DEBUG
            System.Console.WriteLine(Progression.ToString());
#endif

            byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Progression));
            request.ContentLength = data.Length;
            using (Stream requestBody = request.GetRequestStream())
                requestBody.Write(data, 0, data.Length);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    if (reader.ReadToEnd().Contains("404"))
                        return 404;
                    else return 200;
                }
            }
            catch (WebException exception) {
                return 0;
            }
        }
        public static int WEBREQUEST_GET(string bhvrsession)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://{HTTP_Subdomain}.live.bhvrdbd.com/api/v1/wallet/currencies");
            request.Method = "GET";
            request.Headers.Add($"x-kraken-client-platform: {HTTP_Platform}");
            request.Headers.Add($"x-kraken-client-provider: {HTTP_Platform}");
            request.Headers.Add("x-kraken-client-os: 10.0.22000.1.768.64bit");
            request.Headers.Add($"x-kraken-client-version: 5.4.1");
            request.UserAgent = "DeadByDaylight/++DeadByDaylight+Live-CL-506030 Windows/10.0.19044.1.256.64bit";
            request.Headers.Add($"Cookie: bhvrSession={bhvrsession}");


            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return (int)response.StatusCode;
            }
            catch (WebException exception) {
                return System.Convert.ToInt32(((HttpWebResponse)exception.Response).StatusCode);
            }
        }
    }
}
