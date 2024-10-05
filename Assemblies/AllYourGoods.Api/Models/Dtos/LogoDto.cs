namespace AllYourGoods.Api.Models.Dtos
{
    public class LogoDto
    {
        public string URL { get; set; } = null!;          
        public string AltText { get; set; } = null!;      
        public string MimeType { get; set; } = null!;     
        public float FileSize { get; set; }               
    }
}
