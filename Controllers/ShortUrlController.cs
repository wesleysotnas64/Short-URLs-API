using Microsoft.AspNetCore.Mvc;
using Short_URLs_API.Entities;
using Short_URLs_API.DAO;
using Short_URLs_API.Services;

namespace Short_URLs_API.Controllers
{
    [ApiController]
    [Route("/shorturl-api")]
    public class ShortUrlController : ControllerBase
    {
        public ShortUrlController() { }

        [HttpPost("/shorten-url")]
        public IActionResult ShortenUrl([FromBody] string url)
        {
            ResponseAPI responseAPI = new();

            if (string.IsNullOrEmpty(url))
            {
                responseAPI.Message = "URL não pode ser nula ou vazia.";
                responseAPI.IsOk = false;

                return BadRequest(responseAPI);
            }

            ShortUrlDAO shortUrlDAO = new();
            responseAPI.ShortUrl = shortUrlDAO.GetShortUrlByUrl(url);

            if (responseAPI.ShortUrl == null)
            {
                HashService hashService = new();
                string hash;
                do
                {
                    hash = hashService.Generate();
                }
                while (shortUrlDAO.GetShortUrlByHash(hash) != null);

                responseAPI.ShortUrl = new ShortUrl
                {
                    Hash = hash,
                    Url = url
                };

                shortUrlDAO.CreateShortUrl(responseAPI.ShortUrl);
                responseAPI.Message = "Url gerado com sucesso.";
                responseAPI.IsOk = true;
            }

            return Ok(responseAPI);
        }

        [HttpPost("/call-hash")]
        public IActionResult CallHash([FromBody] string hash)
        {
            ResponseAPI responseAPI = new();

            if (string.IsNullOrEmpty(hash))
            {
                responseAPI.Message = "Link quebrado.";
                responseAPI.IsOk = false;
                return BadRequest(responseAPI);
            }

            ShortUrlDAO shortUrlDAO = new();
            responseAPI.ShortUrl = shortUrlDAO.GetShortUrlByHash(hash);

            if (responseAPI.ShortUrl == null)
            {
                responseAPI.Message = "Link quebrado.";
                responseAPI.IsOk = false;
                return BadRequest(responseAPI);
            }
            else
            {
                responseAPI.Message = "Hash encontrada com sucesso.";
                responseAPI.IsOk = true;
                return Ok(responseAPI);
            }
        }
    }
}
