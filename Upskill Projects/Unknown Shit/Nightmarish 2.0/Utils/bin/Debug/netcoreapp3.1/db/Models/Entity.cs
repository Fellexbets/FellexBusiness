

namespace EFCore
{
    // entity is an abstract class, it adopts IHasPrimarykey interface, making all of its children adopt it too. It is the same case
    // with those two methods. It is nice to have this entity to safeguard the entities and make sure they all fit to the format we want.
    public abstract class Entity : IHasPrimaryKey
    {
        public abstract string GetPrimaryKey();
        public abstract string Header();
    }
}