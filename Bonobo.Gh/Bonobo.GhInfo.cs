using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Bonobo.Gh
{
    public class BonoboInfo : GH_AssemblyInfo
  {
    public override string Name
    {
        get
        {
            return "Bonobo";
        }
    }
    public override Bitmap Icon
    {
        get
        {
            //Return a 24x24 pixel bitmap to represent this GHA library.
            return null;
        }
    }
    public override string Description
    {
        get
        {
            //Return a short string describing the purpose of this GHA library.
            return "";
        }
    }
    public override Guid Id
    {
        get
        {
            return new Guid("1c91ae5b-7fb5-4e9b-98dc-4f4f0b274eee");
        }
    }

    public override string AuthorName
    {
        get
        {
            //Return a string identifying you or your company.
            return "";
        }
    }
    public override string AuthorContact
    {
        get
        {
            //Return a string representing your preferred contact details.
            return "";
        }
    }
}
}
