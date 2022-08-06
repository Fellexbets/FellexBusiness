using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PathAttribute : Attribute
    {
        public string Path { get; set; }
        public PathAttribute(string path) 
        {
            Path = path;
        }
    }
}
