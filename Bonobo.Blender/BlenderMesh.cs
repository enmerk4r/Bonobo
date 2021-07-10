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
        public double Mass { get; set; }
        public string CollisionShape { get; set; }
        public double Friction { get; set; }
        public bool UseMargin { get; set; }
        public double CollisionMargin { get; set; }
        public double LinearDamping { get; set; }
        public double AngularDamping { get; set; }
        public bool IsActive { get; set; }

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
            clone.Mass = this.Mass;
            clone.CollisionShape = this.CollisionShape;
            clone.Friction = this.Friction;
            clone.UseMargin = this.UseMargin;
            clone.CollisionMargin = this.CollisionMargin;
            clone.LinearDamping = this.LinearDamping;
            clone.AngularDamping = this.AngularDamping;
            clone.IsActive = this.IsActive;

            return clone;
        }


        public string ToJson()
        {
            BlenderMeshJson dto = new BlenderMeshJson();
            SerializationOptions options = new SerializationOptions();
            dto.Mesh = this.Mesh.ToJSON(options);
            dto.Mass = this.Mass;
            dto.CollisionShape = this.CollisionShape;
            dto.Friction = this.Friction;
            dto.UseMargin = this.UseMargin;
            dto.CollisionMargin = this.CollisionMargin;
            dto.LinearDamping = this.LinearDamping;
            dto.AngularDamping = this.AngularDamping;
            dto.IsActive = this.IsActive;

            return JsonConvert.SerializeObject(dto);
        }

        public void FromJson(string json)
        {
            BlenderMeshJson dto = JsonConvert.DeserializeObject<BlenderMeshJson>(json);
            this.Mesh = (Mesh)Mesh.FromJSON(dto.Mesh);
            this.Mass = dto.Mass;
            this.CollisionShape = dto.CollisionShape;
            this.Friction = dto.Friction;
            this.UseMargin = dto.UseMargin;
            this.CollisionMargin = dto.CollisionMargin;
            this.LinearDamping = dto.LinearDamping;
            this.AngularDamping = dto.AngularDamping;
            this.IsActive = dto.IsActive;
        }
    }
}
