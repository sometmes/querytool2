using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace App
{
    interface ISimpleConnectionEdit
    {
        void EditConnection(DbProviderFactory factory, ConnectionInfo cninfo);
    }
}
