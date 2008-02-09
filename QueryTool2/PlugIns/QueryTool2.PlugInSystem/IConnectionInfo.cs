using System;
namespace QueryTool2.Plugins
{
    public interface IConnectionInfo
    {
        string ConnectionString { get; set; }
        string ConnectionStringWithPwd { get; set; }
        DateTime Created { get; set; }
        DateTime LastUsed { get; set; }
        string Password { get; set; }
        string PasswordEncrypted { get; set; }
        string PasswordKey { get; set; }
        string ProviderInvariantName { get; set; }
        string ProviderName { get; set; }
        bool SupportsProviderFactory { get; set; }
        void UpdateConnectionString(System.Data.Common.DbConnectionStringBuilder csb);
    }
}
