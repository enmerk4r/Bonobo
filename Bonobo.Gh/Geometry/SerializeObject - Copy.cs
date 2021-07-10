using System;
using System.Collections.Generic;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Newtonsoft.Json;
using Rhino.Geometry;

namespace Bonobo.Gh.Geometry
{
    public class SerializeObjects : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SerializeObject class.
        /// </summary>
        public SerializeObjects()
          : base("Serialize Objects", "Serialize",
              "Serialize Blender Objects to list",
              Helper.CATEGORY, Helper.SUBCATEGORY_GEOMETRY)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Blender Object", "Object", "Blender Object to be serialized", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON list", "JSON list", "JSON list", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<IBlenderWrapperGoo> goo = new List<IBlenderWrapperGoo>();
            List<string> strings = new List<string>();
            DA.GetDataList(0, goo);
            foreach(var g in goo)
            {
                strings.Add(g.Wrapper.ToJson());
            }
            DA.SetData(0, JsonConvert.SerializeObject(strings));
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
            get { return new Guid("c26674f6-7f62-47be-af7c-a11f312eb308"); }
        }
    }
}