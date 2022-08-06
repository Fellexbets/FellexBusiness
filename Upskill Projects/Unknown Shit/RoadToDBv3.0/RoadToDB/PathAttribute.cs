using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadToDB
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class PathAttribute : Attribute
    {
        public string Path { get; set; }

        public PathAttribute(string path)
        {
            Path = path;
        }
    }
}
