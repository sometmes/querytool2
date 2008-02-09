using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace QueryTool2.MsSqlPlugIn
{
    class ConnectionProperties : QueryTool2.Plugins.IConnectionProperties
    {
        SqlConnection cn = null;

        public System.Data.IDbConnection Connection
        {
            get
            {
                return this.cn;
            }
            set
            {
                this.cn = value as SqlConnection;
            }
        }

        public string Server
        {
            get
            {
                return this.cn.DataSource;
            }
        }

        public string Database
        {
            get
            {
                return this.cn.Database;
            }
        }

        string userName = null;
        public string UserName
        {
            get
            {
                if (userName == null)
                {
                    SqlCommand cmd = new SqlCommand("select user_name()",this.cn);
                    userName = cmd.ExecuteScalar() as string;
                }
                return userName;
            }
        }
    }
}
