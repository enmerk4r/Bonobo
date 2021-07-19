using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace Bonobo.Gh.Scene
{
    public class BakeAndReplace : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the BakeAndReplace class.
        /// </summary>
        public BakeAndReplace()
          : base("Bake And Replace", "Bake and Replace",
              "Bake and Replace",
              Helper.CATEGORY, Helper.SUBCATEGORY_SCENE)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Meshes", "Meshes", "Meshes to bake", GH_ParamAccess.list);
            pManager.AddTextParameter("Layer", "Layer", "Name of layer to bake to / delete from", GH_ParamAccess.item, "Default");
            pManager.AddBooleanParameter("Bake", "Bake", "Bake and replace", GH_ParamAccess.item, false);

            pManager[1].Optional = true;
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Result", "Result", "Bake result", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            this.Message = "WARNING: This component will delete all geometry\n in the specified layer";

            List<Mesh> meshes = new List<Mesh>();
            string layer = string.Empty;
            bool bake = false;

            DA.GetDataList(0, meshes);
            DA.GetData(1, ref layer);
            DA.GetData(2, ref bake);

            if (bake)
            {
                this.ClearLayer(layer);
                this.Bake(meshes);
                DA.SetData(0, true);
            }
            else
            {
                DA.SetData(0, false);
            }
        }

        private void ClearLayer(string name)
        {
            Rhino.DocObjects.RhinoObject[] rhobjs = Rhino.RhinoDoc.ActiveDoc.Objects.FindByLayer(name);
            if (rhobjs != null) Rhino.RhinoDoc.ActiveDoc.Objects.Delete(rhobjs.Select(o => o.Id), true);
        }

        private void Bake(List<Mesh> meshes)
        {
            foreach (Mesh m in meshes)
            {
                Rhino.RhinoDoc.ActiveDoc.Objects.AddMesh(m);
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
            get { return new Guid("1a6b2e2e-8989-4a73-af9d-5a03007d331b"); }
        }
    }
}