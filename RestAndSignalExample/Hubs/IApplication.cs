using System;
using System.Threading.Tasks;

namespace RestAndSignalExample.Hubs
{
    public interface IConnectivity
    {
        Task Connect();
        Task Disconnect();
        Task CurrentTime(DateTime dateTime);
    }
}