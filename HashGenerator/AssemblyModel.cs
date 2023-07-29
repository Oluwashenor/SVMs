using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerator
{
    public class AssemblyConfig
    {
        public string AssemblyName { get; set; }
        public string Hash { get; set; }
    }

    public class AllowedAssembly
    {
        public List<AssemblyConfig> AllowedAssemblies { get; set; }
    }
}
