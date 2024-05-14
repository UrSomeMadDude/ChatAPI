using ChatAPI.Models;
using System.Collections.Concurrent;

namespace ChatAPI.DataService
{
    public class SharedDB
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new ConcurrentDictionary<string, UserConnection>();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
}
