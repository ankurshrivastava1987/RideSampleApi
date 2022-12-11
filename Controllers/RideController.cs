using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using RideSampleAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RideSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        [HttpGet("GetCandidateInfo")]
        public string GetCandidateInfo()
        {
            CandicateInfo candicateInfo = new CandicateInfo();
            candicateInfo.name = "test";
            candicateInfo.phone = "test";
            return JsonConvert.SerializeObject(candicateInfo);
        }

        [HttpGet("GetLocationInfoByIP/{IP}")] 
        public string GetLocationInfoByIP(string IP)
        {
            IPLocationDetails location = new IPLocationDetails();
            HttpClient client = new HttpClient();
            string accessKey = "db23fbcca157eb0d6797f653204a3e5a";
            string requestUrl = "http://api.ipstack.com/" + IP + "?access_key=" + accessKey;
            string locationJsonResult = client.GetStringAsync(requestUrl).Result;

            var jsonObject = JObject.Parse(locationJsonResult);
            location.city = jsonObject["city"].ToString();
            location.latitude = jsonObject["latitude"].ToString();
            location.longitude = jsonObject["longitude"].ToString();
            location.geoNameId = jsonObject["location"]["geoname_id"].ToString();
            location.capital = jsonObject["location"]["capital"].ToString();
            location.callingCode = jsonObject["location"]["calling_code"].ToString();
            return JsonConvert.SerializeObject(location);

        }


        [HttpGet("GetQuotes/{NumberOfPassengers}")] 
        public string GetQuotes(int NumberOfPassengers)
        {
            Quotes agentQuotes = new Quotes();
            HttpClient client = new HttpClient();
            string QuotesJson =  client.GetStringAsync("https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest").Result.ToString();
            agentQuotes  = JsonConvert.DeserializeObject<Quotes>(QuotesJson);
            List<ListingsItem> QualifiedListing = agentQuotes.listings.Where(x => x.vehicleType.maxPassengers >= NumberOfPassengers).ToList();
            foreach (ListingsItem item in QualifiedListing)
            {
                item.totalPrice = NumberOfPassengers * item.pricePerPassenger;
            }
            QualifiedListing = QualifiedListing.OrderBy(x => x.totalPrice).ToList();
            agentQuotes.listings = QualifiedListing;
            return JsonConvert.SerializeObject(agentQuotes);
        }
    }
}
