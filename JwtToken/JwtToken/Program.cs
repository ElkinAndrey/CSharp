using JwtToken.Models;
using M6T.Core.TupleModelBinder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Стандартный заголовок авторизации с использованием схемы Bearer (\"bearer {токен}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
        };
    });
builder.Services.AddAuthorization(options =>
{
    // Первая секция
    options.AddPolicy("FirstSection", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.Manager1)
                                   || x.User.HasClaim(ClaimTypes.Role, Roles.Administrator));
    });

    // Вторая секция
    options.AddPolicy("SecondSection", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.Manager2)
                                   || x.User.HasClaim(ClaimTypes.Role, Roles.Administrator));
    }); 
    
    // Только для администратора
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, Roles.Administrator));
    });
});
builder.Services.AddCors(options => options.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));
builder.Services.AddMvc(options =>
{
    options.ModelBinderProviders.Insert(0, new TupleModelBinderProvider());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NgOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
