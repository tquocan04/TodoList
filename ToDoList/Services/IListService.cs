using ToDoList.Models;
namespace ToDoList.Services
{
    public interface IListService
    {
        Task<IEnumerable<List>> GetListItemsAsync();
        Task<List?> GetListItemByIdAsync(int id);
        Task CreateListItemAsync(List list);
        Task UpdateListItemAsync(List list);
        Task DeleteListItemAsync(int id);
    }
}
