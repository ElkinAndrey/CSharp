using Minio;

/*
{
	"url": "http://127.0.0.1:9000",
	"accessKey": "fsqz07kszl6tspLK",
	"secretKey": "UZFeVQ5pz5mUMbiyxb7Nx56h7ry9JQ9R",
	"api": "s3v4",
	"path": "auto"
}
*/

var endpoint = "127.0.0.1:9000";
var accessKey = "fsqz07kszl6tspLK";
var secretKey = "UZFeVQ5pz5mUMbiyxb7Nx56h7ry9JQ9R";
var bucketName = "my-bucket";
var secure = false;
MinioClient minio = new MinioClient()
									.WithEndpoint(endpoint)
									.WithCredentials(accessKey, secretKey)
									.WithSSL(secure)
									.Build();

// Загрузить файл в хранилище из стрима
/*
var objectName = "file.txt";
var contentType = "text/plain";
Stream stream = new MemoryStream(new byte[] { 65, 66, 65, 67 });
await minio.PutObjectAsync(
	new PutObjectArgs()
		.WithObjectSize(stream.Length)
		.WithStreamData(stream)
		.WithBucket(bucketName)
		.WithObject(objectName)
		.WithContentType(contentType)
);
*/

// Загрузить файл из хранилища в стрим
/*
var objectName = "file.txt";
await minio.GetObjectAsync(
	new GetObjectArgs()
		.WithBucket(bucketName)
		.WithObject(objectName)
		.WithCallbackStream((stream) =>
		{
			stream.CopyTo(Console.OpenStandardOutput());
		})
);
*/

// Загрузить файл с компьютера в хранилище 
/*
var objectName = "file.png";
var filePath = "C:\\Users\\Ridbir\\Desktop\\file.png";
await minio.PutObjectAsync(bucketName, objectName, filePath);
*/

// Загрузить файл на компьютер из хранилища
/*
var objectName = "file.png";
var filePath = "C:\\Users\\Ridbir\\Desktop\\new-file.png";
await minio.GetObjectAsync(bucketName, objectName, filePath);
*/