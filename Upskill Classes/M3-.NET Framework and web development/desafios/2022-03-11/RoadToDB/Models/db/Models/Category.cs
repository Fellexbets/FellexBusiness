using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("categories.json")]
    public class Category : Entity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override string GetPrimaryKey() => Id.ToString();

        public override string ToString() => string.Format("{0, 20} | {1, 20} | {2, 30}", Id, Name, Description);

        public override string Header() => base.Header() + string.Format("{0, 20} | {1, 20} | {2, 30}", "Id", "Name", "Description");
    }
}
