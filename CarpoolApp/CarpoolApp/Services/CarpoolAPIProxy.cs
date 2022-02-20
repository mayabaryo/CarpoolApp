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
using CarpoolApp.DTO;

namespace CarpoolApp.Services
{
    class CarpoolAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string CLOUD_DATA_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:13939/CarpoolAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://10.100.102.22:13939/CarpoolAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:13939/CarpoolAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:13939/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:13939/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "https://localhost:44319/Images/"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_DATA_URL = "http://10.0.2.2:13939/data/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_DATA_URL = "http://192.168.1.14:13939/data/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_DATA_URL = "https://localhost:44319/data/"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private string baseDataUri;
        private static CarpoolAPIProxy proxy = null;

        public static CarpoolAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            string baseDataUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                        baseDataUri = DEV_ANDROID_EMULATOR_DATA_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                        baseDataUri = DEV_ANDROID_PHYSICAL_DATA_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                    baseDataUri = DEV_WINDOWS_DATA_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
                baseDataUri = CLOUD_DATA_URL;
            }

            if (proxy == null)
                proxy = new CarpoolAPIProxy(baseUri, basePhotosUri, baseDataUri);
            return proxy;
        }

        private CarpoolAPIProxy(string baseUri, string basePhotosUri, string baseDataUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
            this.baseDataUri = baseDataUri;
        }

        public string GetBasePhotoUri() { return this.basePhotosUri; }

        #region LoginAsync
        public async Task<User> LoginAsync(string email, string pass)
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

                    //try
                    //{
                    //    Kid kid = JsonSerializer.Deserialize<Kid>(content, options);
                    //    if (kid.KidsInActivities != null)
                    //        return kid;
                    //}
                    //catch { }
                    //try
                    //{
                    //    Adult adult = JsonSerializer.Deserialize<Adult>(content, options);
                    //    if (adult.Activities != null)
                    //        return adult;
                    //}
                    //catch { }

                    //return null;

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
        #endregion

        #region UpdateUser
        public async Task<User> UpdateUser(User user)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<User>(user, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateUser", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    User ret = JsonSerializer.Deserialize<User>(jsonObject, options);
                    return ret;
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
        #endregion

        #region EmailExistAsync
        public async Task<bool> EmailExistAsync(string email)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/IsEmailExist?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region UserNameExistAsync
        public async Task<bool> UserNameExistAsync(string userName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/IsUserNameExist?userName={userName}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return true;
            }
        }
        #endregion

        #region ActivityExistAsync
        public async Task<bool> ActivityExistAsync(int activityId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/IsActivityExist?activityId={activityId}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region AdultSignUpAsync
        public async Task<Adult> AdultSignUpAsync(Adult adult)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Adult>(adult, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AdultSignUp", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Adult a = JsonSerializer.Deserialize<Adult>(jsonObject, options);
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
        #endregion

        #region AddKidAsync
        public async Task<Kid> AddKidAsync(Kid kid)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Kid>(kid, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddKid", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Kid k = JsonSerializer.Deserialize<Kid>(jsonObject, options);
                    return k;
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
        #endregion

        #region AddAdultAsync
        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Adult>(adult, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddAdult", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Adult a = JsonSerializer.Deserialize<Adult>(jsonObject, options);
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
        #endregion

        #region AddActivityAsync
        public async Task<Activity> AddActivityAsync(Activity activity)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Activity>(activity, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddActivity", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Activity a = JsonSerializer.Deserialize<Activity>(jsonObject, options);
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
        #endregion

        #region AddCarpoolAsync
        public async Task<Carpool> AddCarpoolAsync(Carpool carpool)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Carpool>(carpool, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/AddCarpool", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    Carpool c = JsonSerializer.Deserialize<Carpool>(jsonObject, options);
                    return c;
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
        #endregion

        #region JoinToCarpoolAsync
        public async Task<KidsInCarpool> JoinToCarpoolAsync(KidsInCarpool kidsIn)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<KidsInCarpool>(kidsIn, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/JoinToCarpool", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    KidsInCarpool kic = JsonSerializer.Deserialize<KidsInCarpool>(jsonObject, options);
                    return kic;
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
        #endregion

        #region GetAllKidsAsync
        public async Task<List<Kid>> GetAllKidsAsync(Adult adult)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetAllKids?adult={adult}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Kid> kids = JsonSerializer.Deserialize<List<Kid>>(content, options);
                    return kids;
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
        #endregion

        #region GetAllActivitiesAsync
        public async Task<List<Activity>> GetAllActivitiesAsync(Kid kid)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Kid>(kid, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/GetAllActivities", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    List<Activity> activities = JsonSerializer.Deserialize<List<Activity>>(jsonObject, options);
                    return activities;
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
        #endregion

        #region GetAllCarpoolsAsync
        public async Task<List<Carpool>> GetAllCarpoolsAsync(Kid kid)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<Kid>(kid, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/GetAllCarpools", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    List<Carpool> carpools = JsonSerializer.Deserialize<List<Carpool>>(jsonObject, options);
                    return carpools;
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
        #endregion

        #region JoinToActivityAsync
        public async Task<KidsInActivity> JoinToActivityAsync(KidsInActivity kidsIn)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<KidsInActivity>(kidsIn, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/JoinToActivity", content);
                if (response.IsSuccessStatusCode)
                {
                    jsonObject = await response.Content.ReadAsStringAsync();
                    KidsInActivity k = JsonSerializer.Deserialize<KidsInActivity>(jsonObject, options);
                    return k;
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
        #endregion

        //Upload file to server (only images!)
        #region UploadImage
        public async Task<bool> UploadImage(Models.FileInfo fileInfo, string targetFileName)
        {
            try
            {
                var multipartFormDataContent = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(fileInfo.Name));
                multipartFormDataContent.Add(fileContent, "file", targetFileName);
                HttpResponseMessage response = await client.PostAsync($"{this.baseUri}/UploadImage", multipartFormDataContent);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion


        //************** Streets and Cities JSOn file **********************************
        #region GetCitiesNameList
        private List<string> GetCitiesNameList(List<City> cities)
        {
            List<string> citiesName = new List<string>();

            foreach (City city in cities)
            {
                citiesName.Add(city.name);
            }
            citiesName.Remove(citiesName[0]);

            return citiesName;
        }
        #endregion

        #region GetCitiesAsync
        public async Task<List<string>> GetCitiesAsync()
        {
            ///royts/israel-cities/master/israel-cities.json
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/cities.json");
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
        #endregion

        #region GetStreetsNameList
        private List<string> GetStreetsNameList(List<Street> streets/*, string city*/)
        {
            List<string> streetsName = new List<string>();

            foreach (Street street in streets)
            {
                streetsName.Add(street.street_name);
            }

            return streetsName;
        }
        #endregion

        #region GetStreetsAsync
        public async Task<List<string>> GetStreetsAsync(/*string city*/)
        {
            //?resource_id=d4901968-dad3-4845-a9b0-a57d027f11ab&limit=1500
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/streets.json?666");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
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
        #endregion
    }
}
