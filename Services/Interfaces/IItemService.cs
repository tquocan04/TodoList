using Datas.DTOs;

namespace Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO?> GetItemAsync(int id);
        Task CreateNewItem(ItemDTO itemDTO);
        Task DeleteItemAsync(int Id);
        Task<bool> UpdateItemAsync(int id, ItemDTO item);
    }
}
