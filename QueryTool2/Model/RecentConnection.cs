using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.Common;

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
        private string _connectionStringWithPwd;
        [NonSerialized]
        public string ConnectionStringWithPwd { get { return _connectionStringWithPwd; } set { _connectionStringWithPwd = value; } }
        private string _password;
        [NonSerialized]
        public string Password { get { return _password; } set { _password = value; } }
        public string PasswordEncrypted { get { return _password; } set { _password = value; } }
        private string _passwordKey;
        public string PasswordKey { get { return _passwordKey; } set { _passwordKey = value; } }

        private bool _supportsProviderFactory;
        public bool SupportsProviderFactory { get { return _supportsProviderFactory; } set { _supportsProviderFactory = value; } }
        private string _providerInvariantName;
        public string ProviderInvariantName
        {
            get { return _providerInvariantName; }
            set
            {
                _providerInvariantName = value;
                CheckPasswordKey();
            }
        }
        private void CheckPasswordKey()
        {
            if (_providerInvariantName == "")
                _passwordKey = "Password";
            //else if ...
            else
                _passwordKey = "Password";
        }
        public void UpdateConnectionString(DbConnectionStringBuilder csb)
        {
            string pwd = csb[_passwordKey] as string;
            if (!string.IsNullOrEmpty(pwd))
            {
                _password = pwd;
                csb[_passwordKey] = "****";
            }
            _connectionString = csb.ConnectionString;
        }

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
