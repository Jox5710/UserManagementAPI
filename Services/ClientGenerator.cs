using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UserManagementApi.Services
{
    public class ClientGenerator
    {
        public async Task GenerateClient()
        {
            using var httpClient = new HttpClient();
            var swaggerJson = await httpClient.GetStringAsync("http://localhost:5000/swagger/v1/swagger.json");

            var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = "GeneratedApiClient",
                CSharpGeneratorSettings = { Namespace = "UserManagementApi.Client" }
            };

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();

            await File.WriteAllTextAsync("GeneratedApiClient.cs", code);
            Console.WriteLine("Client code generated successfully!");
        }
    }
}
