using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace App
{
    [XmlType]
    public class ConnectionInfo
    {
        public DateTime Created { get { return _created; } set { _created = value; } }
        private DateTime _created;

        public DateTime LastUsed { get { return _lastUsed; } set { _lastUsed = value; } }
        private DateTime _lastUsed;

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                _connectionStringWithPwd = null;
            }
        }
        private string _connectionString;

        [XmlIgnore]
        public string ConnectionStringWithPwd
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionStringWithPwd = "";
                if (_connectionStringWithPwd == null)
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory(_providerInvariantName);
                    DbConnectionStringBuilder csb = factory.CreateConnectionStringBuilder();
                    csb.ConnectionString = _connectionString;
                    csb[_passwordKey] = Password;
                    _connectionStringWithPwd = csb.ConnectionString;
                }
                return _connectionStringWithPwd;
            }
            set { _connectionStringWithPwd = value; }
        }
        private string _connectionStringWithPwd;

        [XmlIgnore]
        public string Password { get { return _password; } set { _password = value; } }
        public string PasswordEncrypted
        {
            get
            {
                return Lib.DPAPI.Encrypt(_password);
            }
            set
            {
                _password = Lib.DPAPI.Decrypt(value);
            }
        }
        private string _password;

        public string PasswordKey { get { return _passwordKey; } set { _passwordKey = value; } }
        private string _passwordKey;

        public bool SupportsProviderFactory { get { return _supportsProviderFactory; } set { _supportsProviderFactory = value; } }
        private bool _supportsProviderFactory;

        public string ProviderInvariantName
        {
            get { return _providerInvariantName; }
            set
            {
                _providerInvariantName = value;
                CheckPasswordKey();
            }
        }
        private string _providerInvariantName;

        private void CheckPasswordKey()
        {
            if (_providerInvariantName == "System.Data.SqlClient")
                _passwordKey = "Password";
            //else if ...
            else
                _passwordKey = "Password";
        }

        public void UpdateConnectionString(DbConnectionStringBuilder csb)
        {
            string pwd = csb[_passwordKey] as string;
            if (!string.IsNullOrEmpty(pwd) && pwd != "****")
                _password = pwd;

            csb[_passwordKey] = _password;
            _connectionStringWithPwd = csb.ConnectionString;
            csb[_passwordKey] = null;
            _connectionString = csb.ConnectionString;
        }

        public string ProviderName { get { return _providerName; } set { _providerName = value; } }
        private string _providerName;

        public ConnectionInfo()
        {
            _created = DateTime.Now;
        }

        public ConnectionInfo Copy()
        {
            return this.MemberwiseClone() as ConnectionInfo;
        }
    }

    [XmlType]
    public class ConnectionInfoList
    {
        public ConnectionInfoList()
        {
        }

        public ConnectionInfoList(string providerInvariantName)
        {
            _providerInvariantName = providerInvariantName;
        }

        public string ProviderInvariantName
        {
            get { return _providerInvariantName; }
            set { _providerInvariantName = value; }
        }

        private string _providerInvariantName;


        private List<ConnectionInfo> _connections = new List<ConnectionInfo>();

        public List<ConnectionInfo> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }
    }

    [XmlType]
    public class ConnectionInfoMap 
        : System.Collections.ObjectModel.KeyedCollection<string, ConnectionInfoList>
    {
        protected override string GetKeyForItem(ConnectionInfoList item)
        {
            return item.ProviderInvariantName;
        }

        public void RegisterConnection(ConnectionInfo ci)
        {
            ConnectionInfoList cl;
            if (this.Contains(ci.ProviderInvariantName))
                cl = this[ci.ProviderInvariantName];
            else
            {
                cl = new ConnectionInfoList();
                cl.ProviderInvariantName = ci.ProviderInvariantName;
                this.Add(cl);
            }

            foreach (ConnectionInfo ci2 in cl.Connections)
            {
                if (ci2.ConnectionString == ci.ConnectionString)
                {
                    cl.Connections.Remove(ci2);
                    break;
                }
            }
            cl.Connections.Insert(0, ci);
        }
    }
}
