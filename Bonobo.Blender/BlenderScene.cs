using Bonobo.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Blender
{
    public class BlenderScene : IBlenderWrapper
    {
        public List<BlenderMesh> Meshes { get; set; }

        public BlenderScene()
        {
            this.Meshes = new List<BlenderMesh>();
        }

        public void AddMeshes(List<BlenderMesh> meshes)
        {
            this.Meshes.AddRange(meshes);
        }

        public void AddMesh(BlenderMesh mesh)
        {
            this.Meshes.Add(mesh);
        }

        public BlenderScene Clone()
        {
            BlenderScene scene = new BlenderScene();
            scene.AddMeshes(this.Meshes.Select(m => m.Clone()).ToList());
            return scene;
        }

        public string ToJson()
        {
            BlenderSceneJson dto = new BlenderSceneJson();
            dto.Meshes = this.Meshes.Select(m => m.ToJson()).ToList();
            return JsonConvert.SerializeObject(dto);
        }

        public void FromJson(string json)
        {
            BlenderSceneJson dto = new BlenderSceneJson();
            this.AddMeshes(dto.Meshes.Select(m => new BlenderMesh(m)).ToList());
        }
    }
}
