namespace AllYourGoods.Api.Models.Dtos.Responses
{
    public class ResponseCategoryDto
    {
        public ResponseCategoryDto(Guid id, string name, long weight, Guid menuId)
        {
            Id = id;
            Name = name;
            Weight = weight;
            MenuId = menuId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public long Weight { get; set; }
        public Guid MenuId { get; set; }

        public int? StatusCode { get; set; }
        public string? StatusMessage { get; set; }

        // You can display product names associated with the category if needed
        public ICollection<string> ProductNames { get; set; } = new List<string>();
    }
}
