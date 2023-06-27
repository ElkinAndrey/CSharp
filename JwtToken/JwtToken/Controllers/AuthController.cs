using Azure.Core;
using JwtToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;

        public AuthController(IConfiguration configuration, ApplicationContext context)
        {
            _configuration = configuration;
            _context = context;

            /*
            _context.Roles.Add(new Role { Id = 1, Name = Roles.User });
            _context.Roles.Add(new Role { Id = 2, Name = Roles.Manager1 });
            _context.Roles.Add(new Role { Id = 3, Name = Roles.Manager2 });
            _context.Roles.Add(new Role { Id = 4, Name = Roles.Administrator });
            _context.SaveChanges();
            */
        }

        /// <summary>
        /// Зарегистироваться
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto request)
        {
            string passwordHash; // Хэш пароля
            byte[] passwordSalt; // Соль пароля

            // Хэшируется пароль
            PasswordHash.Create(request.Password, out passwordHash, out passwordSalt);

            // Создается новый пользователь
            var user = new User
            {
                UserName = request.Username, // Имя
                PasswordHash = passwordHash, // Хэш пароля
                PasswordSalt = passwordSalt, // Соль пароля
                Role = _context.Roles.FirstOrDefault(r => r.Id == request.RoleId)!,
            };

            // Пользователь добавляется в базу данных
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Войти
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            // Пользователь ищется в базе данных
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == request.Username);

            // Если не найден, то ошибка
            if (user is null)
                return BadRequest("User not found.");

            // Проверяется, правильно ли введен пароль
            if (!PasswordHash.Verify(request.Password, user.PasswordHash!, user.PasswordSalt))
                return BadRequest("Wrong password.");

            // Данные, которые будут записаны в токен
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!), // Имя пользователя
                new Claim(ClaimTypes.Role, user.Role.Name!) // Роль пользователя
            };

            // Генерируется токен доступа (часто обновляется)
            string accessToken = JwtTokens.CreateAccessToken(claims, _configuration.GetSection("AppSettings:Token").Value!);

            // Генерируется токен обновления (редко обновляется)
            var refreshToken = JwtTokens.CreateRefreshToken();

            // Токен обновления записывается в куки
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Куки можно будет изменить только при помощи бекенда, а не при помощи JS
                Expires = refreshToken.Expires // До какого числа будет жить токен
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            // Токен для обновления записывается в базу данных
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            _context.SaveChanges();

            return Ok(accessToken);
        }

        /// <summary>
        /// Выйти
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Проверить, есть ли токен в куки
            string? refreshToken = Request.Cookies["refreshToken"]; // Токен обновления
            if (refreshToken is null)
                return Ok();

            // Удалить токен обновления из куков
            Response.Cookies.Delete("refreshToken");

            // Удалить токен из базы дынных
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is not null)
            {
                user.RefreshToken = null;
                user.TokenExpires = null;
                user.TokenCreated = null;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        /// <summary>
        /// Выйти
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            // Достать токен обновления из куки
            string? refreshToken = Request.Cookies["refreshToken"];

            // Если токена нет, то выдать ошибку
            if (refreshToken is null)
                return BadRequest("Токена нет в куки");

            // Найти человека по токену в базе данных
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

            // Если не найден, но ошибка
            if (user is null)
                return BadRequest("Пользователь не найден");

            if (DateTime.Now > user.TokenExpires)
            {
                user.RefreshToken = null;
                user.TokenExpires = null;
                user.TokenCreated = null;
                await _context.SaveChangesAsync();
                return BadRequest("Вы давно не заходили, токен перестал быть действителен");
            }

            // Данные, которые будут записаны в токен
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!), // Имя пользователя
                new Claim(ClaimTypes.Role, user.Role.Name!) // Роль пользователя
            };

            // Генерируется токен доступа (часто обновляется)
            string accessToken = JwtTokens.CreateAccessToken(claims, _configuration.GetSection("AppSettings:Token").Value!);

            // Генерируется токен обновления (редко обновляется)
            var newRefreshToken = JwtTokens.CreateRefreshToken();

            // Токен обновления записывается в куки
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Куки можно будет изменить только при помощи бекенда, а не при помощи JS
                Expires = newRefreshToken.Expires // До какого числа будет жить токен
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            // Токен для обновления записывается в базу данных
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
            _context.SaveChanges();

            return Ok(accessToken);
        }
    }
}
