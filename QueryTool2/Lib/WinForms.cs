using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace App
{
    public class EnabledState : System.Collections.Generic.Dictionary<object, bool[]>
    {
    }

    class WinForms
    {
        public static void Disable(Control ctrl, EnabledState state)
        {
            state.Add(ctrl, new bool[] { ctrl.Enabled, false });
            ctrl.Enabled = false;
        }

        public static void Disable(ToolStripItem ctrl, EnabledState state)
        {
            state.Add(ctrl, new bool[] { ctrl.Enabled, false });
            ctrl.Enabled = false;
        }

        public static void Enable(Control ctrl, EnabledState state)
        {
            state.Add(ctrl, new bool[] { ctrl.Enabled, true });
            ctrl.Enabled = true;
        }

        public static void Enable(ToolStripItem ctrl, EnabledState state)
        {
            state.Add(ctrl, new bool[] { ctrl.Enabled, true });
            ctrl.Enabled = true;
        }

        public static EnabledState DisableBut(Control ctrl)
        {
            EnabledState enabledDisabled = new EnabledState();
            foreach (Control c2 in ctrl.Parent.Controls)
            {
                enabledDisabled.Add(c2, new bool[] { c2.Enabled, false });
                
                if (c2 == ctrl)
                    c2.Enabled = true;
                else
                    c2.Enabled = false;
            }
            return enabledDisabled;
        }

        public static void DisableRestore(EnabledState state)
        {
            foreach (object o in state.Keys)
            {
                if (o is Control)
                    (o as Control).Enabled = state[o][0];
                else if (o is ToolStripItem)
                    (o as ToolStripItem).Enabled = state[o][0];
                else
                    throw new ApplicationException("Unsupported control type");
            }
        }

        public static void DisableAgain(EnabledState state)
        {
            foreach (object o in state.Keys)
            {
                if (o is Control)
                    (o as Control).Enabled = state[o][1];
                else if (o is ToolStripItem)
                    (o as ToolStripItem).Enabled = state[o][1];
                else
                    throw new ApplicationException("Unsupported control type");
            }
        }
    }
}
