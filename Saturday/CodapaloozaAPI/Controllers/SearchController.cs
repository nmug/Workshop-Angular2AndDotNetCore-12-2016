using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Http;

namespace CodapaloozaAPI.Controllers
{
    public class SearchController : ApiController
    {
        //Get
        public List<Models.Search> Get()
        {
            var model = new List<Models.Search>();

            string request = YelpAPI.PerformRequest();

            DataContractJsonSerializer serialize =
                new DataContractJsonSerializer(typeof(Models.Search));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(request)))
            {
                var searchResults = (Models.Search)serialize.ReadObject(ms);
                model.Add(searchResults);
            }

            return model;
        }

    }
}
