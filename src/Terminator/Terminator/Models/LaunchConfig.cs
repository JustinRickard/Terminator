using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminator.Models
{
    public class Config
    {
        public string BaseDirectory { get; set; }
        public IEnumerable<LaunchConfigGroup> LaunchConfigGroups { get; set; }
    }

    public class LaunchConfig
    {
        public string Name { get; set; }
        public ProgramType ProgramType { get; set; }
        public string Path { get; set; }
        public LaunchConfigGroup Group { get; set; }

    }

    public class LaunchConfigGroup
    {
        public string Name { get; set; }
        public IEnumerable<LaunchConfig> LaunchConfigs { get; set; }
        public Config Config { get; set; }
    }
}
