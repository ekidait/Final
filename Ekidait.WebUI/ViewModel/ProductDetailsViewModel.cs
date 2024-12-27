using Ekidait.Core.Entities;

namespace Ekidait.WebUI.Models
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product>? RelateProducts { get; set; }
    }
}
