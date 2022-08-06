using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RoadToDB
{
    [PathAttribute("categories.json")]
    public class Category : IHasPrimaryKey
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override string ToString() => $"{Id}: {Name} {Description}";

        public string GetPrimaryKey() => Id.ToString();
    }
}
