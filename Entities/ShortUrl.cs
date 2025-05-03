namespace Short_URLs_API.Entities
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string? Hash { get; set; }
        public string? Url { get; set; }
    }
}
