using Ekidait.Core.Entities;

namespace Ekidait.WebUI.ViewModel
{
    public class CartViewModel
    {
        public List<CartLine>? CartLines { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
