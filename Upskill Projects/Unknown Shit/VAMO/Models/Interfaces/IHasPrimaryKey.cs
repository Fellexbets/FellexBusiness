using System;
using System.Collections.Generic;
using System.Text;

namespace RoadToDB
{
    public interface IHasPrimaryKey
    {
        string GetPrimaryKey();
    }
}
