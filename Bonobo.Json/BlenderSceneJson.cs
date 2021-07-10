using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Json
{
    public class BlenderSceneJson
    {
        [JsonProperty("meshes")]
        public List<string> Meshes { get; set; }
    }
}
