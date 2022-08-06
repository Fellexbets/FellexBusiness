using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    // since the interface Ihasprimarykey implements the method getprimary key as a string, if the primary key
    // is an integer, we need to convert it to a string so we can use it properly.
    public interface IHasPrimaryKey
    {
        string GetPrimaryKey();
    }
}
