using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace App
{
    [Serializable]
    public class RecentConnection
    {
        private DateTime _created;
        public DateTime Created { get { return _created; } set { _created = value; } }
        public DateTime LastUsed;
        private string _connectionString;
        public string ConnectionString { get { return _connectionString; } set { _connectionString = value; } }
        public string Password;
    }

    [Serializable]
    public class RecentConnectionList : List<RecentConnection>
    {
    }
}
