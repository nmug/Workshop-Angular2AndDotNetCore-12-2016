Creae WebAPI Project
Navigate to Yelp API Search https://www.yelp.com/developers/documentation/v2/search_api
Copy JSON from bottom of the screen
Navigate to JSONtoCSharp http://json2csharp.com/
Create a new model Search
Paste the result
Remove teh root object
Add the DataContract and the Data member
build
Add a blank controller Search
Add the default Get
public List<Models.Search> Get()
{
	var model = new List<Models.Search>();



	return model;
}
Add the new YelpAPI Class and method
string request = YelpAPI.PerformRequest();
Switch classes
Add System.Configuration
https://www.yelp.com/developers
https://www.yelp.com/developers/v2
https://www.yelp.com/developers/v2/manage_api_keys
Add values to webconfig
	<add key="ConsumerKey" value="HR6C8CTZTko8YCHZphG_zA"/>
    <add key="ConsumerSecret" value="8-pIHkVyo50IsqoCL2jNU3QeDn8"/>
    <add key ="Token" value="FxCmvjybdJYImrqUmOiYr0DnseSlkXUw"/>
    <add key ="TokenSecret" value="lc6ZZSdMmnsnE91A4Np8bqqw8iA"/>
    <add key ="BaseURL" value="https://api.yelp.com/v2"/> 

	private static string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private static string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static string Token = ConfigurationManager.AppSettings["Token"];
        private static string TokenSecret = ConfigurationManager.AppSettings["TokenSecret"];
        private static string BaseURL = ConfigurationManager.AppSettings["BaseURL"];
		
add to PerformRequest
//create the default query
 var queryString = System.Web.HttpUtility.ParseQueryString(String.Empty);
            queryString["location"] = "87109";
            queryString["term"] = "brewery";
            queryString["limit"] = "10";
			
add nuget package SimpleOAuth.Net
add using SimpleOAuth;

//create the REST web request
var uri = new  UriBuilder(BaseURL + "/search");
uri.Query = queryString.ToString();

var request = WebRequest.Create(uri.ToString());
request.Method = "GET";

//sign the request Yelps Authentication OAuth tokens
request.SignRequest(
	new Tokens
	{
		ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    AccessToken = Token,
                    AccessTokenSecret = TokenSecret,
	}).WithEncryption(EncryptionMethod.HMACSHA1).InHeader();

HttpWebResponse response = (HttpWebResponse)request.GetResponse();

using System.IO;
using System.Text;
//convert to string an return to controller
var stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

return stream.ReadToEnd();

back to controller
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

String request = YelpAPI.PerformRequest();

//Convert Response to model
DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Models.Search));
using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(request)))
{
	var searchResults = (Models.Search)serializer.ReadObject(ms);

	model.Add(searchResults);
}

 public String Get()
{
	var model = new List<Models.Search>();

	String request = YelpAPI.PerformRequest();

	return request;
}
Add reference to system.devices
add using system.devices.location
modify code

 double latitude = 0;
            double longitude = 0;
            string latLong = string.Empty;
            int counter = 0;
            do
            {
                GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
                watcher.TryStart(false, TimeSpan.FromMilliseconds(10000));
                latitude = System.Math.Round(watcher.Position.Location.Latitude, 5);
                longitude = System.Math.Round(watcher.Position.Location.Longitude, 5);
                latLong = string.Format("{0},{1}", latitude, longitude);
                counter++;
                if (counter == 10) break;
            } while (latitude.ToString() == "NaN");



            //create the default query
            var queryString = System.Web.HttpUtility.ParseQueryString(String.Empty);
            if (latitude.ToString() == "NaN")
            {
                queryString["location"] = "87109";
            }
            else
            {
                queryString["ll"] = latLong;
            }
Create DTO
Add new class in models Searchdto
 public class SearchDTO
    {
		public string id { get; set; } 
        public string name { get; set; }
        public string display_phone { get; set; }
        public string image_url { get; set; }
        public double rating { get; set; }
        public string url { get; set; }
        public List<string> address { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string state_code { get; set; }
    }
change the controller
public List<Models.SearchDTO> Get()
        {
            var model = new List<Models.Search>();

            String request = YelpAPI.PerformRequest();

            //Convert Response to model
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Models.Search));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(request)))
            {
                var searchResults = (Models.Search)serializer.ReadObject(ms);

                model.Add(searchResults);
            }

            var search = from s in model
                         from b in s.businesses
                         select new Models.SearchDTO()
                         {
                             id = b.id,
                             name = b.name,
                             display_phone = b.display_phone,
                             image_url = b.image_url,
                             rating = b.rating,
                             url = b.url,
                             address = b.location.address,
                             city = b.location.city,
                             postal_code = b.location.postal_code,
                             state_code = b.location.state_code
                        };

            return search.ToList();
        }