using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtToken.Models
{
    /// <summary>
    /// Работа с Jwt токенами
    /// </summary>
    public static class JwtTokens
    {
        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public static TimeSpan AccessTimeSpan { get; } = new TimeSpan(
            days: 1,
            hours: 0,
            minutes: 0,
            seconds: 0);

        /// <summary>
        /// Время жизни токена обновления
        /// </summary>
        public static TimeSpan RefreshTimeSpan { get; } = new TimeSpan(
            days: 60,
            hours: 0,
            minutes: 0,
            seconds: 0);

        /// <summary>
        /// Создать токен доступа
        /// </summary>
        /// <param name="claims">Параметры</param>
        /// <param name="secretKey">Секретный ключ</param>
        /// <returns>Токен доступа</returns>
        public static string CreateAccessToken(List<Claim> claims, string secretKey)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.Add(AccessTimeSpan),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Создать токен обновления
        /// </summary>
        /// <returns>Токен обновления</returns>
        public static RefreshToken CreateRefreshToken()
            => new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.Add(RefreshTimeSpan),
                Created = DateTime.Now,
            };
    }
}
