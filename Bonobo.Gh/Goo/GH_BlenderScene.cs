using Bonobo.Blender;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Gh
{
    public class GH_BlenderScene : GH_Goo<BlenderScene>, IBlenderWrapperGoo
    {
        public override bool IsValid => true;

        public override string TypeName => "Blender Scene";

        public override string TypeDescription => "Blender Scene Object";

        public IBlenderWrapper Wrapper => this.Value;

        public GH_BlenderScene(BlenderScene scene)
        {
            this.Value = scene;
        }

        public override IGH_Goo Duplicate()
        {
            return new GH_BlenderScene(this.Value.Clone());
        }

        public override string ToString()
        {
            return $"Blender Scene | {this.Value.Meshes.Count} meshes";
        }
    }
}
