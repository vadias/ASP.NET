namespace AppAuthLesson4.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(string email, string roleName);
    }
}
