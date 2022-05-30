using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarpoolApp.Services
{
    public class CarpoolService : ICarpoolService
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:13939/carpool"; //carpool url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:13939/carpool"; //carpool url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:13939/carpool"; //API url when using windoes on development

        private readonly HubConnection hubConnection;
        public CarpoolService()
        {
            string chatUrl = GetChatUrl();
            hubConnection = new HubConnectionBuilder().WithUrl(chatUrl).Build();

        }

        private string GetChatUrl()
        {
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        return DEV_ANDROID_EMULATOR_URL;
                    }
                    else
                    {
                        return DEV_ANDROID_PHYSICAL_URL;
                    }
                }
                else
                {
                    return DEV_WINDOWS_URL;
                }
            }
            else
            {
                return CLOUD_URL;
            }
        }

        //Connect gets a list of groups the user belongs to!
        public async Task Connect(int carpoolId)
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("OnConnect", carpoolId);
        }

        
        public async Task Disconnect(int carpoolId)
        {
            await hubConnection.InvokeAsync("OnDisconnect", carpoolId);
            await hubConnection.StopAsync();

        }

       
        public async Task SendKidOnBoard(int carpoolId, int kidId)
        {

            await hubConnection.InvokeAsync("SendKidOnBoard", carpoolId, kidId);

        }

        public async Task SendArriveToDestination(int carpoolId)
        {

            await hubConnection.InvokeAsync("SendArriveToDestination", carpoolId);

        }

        public async Task SendLocation(int carpoolId, double longitude, double latitude)
        {

            await hubConnection.InvokeAsync("SendLocation", carpoolId, longitude, latitude);

        }

        public async Task StartDrive(int carpoolId)
        {

            await hubConnection.InvokeAsync("StartDrive", carpoolId);

        }



        public void RegisterToKidOnBoard(Action<int> UpdateKidOnBoard)
        {
            hubConnection.On("UpdateKidOnBoard", UpdateKidOnBoard);
        }
        public void RegisterToLocation(Action<int, int> UpdateLocation)
        {
            hubConnection.On("UpdateDriverLocation", UpdateLocation);
        }
        public void RegisterToArrive(Action UpdateArriveToDestination)
        {
            hubConnection.On("UpdateArriveToDestination", UpdateArriveToDestination);
        }
        public void RegisterToStartDrive(Action UpdateStartDrive)
        {
            hubConnection.On("UpdateStartDrive", UpdateStartDrive);
        }
    }
}
