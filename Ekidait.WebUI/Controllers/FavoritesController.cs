using Ekidait.Core.Entities;
using Ekidait.Service.Abstract;
using Ekidait.WebUI.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;

namespace Ekidait.WebUI.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IService<Product> _service;

        public FavoritesController(IService<Product> service)
        {
            _service = service;
        }

        // Favorileri listele
        public IActionResult Index()
        {
            var favoriler = GetFavorites();
            return View(favoriler);
        }

        // Favorilere ürün ekle
        [HttpPost]
        public IActionResult Add(int ProductId)
        {
            var favoriler = GetFavorites();
            var product = _service.Find(ProductId);
            if (product != null && !favoriler.Any(p => p.Id == ProductId))
            {
                favoriler.Add(product);
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }

            // Favorilere ekleme işlemi başarılı olduğunda, "Favorilerim" sayfasına yönlendirme
            return RedirectToAction("Index", "Favorites"); // "Index" metoduna yönlendir
        }


        // Favorilerden ürün çıkar
        [HttpPost]
        public IActionResult Remove(int ProductId)
        {
            var favoriler = GetFavorites();
            var product = _service.Find(ProductId);
            if (product != null && favoriler.Any(p => p.Id == ProductId))
            {
                favoriler.RemoveAll(i => i.Id == product.Id);
                HttpContext.Session.SetJson("GetFavorites", favoriler);
            }

            // AJAX yanıtı olarak favori listesi döndür
            return Json(new { success = true, message = "Ürün favorilerden çıkarıldı.", data = favoriler });
        }

        // Favorileri almak
        private List<Product> GetFavorites()
        {
            return HttpContext.Session.GetJson<List<Product>>("GetFavorites") ?? new List<Product>();
        }
    }
}
