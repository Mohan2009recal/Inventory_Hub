using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Shared.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be >= 0.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be > 0.")]
        public decimal Price { get; set; }

        public string Status { get; set; } = "In Stock";
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public int? Count { get; set; }

        public ApiResponse() { }

        public ApiResponse(bool success, string message, T? data = default, int? count = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Count = count;
        }
    }
}