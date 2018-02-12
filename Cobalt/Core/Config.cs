using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Core
{
    public class Config
    {
        public static Config Instance { get; private set; }

        private const int CheckEveryNSeconds = 1;

        private string fileName;
        private JObject data;
        private DateTime lastCheck;
        private DateTime lastModified;

        public Config(string fileName)
        {
            Config.Instance = this;

            this.fileName = fileName;
            this.ReloadData();
        }
        
        public T Get<T>(string key)
        {
            // Every .Get(...) call isn't a file I/O op. Allow it once every N seconds.
            if ((DateTime.Now - this.lastCheck).TotalSeconds >= CheckEveryNSeconds)
            {
                this.lastCheck = DateTime.Now;

                var modified = File.GetLastWriteTime(this.fileName);
                if (modified != this.lastModified)
                {
                    this.ReloadData();
                }
            }

            var value = data.GetValue(key).Value<T>();
            return value;
        }

        private void ReloadData()
        {
            var contents = System.IO.File.ReadAllText(this.fileName);
            this.data = JObject.Parse(contents);
            this.lastCheck = DateTime.Now;
            this.lastModified = File.GetLastWriteTime(this.fileName);
        }
    }
}
