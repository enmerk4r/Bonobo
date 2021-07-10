using Bonobo.Json;
using Newtonsoft.Json;
using Rhino.FileIO;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Blender
{
    public class BlenderMesh : IBlenderWrapper
    {
        public Mesh Mesh { get; set; }

        public BlenderMesh(Mesh mesh)
        {
            this.Mesh = mesh;
        }

        public BlenderMesh(string json)
        {
            this.FromJson(json);
        }

        public BlenderMesh Clone()
        {
            BlenderMesh clone = new BlenderMesh((Mesh)this.Mesh.Duplicate());
            return clone;
        }


        public string ToJson()
        {
            BlenderMeshJson dto = new BlenderMeshJson();
            SerializationOptions options = new SerializationOptions();
            dto.Mesh = this.Mesh.ToJSON(options);

            return JsonConvert.SerializeObject(dto);
        }

        public void FromJson(string json)
        {
            BlenderMeshJson dto = JsonConvert.DeserializeObject<BlenderMeshJson>(json);
            this.Mesh = (Mesh)Mesh.FromJSON(dto.Mesh);
        }
    }
}
