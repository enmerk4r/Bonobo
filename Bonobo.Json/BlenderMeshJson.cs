using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Json
{
    public class BlenderMeshJson
    {
        [JsonProperty("mesh")]
        public string Mesh { get; set; }
        [JsonProperty("mass")]
        public double Mass { get; set; }
        [JsonProperty("collision_shape")]
        public string CollisionShape { get; set; }
        [JsonProperty("friction")]
        public double Friction { get; set; }
        [JsonProperty("use_margin")]
        public bool UseMargin { get; set; }
        [JsonProperty("collision_margin")]
        public double CollisionMargin { get; set; }
        [JsonProperty("linear_damping")]
        public double LinearDamping { get; set; }
        [JsonProperty("angular_damping")]
        public double AngularDamping { get; set; }
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }
}
