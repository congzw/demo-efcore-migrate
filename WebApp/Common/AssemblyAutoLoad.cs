using System.Reflection;
using System;
using System.IO;
using System.Collections.Generic;

namespace NbSites.Web.Common
{
    public class AssemblyAutoLoad
    {
        public static AssemblyAutoLoad Instance = new AssemblyAutoLoad();

        public AssemblyAutoLoad()
        {
            SearchLocations.Add(AppDomain.CurrentDomain.BaseDirectory);
            SearchLocations.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugin"));
        }

        public List<string> SearchLocations { get; set; } = new List<string>();

        public Assembly AssemblyResolve(AssemblyName assemblyName)
        {
            var theSimpleName = assemblyName.Name;
            LoadAssemblies.TryGetValue(theSimpleName, out var result);
            if (result != null)
            {
                return result;
            }

            foreach (var location in SearchLocations)
            {
                var assemblyPath = Path.Combine(location, $"{theSimpleName}.dll");
                if (File.Exists(assemblyPath))
                {
                    var assm = Assembly.LoadFile(assemblyPath);
                    LoadAssemblies[theSimpleName] = assm;
                    return assm;
                }
            }
            return null;
        }

        public IDictionary<string, Assembly> LoadAssemblies { get; set; } = new Dictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);
    }
}