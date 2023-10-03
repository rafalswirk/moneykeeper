using Microsoft.AspNetCore.Mvc;
using MoneyKeeper.Budget.Core.Services;
using MoneyKeeper.Budget.Repositories;

namespace MoneyKeeper.Budget.API.Controllers
{
    [ApiController]
    [Route("api/budget/categories")]
    public class BudgetCategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;

        public BudgetCategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseAsync() 
        {
            var categories = await _categoriesService.BrowseAsync();
            return Ok(categories);
        }
    }
}
