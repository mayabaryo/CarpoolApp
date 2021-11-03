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

namespace CarpoolApp.Services
{
    class AddressAPIProxy
    {
        private const string CLOUD_URL = "https://data.gov.il/api/3/action/datastore_search"; //API url when going on the cloud
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

        public async Task<List<object>> GetCitiesAsync()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?resource_id=d4901968-dad3-4845-a9b0-a57d027f11ab&limit=1500");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<object> objList = JsonSerializer.Deserialize<List<object>>(content, options);

                    return objList;
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

        public async Task<object> GetStreetsAsync(string city)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?resource_id=a7296d1a-f8c9-4b70-96c2-6ebb4352f8e3&limit=01000&q={city}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    object objList = JsonSerializer.Deserialize<object>(content, options);

                    return objList;
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

