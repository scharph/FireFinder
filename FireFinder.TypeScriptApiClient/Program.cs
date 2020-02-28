using System;
using System.IO;
using System.Threading.Tasks;
using NSwag;
using NSwag.CodeGeneration.TypeScript;

namespace FireFinder.TypeScriptApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var document = OpenApiDocument.FromUrlAsync("https://firefinder.azurewebsites.net/swagger/v1/swagger.json");
            //var document = OpenApiDocument.FromUrlAsync("https://localhost:5000/swagger/v1/swagger.json");

            var settings = new TypeScriptClientGeneratorSettings
            {
                ClassName = "{controller}Client",
                HttpClass = HttpClass.HttpClient,
                InjectionTokenType = InjectionTokenType.InjectionToken,
                ImportRequiredTypes = true,
                Template = TypeScriptTemplate.Angular,
                GenerateClientClasses = true,
                GenerateClientInterfaces = true
            };

            var generator = new TypeScriptClientGenerator(await document, settings);
            var code = generator.GenerateFile();

            string path = "firefinder-api.service.ts";

            using (StreamWriter sw = File.CreateText(path))
                sw.WriteLine(code);

            Console.WriteLine("Done");

        }
    }
}
