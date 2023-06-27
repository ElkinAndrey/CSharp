# Пример авторизации с использованием JWT

* [Используемые технологии](#TechnologiesUsed)
* [Описание](#Description)
* [Настройка окружения разработки](#SettingDevelopmentEnvironment)

<a id="TechnologiesUsed"></a>

## Используемые технологии

Операционная система : Windows 11  
Среда разработки : Visual Studio 2022  
Тип проекта : Веб-API ASP.NET Core (Майкрософт)  

C# 11.0  
.NET 7.0  

<a id="Description"></a>

## Описание

В проекте два контроллера. В первом контроллере конечные точки для регистрации, входа, выхода и обновления Access токена. Во втором контроллере 5 конечных точке. Первая доступна все, даже не авторизованным пользователям, вторая доступна всем авторизованным пользователям, третья доступна первому менеджеру и администратору, четвертая второму менеджеру и администратору, пятая только администратору.

<a id="SettingDevelopmentEnvironment"></a>

## Настройка окружения разработки

Скачайте проект с Git Hub. В консоли откройте папку с проектом (в ней лежит файл Program.cs). В консоль введите команду
```bash
dotnet ef database update
```
после чего будет создана база данных.  
В бд нужно добавить роли. Для этого перейдите к файлу "AuthController.cs" по пути ".\JwtToken\Controllers\AuthController.cs". В этом файле найдите конструктор.
```bash
...
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
...
```
В конструкторе закоментирован код для добавления ролей в базу данных. Раскоментируйте код, запустите программу и вызовите любой из методов. После этого в базу данных будут добавлены роли. После добавления верните коменталии в коде обратно.  
Запустите программу, зареистрируйтесь и войдите в аккаунт.