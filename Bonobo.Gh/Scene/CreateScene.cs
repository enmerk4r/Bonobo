using System;
using System.Collections.Generic;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Bonobo.Gh
{
    public class CreateScene : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CreateScene class.
        /// </summary>
        public CreateScene()
          : base("Create Scene", "Create Scene",
              "Create Blender Scene",
              Helper.CATEGORY, Helper.SUBCATEGORY_SCENE)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
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
            get { return new Guid("ed7eaf14-2084-4b84-b175-4769ec3183cf"); }
        }
    }
}