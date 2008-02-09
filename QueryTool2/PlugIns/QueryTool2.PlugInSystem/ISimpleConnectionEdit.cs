using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace QueryTool2.Plugins
{
    interface ISimpleConnectionEdit
    {
        void EditConnection(DbProviderFactory factory, IConnectionInfo cninfo);
    }
}
