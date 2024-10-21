using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using ToDoList.Repositories;

namespace ToDoList.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;
        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public async Task<IEnumerable<List>> GetListItemsAsync()
        {
            return await _listRepository.GetAllAsync();
        }
        public async Task<List?> GetListItemByIdAsync(int id)
        {
            return await _listRepository.GetByIdAsync(id);
        }
        public async Task CreateListItemAsync(List item)
        {
            await _listRepository.AddAsync(item);
        }

        public async Task UpdateListItemAsync(List item)
        {
            await _listRepository.UpdateAsync(item);
        }

        public async Task DeleteListItemAsync(int id)
        {
            await _listRepository.DeleteAsync(id);
        }
    }
}
