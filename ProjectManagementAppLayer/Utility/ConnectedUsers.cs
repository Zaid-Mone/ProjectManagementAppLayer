using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Utility
{
    public class ConnectedUsers
    {
        public static ConcurrentDictionary<string, string> _connectedClients = new ConcurrentDictionary<string, string>();
    }
}
