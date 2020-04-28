using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace RestAndSignalExample.Hubs
{
    public class ManagerHub
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
        private static IHubContext<ApplicationHub> Current { get; set; }

        public static void SetContect(IHubContext<ApplicationHub>  context)
        {
            Current = context;
        }

        public static void Send(string method, object obj)
        {
            Current.Clients.All.SendAsync(method, obj);
        }
    }
}