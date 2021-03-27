using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSimulator.Service.SignalR
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, string> _connections = new Dictionary<T, string>();
        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string user)
        {
            lock (_connections)
            {
                string connection;
                if (!_connections.TryGetValue(key, out connection))
                {
                    _connections.Add(key, user);
                }
            }
        }

        public IEnumerable<string> GetConnections(string user)
        {
            return _connections.Where(x => x.Value == user).Select(y => y.Key.ToString()).AsEnumerable();
        }

        public dynamic GetAllConnections()
        {
            return _connections.AsEnumerable().ToList();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                string connection;
                if (!_connections.TryGetValue(key, out connection))
                {
                    return;
                }
                _connections.Remove(key);
            }
        }
    }
}
