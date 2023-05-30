using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MoneyKeeper.Budget.Core.Services.GCloud;
using MoneyKeeper.Budget.DAL.Repositories;
using MoneyKeeper.Budget.Entities;
using MoneyKeeper.Budget.Repositories;
using MoneyKeeper.Console.GCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.Console
{
    public class GCloudDemo
    {
        private readonly IBudgetCategoryRepository _budgetCategory;
        private readonly ICategorySpreadsheetMapRepository _spreadsheetMapRepository;
        private readonly IConfiguration _configuration;

        public GCloudDemo(IBudgetCategoryRepository budgetCategory, ICategorySpreadsheetMapRepository spreadsheetMapRepository, IConfiguration configuration)
        {
            _budgetCategory = budgetCategory;
            _spreadsheetMapRepository = spreadsheetMapRepository;
            _configuration = configuration;
        }
        public async Task Run()
        {
            System.Console.WriteLine("Hello from GCloudDemo!");

            //var configuration = new ConfigurationBuilder()
            //.AddUserSecrets<Program>()
            //.Build();

            var token = _configuration.GetSection("GCloud:AccessToken");
            var projectId = _configuration.GetSection("GCloud:ProjectId");

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

            var editor = new GoogleDocsEditor(new SpreadsheetDataEditor());

            await editor.AddValueToGoogleDocsAsync("Styczeń", "58", "I", "7");
            var spreadsheetSettings = new Budget.Core.Data.SpreadsheetSettings("Wzorzec kategorii", 79);
            var categoriesGenerator = new BudgetCategoriesGenerator(editor, spreadsheetSettings);
            var categories = await categoriesGenerator.GenerateAsync("fooo");

            //await editor.AddValueToGoogleDocs($"Bearer {token.Value}", projectId.Value);

            //var budgetCategories = new BudgetCategoryRepository(new Budget.DAL.BudgetCategoryDbContext(new DbContextOptions<Budget.DAL.BudgetCategoryDbContext>(), configuration.GetSection("Database:ConnectionString").Value));
            foreach (var category in categories)
            {
                await _budgetCategory.AddAsync(category);
            }
            var rawData = await editor.GetValuesRangeAsync(spreadsheetSettings.CategorySheetName, "B35:B177");


            var positionGenerator = new BudgetCategoryPositionGenerator();
            var positions = positionGenerator.Generate(categories, rawData.ToList(), spreadsheetSettings.CategoryOffset);

            foreach (var position in positions)
            {
                await _spreadsheetMapRepository.AddAsync(position);
            }
        }
    }
}
