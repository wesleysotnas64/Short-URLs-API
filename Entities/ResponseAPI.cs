namespace Short_URLs_API.Entities
{
    public class ResponseAPI
    {
        public string? Message { get; set; }
        public bool IsOk { get; set; }
        public ShortUrl? ShortUrl { get; set; }
    }
}
