using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace App
{
    public class EnabledState : System.Collections.Generic.Dictionary<Control, bool>
    {
    }

    class WinForms
    {

        public static EnabledState DisableBut(Control ctrl)
        {
            EnabledState enabledDisabled = new EnabledState();
            foreach (Control c2 in ctrl.Parent.Controls)
            {
                enabledDisabled.Add(c2, c2.Enabled);
                
                if (c2 == ctrl)
                    c2.Enabled = true;
                else
                    c2.Enabled = false;
            }
            return enabledDisabled;
        }

        public static void DisableRestore(EnabledState state)
        {
            foreach (Control c2 in state.Keys)
                c2.Enabled = state[c2];
        }
    }
}
