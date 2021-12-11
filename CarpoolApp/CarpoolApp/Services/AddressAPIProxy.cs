using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CarpoolApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using System.Reflection;
using CarpoolApp.DTO;

namespace CarpoolApp.Services
{
    class AddressAPIProxy
    {
        private const string CLOUD_URL = "https://raw.githubusercontent.com"; //API url when going on the cloud
        //https://data.gov.il/api/3/action/datastore_search
        private const string CLOUD_PHOTOS_URL = "TBD";

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static AddressAPIProxy proxy = null;

        public static AddressAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;

            baseUri = CLOUD_URL;
            basePhotosUri = CLOUD_PHOTOS_URL;

            //if (App.IsDevEnv)
            //{
            //    if (Device.RuntimePlatform == Device.Android)
            //    {
            //        if (DeviceInfo.DeviceType == DeviceType.Virtual)
            //        {
            //            baseUri = DEV_ANDROID_EMULATOR_URL;
            //            basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
            //        }
            //        else
            //        {
            //            baseUri = DEV_ANDROID_PHYSICAL_URL;
            //            basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
            //        }
            //    }
            //    else
            //    {
            //        baseUri = DEV_WINDOWS_URL;
            //        basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
            //    }
            //}
            //else
            //{
            //    baseUri = CLOUD_URL;
            //    basePhotosUri = CLOUD_PHOTOS_URL;
            //}

            if (proxy == null)
                proxy = new AddressAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }


        private AddressAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }

        public List<string> GetCitiesNameList(List<City> cities)
        {
            List<string> citiesName = new List<string>();

            foreach(City city in cities)
            {
                citiesName.Add(city.name);
            }
            citiesName.Remove(citiesName[0]);

            return citiesName;    
        }
        public async Task<List<string>> GetCitiesAsync()
        {
            ///royts/israel-cities/master/israel-cities.json
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/royts/israel-cities/master/israel-cities.json");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<City> cities = JsonSerializer.Deserialize<List<City>>(content, options);

                    return GetCitiesNameList(cities);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<string> GetStreetsNameList(List<Street> streets/*, string city*/)
        {
            List<string> streetsName = new List<string>();

            foreach (Street street in streets)
            {
                streetsName.Add(street.street_name);
            }

            return streetsName;
        }
        public async Task<List<string>> GetStreetsAsync(/*string city*/)
        {
            //?resource_id=d4901968-dad3-4845-a9b0-a57d027f11ab&limit=1500
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GabMic/israeli-cities-and-streets-list/master/israeli_street_and_cities_names.json");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Street> streets = JsonSerializer.Deserialize<List<Street>>(content, options);

                    return GetStreetsNameList(streets/*, city*/);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}

