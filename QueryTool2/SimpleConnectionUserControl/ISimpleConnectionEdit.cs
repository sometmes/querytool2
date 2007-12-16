using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace App.SimpleConnectionUserControl
{
    interface ISimpleConnectionEdit
    {
        void EditConnection(DbProviderFactory factory, RecentConnection cninfo);
    }
}
