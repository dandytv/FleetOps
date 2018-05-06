using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetOps.Models
{
    public class RealTimeJTable : Hub
    {
        public void SendMessage(string clientName, string message)
        {
            Clients.All.broadcastMessage(clientName, message);
        }

    }
}