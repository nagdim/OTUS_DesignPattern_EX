namespace AuthTokenProject
{
    public interface ITokenGenerator
    {
        string GenerateToken(string value);
    }
}
