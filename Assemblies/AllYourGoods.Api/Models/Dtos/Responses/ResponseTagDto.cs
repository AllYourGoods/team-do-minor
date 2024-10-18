namespace AllYourGoods.Api.Models.Dtos.Responses
{
    public class ResponseTagDto
    {
        public ResponseTagDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public int? StatusCode { get; set; }
        public string? StatusMessage { get; set; }

        // You can display product names associated with the tag if needed
        public ICollection<string> ProductNames { get; set; } = new List<string>();
    }
}
