using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Bonobo.Blender;
using Grasshopper.Kernel;
using Newtonsoft.Json;
using Rhino.Geometry;

namespace Bonobo.Gh
{
    public class Solver : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CreateScene class.
        /// </summary>
        public Solver()
          : base("Solver", "Solver",
              "Blender Solver",
              Helper.CATEGORY, Helper.SUBCATEGORY_SCENE)
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Scratch Folder", "Scratch Folder", "Scratch Folder for saving simulation inputs", GH_ParamAccess.item);
            pManager.AddTextParameter("Destination Folder", "Destination Folder", "Folder for saving simulation results", GH_ParamAccess.item);
            pManager.AddTextParameter("Python Path", "Python Path", "Python Path", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Step", "Step", "Simulation step", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start", "Start", "Simulation start", GH_ParamAccess.item);
            pManager.AddIntegerParameter("End", "End", "Simulation end", GH_ParamAccess.item);
            pManager.AddGenericParameter("Blender Meshes", "Blender Meshes", "Blender Meshes", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Results", "Results", "Simulation results", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string scratch = string.Empty;
            string destination = string.Empty;
            string pythonPath = string.Empty;
            int step = 0;
            int start = 0;
            int end = 0;

            List<IBlenderWrapperGoo> goo = new List<IBlenderWrapperGoo>();

            DA.GetData(0, ref scratch);
            DA.GetData(1, ref destination);
            DA.GetData(2, ref pythonPath);
            DA.GetData(3, ref step);
            DA.GetData(4, ref start);
            DA.GetData(5, ref end);
            DA.GetDataList(6, goo);

            
            List<string> strings = new List<string>();
            foreach (var g in goo)
            {
                strings.Add(g.Wrapper.ToJson());
            }

            string inputJSON = JsonConvert.SerializeObject(strings);
            string inputName = $"{Guid.NewGuid()}.json";
            string simPath = Path.Combine(scratch, inputName);
            using (StreamWriter writer = new StreamWriter(simPath))
            {
                writer.Write(inputJSON);
            }

            // Run Simulation
            string folderName = Guid.NewGuid().ToString();
            this.RunSimulation(pythonPath, simPath, step, start, end, destination, folderName);

            string resultsPath = Path.Combine(destination, folderName);

            DA.SetData(0, resultsPath);

        }

        private void RunSimulation(string pythonPath, string simPath, int steps, int startFrame, int endFrame, string root, string folderName)
        {
            string simulationScript = $"C:\\Users\\{Environment.UserName}\\Documents\\GitHub\\Bonobo\\Bonobo.Py\\run_simulation.py";
            //string simulationScript = $"C:\\Users\\{Environment.UserName}\\Documents\\GitHub\\Bonobo\\Bonobo.Py\\test.py";

            var startInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = pythonPath,
                Arguments = $"{simulationScript} \"{simPath}\" {steps} {startFrame} {endFrame} {root} {folderName}",
                //Arguments = $"{simulationScript}",
                RedirectStandardError = false,
                RedirectStandardOutput = false
            };

            Process p = Process.Start(startInfo);
            //string output = process.StandardOutput.ReadToEnd();
            p.WaitForExit();
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