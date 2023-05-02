using System.Net.Mime;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.AspNetCore;
using Minio.DataModel;
using Newtonsoft.Json;

namespace MinIoAspNetCoreWebApi
{
	/// <summary>
	/// Работа с Min IO хранилищем
	/// </summary>
	public class MinIoRepository
	{
		/// <summary>
		/// Функция для преобразования стрима в скачиваемый файл
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="contentType"></param>
		/// <param name="fileDownloadName"></param>
		/// <returns></returns>
		public delegate FileStreamResult FileCreator(Stream stream, string contentType, string? fileDownloadName);

		/// <summary>
		/// Клиент
		/// </summary>
		MinioClient client;

		/// <summary>
		/// Название корзины
		/// </summary>
		private readonly string bucketName = "my-bucket";

		/// <summary>
		/// Работа с Min IO хранилищем
		/// </summary>
		/// <param name="factory">Генератор Min IO клиента</param>
		public MinIoRepository(IMinioClientFactory factory)
		{
			this.client = factory.CreateClient();
		}

		/// <summary>
		/// Добавить в хранилище новый файл
		/// </summary>
		/// <param name="name">Имя и содержимое файла</param>
		/// <returns></returns>
		public async Task Add(string name)
		{
			using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(name)))
			{
				await client.PutObjectAsync(
					new PutObjectArgs()
						.WithObjectSize(stream.Length)
						.WithStreamData(stream)
						.WithBucket(bucketName)
						.WithObject($"{name}.txt")
						.WithContentType("text/plain")
				);
			}
		}

		/// <summary>
		/// Получить файл из хранилища
		/// </summary>
		/// <param name="name">Название файла</param>
		/// <param name="fileCreator">Функция для преобразования стрима в файл</param>
		/// <returns></returns>
		public async Task<FileStreamResult> Get(string name, FileCreator fileCreator)
		{
			var objectName = $"{name}.txt";
			MemoryStream stream2 = new MemoryStream();
			await client.GetObjectAsync(
				new GetObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName)
					.WithCallbackStream((stream) =>
					{
						stream.CopyTo(stream2);
					})
			);
			stream2.Position = 0;

			return fileCreator(stream2, "text/plain", $"{name}.txt");
		}
	}
}
