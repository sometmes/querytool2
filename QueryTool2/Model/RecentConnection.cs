using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace App
{
    [Serializable]
    public class ConnectionInfo
    {
        private DateTime _created;
        public DateTime Created { get { return _created; } set { _created = value; } }
        public DateTime LastUsed;
        private string _connectionString;
        public string ConnectionString { get { return _connectionString; } set { _connectionString = value; } }
        public string Password;

        private bool _supportsProviderFactory;
        public bool SupportsProviderFactory { get { return _supportsProviderFactory; } set { _supportsProviderFactory = value; } }
        string providerInvariantName;
        public string ProviderInvariantName { get { return providerInvariantName; } set { providerInvariantName = value; } }
        string providerName;
        public string ProviderName { get { return providerName; } set { providerName = value; } }

        public ConnectionInfo()
        {
            _created = DateTime.Now;
        }
    }

    [Serializable]
    public class ConnectionInfoMap : Dictionary<string,ConnectionInfo>
    {
    }
}
