using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace QueryTool2.DefaultPlugIn
{
    class ConnectionProperties : QueryTool2.Plugins.IConnectionProperties
    {
        IDbConnection cn = null;
        public System.Data.IDbConnection Connection
        {
            get
            {
                return this.cn;
            }
            set
            {
                this.cn = value;
            }
        }

        public string Database
        {
            get { return this.cn.Database; ; }
        }

        public string Server
        {
            get
            {
                if (this.cn is DbConnection)
                    return (cn as DbConnection).DataSource;
                else
                    return "¿?";
            }
        }

        public string UserName
        {
            get { return "¿?"; }
        }
    }
}
