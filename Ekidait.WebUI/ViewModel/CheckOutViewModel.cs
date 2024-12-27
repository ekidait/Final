using Ekidait.Core.Entities;

namespace Ekidait.WebUI.ViewModel
{
    public class CheckOutViewModel
    {
        public List<CartLine>? CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Address>? Addresses { get; set; }
    }
}
