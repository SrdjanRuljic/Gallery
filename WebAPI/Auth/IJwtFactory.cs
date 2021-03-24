namespace Gallery.WebAPI.Auth
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(string username, string role);
    }
}
