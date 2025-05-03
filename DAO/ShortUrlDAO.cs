using Npgsql;
using Short_URLs_API.Entities;
using Short_URLs_API.Services;

namespace Short_URLs_API.DAO
{
    public class ShortUrlDAO
    {
        private readonly ConnectionService connectionService;

        public ShortUrlDAO()
        {
            connectionService = new ConnectionService();
        }

        public void CreateShortUrl(ShortUrl shortUrl)
        {
            try
            {
                connectionService.OpenConnection();
                using var command = new NpgsqlCommand(@"
                    INSERT INTO short_urls (hash, url)
                    VALUES (@hash, @url);
                ", connectionService.connection);

                command.Parameters.AddWithValue("@hash", shortUrl.Hash);
                command.Parameters.AddWithValue("@url", shortUrl.Url);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar URL encurtada: {ex.Message}");
                throw;
            }
            finally
            {
                connectionService.CloseConnection();
            }
        }

        public ShortUrl? GetShortUrlByUrl(string url)
        {
            try
            {
                connectionService.OpenConnection();

                using var command = new NpgsqlCommand(@"
                    SELECT id, hash, url 
                    FROM short_urls 
                    WHERE url = @url
                    LIMIT 1;
                ", connectionService.connection);

                command.Parameters.AddWithValue("@url", url);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new ShortUrl
                    {
                        Id = reader.GetInt32(0),
                        Hash = reader.GetString(1),
                        Url = reader.GetString(2)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar a URL '{url}': {ex.Message}");
                throw;
            }
            finally
            {
                connectionService.CloseConnection();
            }
        }

        public ShortUrl? GetShortUrlByHash(string hash)
        {
            try
            {
                connectionService.OpenConnection();

                using var command = new NpgsqlCommand(@"
                    SELECT id, hash, url 
                    FROM short_urls 
                    WHERE hash = @hash
                    LIMIT 1;
                ", connectionService.connection);

                command.Parameters.AddWithValue("@hash", hash);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new ShortUrl
                    {
                        Id = reader.GetInt32(0),
                        Hash = reader.GetString(1),
                        Url = reader.GetString(2)
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar o hash '{hash}': {ex.Message}");
                throw;
            }
            finally
            {
                connectionService.CloseConnection();
            }
        }
    }
}
