using Microsoft.AspNetCore.Mvc;
using InventoryHub.Shared.Models;

namespace InventoryHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private static readonly List<InventoryItem> Inventory = new()
        {
            new InventoryItem { Id = 1, Name = "Surface Pro 9", Category = "Electronics", Quantity = 10, Price = 1199.99m, Status = "In Stock" },
            new InventoryItem { Id = 2, Name = "Ergonomic Desk", Category = "Furniture", Quantity = 3, Price = 450.00m, Status = "Low Stock" },
            new InventoryItem { Id = 3, Name = "Microsoft Mouse", Category = "Electronics", Quantity = 0, Price = 29.99m, Status = "Out of Stock" }
        };

        [HttpGet]
        public ActionResult<ApiResponse<List<InventoryItem>>> Get([FromQuery] string? category, [FromQuery] string? search)
        {
            var query = Inventory.AsQueryable();

            if (!string.IsNullOrEmpty(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(i => i.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(i => i.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            var result = query.ToList();
            return Ok(new ApiResponse<List<InventoryItem>>(true, "Success", result, result.Count));
        }

        [HttpPost]
        public ActionResult<ApiResponse<InventoryItem>> Post([FromBody] InventoryItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<InventoryItem>(false, "Invalid item payload"));
            }

            item.Status = item.Quantity switch
            {
                0 => "Out of Stock",
                <= 5 => "Low Stock",
                _ => "In Stock"
            };

            item.Id = Inventory.Count > 0 ? Inventory.Max(i => i.Id) + 1 : 1;
            Inventory.Add(item);

            return Ok(new ApiResponse<InventoryItem>(true, "Item added successfully", item));
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<InventoryItem>> Delete(int id)
        {
            var item = Inventory.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound(new ApiResponse<InventoryItem>(false, "Item not found"));
            }

            Inventory.Remove(item);
            return Ok(new ApiResponse<InventoryItem>(true, "Item deleted successfully", item));
        }
    }
}