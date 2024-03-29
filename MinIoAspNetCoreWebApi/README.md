# Пример использования Minio.AspNetCore

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
Minio.AspNetCore 4.0.8  

<a id="Description"></a>

## Описание

В проекте две конечные точки. Первая для добавления файла в хранилище. В Post запрос вводится строка с названием файла, после чего в хранилище добавится новый .txt файл, который будет содержать в себе это название. Вторая для полуения файла из хранилища. В Post запрос вводится название файла, после чего будет скачан файл с этим названием.

Все названия пишутся без расширений. В программе не предусмотрена обработка ошибок.

<a id="SettingDevelopmentEnvironment"></a>

## Настройка окружения разработки

Для того чтобы запустить проект необходимо установить локальное хранилище Min IO.
После установки перейдите в раздел "Access Keys". В нем посмотрите ключ доступа (Access Key) и секретный ключ (Secret Key). Вставьте ключи на соответствующие места в файле Program.cs. Ниже представлен код, где нужно поменять ключи.
```bash
...
builder.Services.AddMinio(options =>
{
	options.Endpoint = "127.0.0.1:9000";
	options.AccessKey = "fsqz07kszl6tspLK";
	options.SecretKey = "UZFeVQ5pz5mUMbiyxb7Nx56h7ry9JQ9R";
	options.ConfigureClient(client =>
	{
		client.WithSSL(false);
	});
});
...
```
По умолчанию в проекте используется корзина с названием "my-bucket". Создайте корзину с таким же именем или замените название корзины в проекте. Название меняется в файле MinIoRepository.cs. Ниже представлен код, где можно изменить название корзины.
```bash
...
/// <summary>
/// Название корзины
/// </summary>
private readonly string bucketName = "my-bucket";
...
```
После смены запустите проект. В открывшемся окне Swagger протестируйте методы. 