using System;
using System.Configuration;
using SimpleOAuth;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace CodapaloozaAPI.Controllers
{
    internal class YelpAPI
    {
        private static string consumerKey =
            ConfigurationManager.AppSettings["ConsumerKey"];
        private static string consumerSecret =
            ConfigurationManager.AppSettings["ConsumerSecret"];
        private static string token =
            ConfigurationManager.AppSettings["Token"];
        private static string tokenSecret =
            ConfigurationManager.AppSettings["TokenSecret"];
        private static string baseUrl =
             ConfigurationManager.AppSettings["BaseURL"];

        internal static string PerformRequest()
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(String.Empty);
            queryString["location"] = "87109";
            queryString["term"] = "brewery";
            queryString["limit"] = "10";

            //create the web request
            var uri = new UriBuilder(baseUrl + "/search");
            uri.Query = queryString.ToString();

            var request = WebRequest.Create(uri.ToString());
            request.Method = "GET";

            request.SignRequest(
                new Tokens
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    AccessToken = token,
                    AccessTokenSecret = tokenSecret
                }).WithEncryption(EncryptionMethod.HMACSHA1).InHeader();

            HttpWebResponse response =
                (HttpWebResponse)request.GetResponse();

            var stream = new StreamReader(response.GetResponseStream(),
                Encoding.UTF8);

            

                return stream.ReadToEnd();
        }


    }

}
