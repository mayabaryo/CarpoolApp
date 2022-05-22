using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarpoolApp.Services
{
    public interface ICarpoolService
    {
        Task Connect(int carpoolId);
        //Task Connect(string groupName);
        Task Disconnect(int carpoolId);
        //Task Disconnect(string groupName);
        Task SendKidOnBoard(int carpoolId, int kidId);
        Task SendLocation(int carpoolId, double longitude, double latitude);
        Task SendArriveToDestination(int carpoolId);
        void RegisterToKidOnBoard(Action<int> UpdateKidOnBoard);
        void RegisterToLocation(Action<int, int> UpdateLocation);
        void RegisterToArrive(Action UpdateLocation);
    }
}
