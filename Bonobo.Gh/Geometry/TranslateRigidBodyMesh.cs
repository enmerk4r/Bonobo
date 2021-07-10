using System;
using System.Collections.Generic;
using Bonobo.Blender;
using Bonobo.Gh.Params;
using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Bonobo.Gh
{
    public class TranslateRigidBodyMesh : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public TranslateRigidBodyMesh()
          : base("Translate Rigid", "Translate Rigid",
              "Convert a Rhino Mesh into Blender Rigid Body object",
              Helper.CATEGORY, Helper.SUBCATEGORY_GEOMETRY)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "Mesh", "Rhino Mesh", GH_ParamAccess.item);
            pManager.AddNumberParameter("Mass", "Mass", "Rigid Body Mass", GH_ParamAccess.item, 20);
            pManager.AddTextParameter("Collision Shape", "Collision Shape", "Collision Shape", GH_ParamAccess.item, "BOX");
            pManager.AddNumberParameter("Friction", "Friction", "Friction", GH_ParamAccess.item, 1);
            pManager.AddBooleanParameter("Use Margin", "Use Margin", "Use Margin", GH_ParamAccess.item, true);
            pManager.AddNumberParameter("Collision Margin", "Collision Margin", "Collision Margin", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Linear Damping", "Linear Damping", "Linear Damping", GH_ParamAccess.item, 0.35);
            pManager.AddNumberParameter("Angular Damping", "Angular Damping", "Angular Damping", GH_ParamAccess.item, 0.6);
            pManager.AddBooleanParameter("Is Active", "Is Active", "Is Active", GH_ParamAccess.item, true);

            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
            pManager[4].Optional = true;
            pManager[5].Optional = true;
            pManager[6].Optional = true;
            pManager[7].Optional = true;
            pManager[8].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Blender Mesh", "BM", "Blender Mesh", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh mesh = null;
            double mass = default(double);
            string collisionShape = string.Empty;
            double friction = default(double);
            bool useMargin = false;
            double collisionMargin = default(double);
            double linearDamping = default(double);
            double angularDamping = default(double);
            bool isActive = false;

            if (DA.GetData(0, ref mesh))
            {
                DA.GetData(1, ref mass);
                DA.GetData(2, ref collisionShape);
                DA.GetData(3, ref friction);
                DA.GetData(4, ref useMargin);
                DA.GetData(5, ref collisionMargin);
                DA.GetData(6, ref linearDamping);
                DA.GetData(7, ref angularDamping);
                DA.GetData(8, ref isActive);

                BlenderMesh b = new BlenderMesh(mesh);
                b.Mass = mass;
                b.CollisionShape = collisionShape;
                b.Friction = friction;
                b.UseMargin = useMargin;
                b.CollisionMargin = collisionMargin;
                b.LinearDamping = linearDamping;
                b.AngularDamping = angularDamping;
                b.IsActive = isActive;

                GH_BlenderMesh bmesh = new GH_BlenderMesh(b);
                DA.SetData(0, bmesh);
            }


        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("00c7efb5-bfcc-42a9-a311-53893064f0c3"); }
        }
    }
}
