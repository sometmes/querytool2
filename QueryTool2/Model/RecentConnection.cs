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
    }

    [Serializable]
    public class ConnectionInfo2:ConnectionInfo
    {
    }


    [Serializable]
    public class ConnectionInfoList : List<ConnectionInfo>
    {
    }
}
