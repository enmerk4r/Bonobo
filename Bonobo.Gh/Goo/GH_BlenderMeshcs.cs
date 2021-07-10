
using Bonobo.Blender;
using Grasshopper.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Gh
{
    public class GH_BlenderMesh : GH_Goo<BlenderMesh>, IBlenderWrapperGoo
    {
        public override bool IsValid => this.Value != null && this.Value.Mesh.IsValid;

        public override string TypeName => "BlenderMesh";

        public override string TypeDescription => "Grasshopper representation of a Blender mesh";

        public IBlenderWrapper Wrapper => this.Value;

        public GH_BlenderMesh(BlenderMesh mesh)
        {
            this.Value = mesh;
        }

        public GH_BlenderMesh()
        {
            this.Value = null;
        }

        public override IGH_Goo Duplicate()
        {
            return new GH_BlenderMesh(this.Value.Clone());
        }

        public override string ToString()
        {
            return "Blender Mesh";
        }
    }
}
