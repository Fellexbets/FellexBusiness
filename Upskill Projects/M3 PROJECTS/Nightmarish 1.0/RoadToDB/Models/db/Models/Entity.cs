namespace RoadToDB
{
    public abstract class Entity : IHasPrimaryKey
    {
        public abstract string GetPrimaryKey();
        public abstract string Header();
    }
}