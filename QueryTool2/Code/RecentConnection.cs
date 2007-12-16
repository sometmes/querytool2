using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace App
{
    [Serializable]
    public class RecentConnection
    {
        public DateTime Created;
        public DateTime LastUsed;
        public string ConnectionString;
        public string Password;
    }

    [Serializable]
    public class RecentConnectionList : List<RecentConnection>
    {
    }
}
