using Minio.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
