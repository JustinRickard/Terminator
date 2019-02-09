using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminator.Models
{
    public class LaunchConfig
    {
        public string Name { get; set; }
        public ProgramType ProgramType { get; set; }
        public string CommandText { get; set; }

    }

    public class LaunchConfigGroup
    {
        public string Name { get; set; }
        public IEnumerable<LaunchConfig> LaunchConfigs { get; set; }
    }
}
