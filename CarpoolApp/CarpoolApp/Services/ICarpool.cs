﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarpoolApp.Services
{
    public interface ICarpoolService
    {
        Task Connect(string[] groupNames);
        //Task Connect(string groupName);
        Task Disconnect(string[] groupNames);
        //Task Disconnect(string groupName);
        Task SendMessage(string userId, string message);
        Task SendMessageToGroup(string userId, string message, string groupName);
        void RegisterToReceiveMessage(Action<string, string> GetMessageAndUser);
        void RegisterToReceiveMessageFromGroup(Action<string, string, string> GetMessageAndUserFromGroup);
    }
}
