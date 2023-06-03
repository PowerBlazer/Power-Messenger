namespace PowerMessenger.Domain.DTOs.Authorization
{
    public class RefreshTokenRequest
    {
        public RefreshTokenRequest(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; }
        /// <summary>
        /// Токен обновления
        /// </summary>
        public string RefreshToken { get; }
    }
}