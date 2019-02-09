using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminator.Models
{
    public enum ProgramType
    {
        [Description("Powershell")]
        Powershell = 1,

        [Description("Git Bash")]
        GitBash = 2,

        [Description("Command Prompt (cmd)")]
        Cmd = 3,

        [Description("Visual Studio Code")]
        VisualStudioCode = 4
    }
}
