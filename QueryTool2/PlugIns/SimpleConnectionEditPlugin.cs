using System;
using System.Collections.Generic;
using System.Text;
using QueryTool2.Plugins;

namespace App
{
    class SimpleConnectionEditorPlugin
    {
        static System.Collections.Generic.Dictionary<string, string> _pluginList = new Dictionary<string, string>();

        public static void RegisterPlugin(string invariantName, string assemblyQualifiedTypeName)
        {
            _pluginList.Add(invariantName, assemblyQualifiedTypeName);
        }

        public static ISimpleConnectionEdit CreateInstance(string invariantName)
        {
            string assemblyQualifiedTypeName;
            if (_pluginList.ContainsKey(invariantName))
                assemblyQualifiedTypeName = _pluginList[invariantName];
            else
                assemblyQualifiedTypeName = "QueryTool2.DefaultPlugIn.SimpleConnectionEditor";
            Type t = Type.GetType(assemblyQualifiedTypeName);
            return (Activator.CreateInstance(t) as ISimpleConnectionEdit);
        }
    }

    class ConnectionPropertiesPlugin
    {
        static System.Collections.Generic.Dictionary<string, string> _pluginList = new Dictionary<string, string>();

        public static void RegisterPlugin(string invariantName, string assemblyQualifiedTypeName)
        {
            _pluginList.Add(invariantName, assemblyQualifiedTypeName);
        }

        public static IConnectionProperties CreateInstance(string invariantName)
        {
            string assemblyQualifiedTypeName;
            if (_pluginList.ContainsKey(invariantName))
                assemblyQualifiedTypeName = _pluginList[invariantName];
            else
                assemblyQualifiedTypeName = "QueryTool2.DefaultPlugIn.ConnectionProperties";
            Type t = Type.GetType(assemblyQualifiedTypeName);
            return (Activator.CreateInstance(t) as IConnectionProperties);
        }
    }
}
