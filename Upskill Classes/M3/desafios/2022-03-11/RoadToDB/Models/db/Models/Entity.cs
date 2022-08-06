namespace RoadToDB
{
    public abstract class Entity : IHasPrimaryKey
    {
        public abstract string GetPrimaryKey();
        public virtual string Header() => string.Format("{0," + ((System.Console.WindowWidth / 2) + (this.GetType().Name.Length / 2)) + "}\n\n", this.GetType().Name.ToUpper());
    }
}