using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args); //  включает внедрение зависимостей, позволяющее обращаться к настроенным службам в рамках приложения. 

builder.Services.AddAuthentication("Cookie") // Добавление аутентификации
    .AddCookie("Cookie", config =>
    {
        config.LoginPath = "/Admin/Login"; // Куда перейти при попытке аутентификации
        config.AccessDeniedPath = "/Home/AccessDenied";
    }); // Использование куки
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Administrator"); // Только администратор имеет доступ к функциям администратора
    });
    /*options.AddPolicy("Manager", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Manager");
    });*/
    options.AddPolicy("Manager", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager") // Менеджер и администратор имеют доступ к функциям менеджеда
                                   || x.User.HasClaim(ClaimTypes.Role, "Administrator")); 
    });
}); // Добавление авторизации

builder.Services.AddControllersWithViews(); // добавляет в коллекцию сервисов сервисы, которые необходимы для работы контроллеров MVC


var app = builder.Build(); // для создания экземпляра WebApplication
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication(); // Добавление аутентификации
app.UseAuthorization(); // Добавление авторизации

app.MapControllerRoute( // Добавление маршрута к контроллеру
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // От куда начинать
app.Run(); // Запуск
