using System;
using System.Collections.Generic;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace Bonobo.Gh.Geometry
{
    public class SerializeObject : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SerializeObject class.
        /// </summary>
        public SerializeObject()
          : base("Serialize Object", "Serialize",
              "Serialize Blender Object",
              Helper.CATEGORY, Helper.SUBCATEGORY_GEOMETRY)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Blender Object", "Object", "Blender Object to be serialized", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "JSON", "JSON", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IBlenderWrapperGoo goo = null;
            DA.GetData(0, ref goo);
            if (goo != null)
            {
                DA.SetData(0, goo.Wrapper.ToJson());
            }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("39ed851c-aae4-4506-934d-3a6cb4da2400"); }
        }
    }
}