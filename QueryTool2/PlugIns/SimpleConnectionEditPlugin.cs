using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class SimpleConnectionEditPlugin
    {
        static System.Collections.Generic.Dictionary<string, string> _editors = new Dictionary<string, string>();

        public static void RegisterPlugin(string invariantName, string assemblyQualifiedTypeName)
        {
            _editors.Add(invariantName, assemblyQualifiedTypeName);
        }

        public static ISimpleConnectionEdit CreateInstance(string invariantName)
        {
            string assemblyQualifiedTypeName;
            if (_editors.ContainsKey(invariantName))
                assemblyQualifiedTypeName = _editors[invariantName];
            else
                assemblyQualifiedTypeName = "App.SimpleConnectionUserControl.NoEditor";
            Type t = Type.GetType(assemblyQualifiedTypeName);
            return (Activator.CreateInstance(t) as ISimpleConnectionEdit);
        }
    }
}
