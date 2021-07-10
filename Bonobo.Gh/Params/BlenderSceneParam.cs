using Bonobo.Blender;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Gh.Params
{
    public class BlenderSceneParam : GH_PersistentParam<GH_BlenderScene>
    {
        public override Guid ComponentGuid => new Guid("d467afae-1a80-48b1-bc51-f1559815e2ce");

        public BlenderSceneParam() : base("Blender Scene", "Blender Scene", "Blender Scene Object", Helper.CATEGORY, Helper.SUBCATEGORY_PARAMS)
        {

        }

        protected override GH_GetterResult Prompt_Plural(ref List<GH_BlenderScene> values)
        {
            return GH_GetterResult.accept;
        }

        protected override GH_GetterResult Prompt_Singular(ref GH_BlenderScene value)
        {
            return GH_GetterResult.accept;
        }
    }
}
