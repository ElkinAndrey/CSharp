

var builder = WebApplication.CreateBuilder(args); //  включает внедрение зависимостей, позволяющее обращаться к настроенным службам в рамках приложения. 
builder.Services.AddControllersWithViews(); // добавляет в коллекцию сервисов сервисы, которые необходимы для работы контроллеров MVC


var app = builder.Build(); // для создания экземпляра WebApplication
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute( // Добавление маршрута к контроллеру
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // От куда начинать
app.Run(); // Запуск
