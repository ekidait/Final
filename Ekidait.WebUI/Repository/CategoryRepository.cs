using Ekidait.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Ekidait.WebUI.Repository
{
    public class CategoryRepository
    {
        private readonly IHubContext<GeneralHub> _hubContext;

        public CategoryRepository(IHubContext<GeneralHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AddCategory(string categoryName)
        {
            // Veritabanına kategori ekleme işlemleri burada yapılabilir
            await _hubContext.Clients.All.SendAsync("onCategoryAdd", $"Kategori '{categoryName}' eklendi.");
        }

        public async Task UpdateCategory(string categoryName)
        {
            // Kategori güncelleme işlemleri
            await _hubContext.Clients.All.SendAsync("onCategoryUpdate", $"Kategori '{categoryName}' güncellendi.");
        }

        public async Task DeleteCategory(string categoryName)
        {
            // Kategori silme işlemleri
            await _hubContext.Clients.All.SendAsync("onCategoryDelete", $"Kategori '{categoryName}' silindi.");
        }
    }
}