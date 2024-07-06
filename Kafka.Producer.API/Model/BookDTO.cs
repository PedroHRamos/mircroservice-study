namespace Kafka.Producer.API.Model
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AuthorFullName { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
