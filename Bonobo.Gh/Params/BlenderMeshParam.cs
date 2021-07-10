using Bonobo.Blender;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Gh.Params
{
    public class BlenderMeshParam : GH_PersistentParam<GH_BlenderMesh>
    {
        public BlenderMeshParam() : 
            base("Blender Mesh", "Blender Mesh", "A collection of BlenderMeshes", Helper.CATEGORY, Helper.SUBCATEGORY_PARAMS)
        {
        }

        public override Guid ComponentGuid => new Guid("872c3561-9bc4-40fb-b860-b402aa539560");

        protected override GH_GetterResult Prompt_Plural(ref List<GH_BlenderMesh> values)
        {
            return GH_GetterResult.accept;
        }

        protected override GH_GetterResult Prompt_Singular(ref GH_BlenderMesh value)
        {
            return GH_GetterResult.accept;
        }
    }
}
