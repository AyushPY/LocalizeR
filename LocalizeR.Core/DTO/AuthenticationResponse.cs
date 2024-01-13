namespace LocalizeR.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;

        public Guid Id { get; set; } = Guid.Empty;

        public DateTime ExpirationTime { get; set; }
    }
}
