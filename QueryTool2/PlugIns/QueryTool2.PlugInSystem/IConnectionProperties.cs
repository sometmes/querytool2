using System;
using System.Data;

namespace QueryTool2.Plugins
{
    public interface IConnectionProperties
    {
        IDbConnection Connection { get; set; }
        string Database { get; }
        string Server { get; }
        string UserName { get; }
    }
}
