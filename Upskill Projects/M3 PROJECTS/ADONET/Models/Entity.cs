using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Models
{
    public abstract class Entity
    {
        public abstract void Create();

        public abstract void Update();

        public abstract void RemoveIten();

        public abstract void ListAll();
        




    }
}
