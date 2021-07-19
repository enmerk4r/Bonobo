using System;
using System.Collections.Generic;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Rhino.Display;
using Rhino.Geometry;

namespace Bonobo.Gh.Scene
{
    public class ViewCaptureToFile : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ViewCaptureToFile class.
        /// </summary>
        public ViewCaptureToFile()
          : base("View Capture To File", "View Capture To File",
              "View Capture To File",
              Helper.CATEGORY, Helper.SUBCATEGORY_SCENE)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "Path", "Path", GH_ParamAccess.item);
            pManager.AddTextParameter("View", "View", "View", GH_ParamAccess.item, string.Empty);
            pManager.AddIntegerParameter("Width", "Width", "Width", GH_ParamAccess.item, -1);
            pManager.AddIntegerParameter("Height", "Height", "Height", GH_ParamAccess.item, -1);
            pManager.AddBooleanParameter("Draw Axes", "Draw Axes", "Draw Axes", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Draw Grid", "Draw Grid", "Draw Grid", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Draw Grid Axes", "Draw Grid Axes", "Draw Grid Axes", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Transparent Background", "Transparent Background", "Transparent Background", GH_ParamAccess.item, true);
            pManager.AddBooleanParameter("Capture", "Capture", "Capture", GH_ParamAccess.item);

            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
            pManager[4].Optional = true;
            pManager[5].Optional = true;
            pManager[6].Optional = true;
            pManager[7].Optional = true;
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
            string path = string.Empty;
            string view = string.Empty;
            int width = 0;
            int height = 0;
            bool drawAxes = false;
            bool drawGrid = false;
            bool drawGridAxes = false;
            bool transparentBackground = false;
            bool capture = false;

            DA.GetData(0, ref path);
            DA.GetData(1, ref view);
            DA.GetData(2, ref width);
            DA.GetData(3, ref height);
            DA.GetData(4, ref drawAxes);
            DA.GetData(5, ref drawGrid);
            DA.GetData(6, ref drawGridAxes);
            DA.GetData(7, ref transparentBackground);
            DA.GetData(8, ref capture);

            if (capture)
            {
                RhinoView v;
                if (string.IsNullOrEmpty(view))
                {
                    v = Rhino.RhinoDoc.ActiveDoc.Views.ActiveView;
                }
                else
                {
                    v = Rhino.RhinoDoc.ActiveDoc.Views.Find(view, false);
                }

                if (width < 0) width = v.Size.Width;
                if (height < 0) height = v.Size.Height;

                var viewCapture = new ViewCapture()
                {
                    Width = width,
                    Height = height,
                    ScaleScreenItems = false,
                    DrawAxes = drawAxes,
                    DrawGrid = drawGrid,
                    DrawGridAxes = drawGridAxes,
                    TransparentBackground = transparentBackground
                };

                var bitmap = viewCapture.CaptureToBitmap(v);

                if (bitmap != null)
                {
                    bitmap.Save(path);
                }
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
            get { return new Guid("470779d9-fccf-4a4a-b9ab-39e24f2b9423"); }
        }
    }
}