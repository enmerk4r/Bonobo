using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonobo.Blender
{
    public interface IBlenderWrapper
    {
        string ToJson();
        void FromJson(string json);
    }
}
