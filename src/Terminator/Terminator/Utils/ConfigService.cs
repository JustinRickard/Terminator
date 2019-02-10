using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminator.Models;

namespace Terminator.Utils
{
    public class ConfigService
    {
        public ConfigService()
        {
        }

        public Config GetConfig()
        {
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, "Config.json");

            var fileContent = File.ReadAllText(path);

            var config = JsonConvert.DeserializeObject<Config>(fileContent);


            // Add reverse relationships
            foreach(var group in config.LaunchConfigGroups)
            {
                group.Config = config;

                foreach(var launch in group.LaunchConfigs)
                {
                    launch.Group = group;
                }
            }

            return config;
        }


    }
}
