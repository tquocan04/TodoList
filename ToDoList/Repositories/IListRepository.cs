using ToDoList.Models;
namespace ToDoList.Repositories
{
    public interface IListRepository
    {
        Task<IEnumerable<List>> GetAllAsync();

        Task<List?> GetByIdAsync(int id);

        Task AddAsync(List item);

        Task UpdateAsync(List item);

        Task DeleteAsync(int id);
    }
}
