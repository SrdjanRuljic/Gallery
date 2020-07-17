namespace Gallery.WebAPI.Auth
{
    public static class TokenHelper
    {
        public static object GenerateJwt(string username, string role, IJwtFactory jwtFactory) => new
        {
            auth_token = jwtFactory.GenerateEncodedToken(username, role)
        };
    }
}
