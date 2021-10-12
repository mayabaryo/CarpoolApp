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
    class CarpoolAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:13939/CarpoolAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://10.100.102.22:13939/CarpoolAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:13939/CarpoolAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:13939/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:13939/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "https://localhost:44319/Images/"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static CarpoolAPIProxy proxy = null;

        public static CarpoolAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
            }

            if (proxy == null)
                proxy = new CarpoolAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }


        private CarpoolAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }

        public async Task<User> LoginAsync(string email,/* string userName,*/ string pass)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    return u;
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

        public async Task<Adult> AdultSignUpAsync(Adult adult)
        {
            //, string email, string userName, string pass, string fName, string lName,
            //DateTime birthDate, string phoneNum, string photo, string city, string neighborhood, string street, string houseNum

            //email ={ email}
            //&userName ={ userName}
            //&pass ={ pass}
            //&fName ={ fName}
            //&lName ={ lName}
            //" +
            //        $"&birthDate={birthDate}&phoneNum={phoneNum}&photo={photo}&city={city}&neighborhood={neighborhood}&street={street}&houseNum={houseNum}
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/AdultSignUp?adult={adult}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Adult a = JsonSerializer.Deserialize<Adult>(content, options);
                    return a;
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

        public async Task<User> KidSignUpAsync(string email, string userName, string pass, string fName, string lName,
            DateTime birthDate, string phoneNum, string photo, string city, string neighborhood, string street, string houseNum)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/KidSignUp?email={email}&userName={userName}&pass={pass}&fName={fName}&lName={lName}" +
                    $"&birthDate={birthDate}&phoneNum={phoneNum}&photo={photo}&city={city}&neighborhood={neighborhood}&street={street}&houseNum={houseNum}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    return u;
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

        public async Task<string> GetStringAsync()
        {
            try
            {
                string s = $"{this.baseUri}/GetString";
                //System.Net.ServicePointManager.SecurityProtocol =
                //    System.Net.SecurityProtocolType.Tls12 |
                //    System.Net.SecurityProtocolType.Tls11 |
                //    System.Net.SecurityProtocolType.Tls;
                HttpResponseMessage response = await this.client.GetAsync(s);
                if (response.IsSuccessStatusCode)
                {
                    //JsonSerializerOptions options = new JsonSerializerOptions
                    //{
                    //    ReferenceHandler = ReferenceHandler.Preserve,
                    //    PropertyNameCaseInsensitive = true
                    //};
                    string content = await response.Content.ReadAsStringAsync();
                    //string eString = JsonSerializer.Deserialize<string>(content, options);
                    return content;
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
