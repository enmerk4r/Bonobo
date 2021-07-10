using System;
using System.Collections.Generic;
using System.IO;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using Rhino.Geometry;

namespace Bonobo.Gh
{
    public class LoadFrame : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the LoadFrame class.
        /// </summary>
        public LoadFrame()
          : base("Load Frame", "Load Frame",
              "Load a frame of the Blender simulation",
              Helper.CATEGORY, Helper.SUBCATEGORY_GEOMETRY)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Directory", "Directory", "Export Directory", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Frame", "Frame", "Frame Number", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Meshes", "Meshes", "Rhino Meshes", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string path = string.Empty;
            int frame = 0;

            DA.GetData(0, ref path);
            DA.GetData(1, ref frame);

            string filepath = Path.Combine(path, $"{frame}.json");
            if (!File.Exists(filepath)) throw new Exception($"Frame {frame} could not be found");

            string json = string.Empty;

            using(StreamReader reader = new StreamReader(filepath))
            {
                json = reader.ReadToEnd();
            }

            List<string> stringList = JsonConvert.DeserializeObject<List<string>>(json);

            List<Mesh> meshes = new List<Mesh>();
            foreach(string m in stringList)
            {
                meshes.Add((Mesh)Mesh.FromJSON(m));
            }

            DA.SetDataList(0, meshes);

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
            get { return new Guid("26621763-1517-4882-8931-97e5f5f91ccb"); }
        }
    }
}