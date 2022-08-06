using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    // the attributeUsage function defines how attribute can be used in our program
    // in this case, we can only apply it to classes and only 1 time.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PathAttribute : Attribute
    {

        // the pathattribute class derives from the base system class Attribute, i am not entirely sure what are the benefits of this. i will try to find out
        public string Path { get; set; }

        // we have a Pathattribute constructor with the string path only
        // it is public, so we can call PathAttribute Path anywhere - this is the case in savechanges, loadfromfrile methos in the manager calss
        public PathAttribute(string path) 
        {
            Path = path;
        }
    }
}
