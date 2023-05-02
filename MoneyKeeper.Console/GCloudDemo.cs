using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console
{
    public class GCloudDemo : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            System.Console.WriteLine("Hello from GCloudDemo!");

            //var configuration = new ConfigurationBuilder()
            //.AddUserSecrets<Program>()
            //.Build();

            //var token = configuration.GetSection("GCloud:AccessToken");
            //var projectId = configuration.GetSection("GCloud:ProjectId");

            //var imageProvider = new GCloud.ImageProvider();
            //var filePath = "";
            //if(args.Length == 0)
            //{
            //    System.Console.WriteLine("No image path provided using hardcoded value!");
            //    filePath = @"d:\Paragony\IMG_20230418_102305232~2.jpg";
            //}
            //else
            //{
            //    filePath = args[0];
            //}
            //var jsonFileName = $"gcloud-{Path.GetFileNameWithoutExtension(filePath)}.json";
            //var gloudJson = await imageProvider.SendImage(filePath, $"Bearer {token.Value}", projectId.Value);
            //File.WriteAllText(jsonFileName, gloudJson); //paragon2

            //var parser = new BillOfSaleParser();
            //var result = parser.Parse(File.ReadAllText(jsonFileName));

            //System.Console.WriteLine(result.ToString());
            //System.Console.WriteLine("Program finished!");

            //next - add value to google spreadsheet

            //var editor = new GoogleDocsEditor();
            //await editor.AddValueToGoogleDocs($"Bearer {token.Value}", projectId.Value);

            //var budgetCategories = new BudgetCategoryRepository(new Budget.DAL.BudgetCategoryDbContext(new DbContextOptions<Budget.DAL.BudgetCategoryDbContext>(), configuration.GetSection("Database:ConnectionString").Value));
            //await budgetCategories.AddAsync(new BudgetCategory
            //{
            //    Category = "Food",
            //    Group = "Groceries",
            //});
            return Task.CompletedTask;
        }
    }
}
