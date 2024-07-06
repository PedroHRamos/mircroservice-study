using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kafka.Consumer.Console.Models
{
    public class BookDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("authorFullName")]
        public string? AuthorFullName { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("creationDate")]
        public DateTime? CreationDate  { get; set; }


        public override string ToString()
        {
            return $"Id: {this.Id}\n" +
                $"Name: {this.Name}\n" +
                $"AuthorFullName: {this.AuthorFullName}\n" +
                $"Description: {this.Description}\n" +
                $"CreationDate: {this.CreationDate}\n" +
                $"Name: {this.Name}\n";
        }

    }
}
