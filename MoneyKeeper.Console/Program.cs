using System;
using Microsoft.Extensions.Configuration;

using MoneyKeeper.OCR.GCloud;

namespace MoneyKeeper.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            var token = configuration.GetSection("GCloud:AccessToken");
            var projectId = configuration.GetSection("GCloud:ProjectId");

            var imageProvider = new GCloud.ImageProvider();
            var filePath = @"d:\Paragony\IMG_20230418_102305232~2.jpg";
            var jsonFileName = $"gcloud-{Path.GetFileNameWithoutExtension(filePath)}.json";
            var gloudJson = await imageProvider.SendImage(filePath, $"Bearer {token.Value}", projectId.Value);
            File.WriteAllText("gcloud-IMG_20230418_102305232~2.json", gloudJson); //paragon2
            
            var parser = new BillOfSaleParser();
            var result = parser.Parse(File.ReadAllText(jsonFileName));

            System.Console.WriteLine(result.ToString());
            System.Console.WriteLine("Program finished!");
        }
    }
}