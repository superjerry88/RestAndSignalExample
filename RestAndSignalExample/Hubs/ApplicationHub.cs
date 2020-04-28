using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RestAndSignalExample.Hubs
{
    public class ApplicationHub:Hub
    {
        private static ApplicationHub _instance;

        public ApplicationHub()
        {
            _instance = this;
        }

        public override Task OnConnectedAsync()
        {
            ManagerHub.ConnectedIds.Add(Context.ConnectionId);
            Clients.Caller.SendAsync("Connected", DateTime.Now);
            Console.WriteLine("1 machine connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ManagerHub.ConnectedIds.Remove(Context.ConnectionId);
            Console.WriteLine("ended connection");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
