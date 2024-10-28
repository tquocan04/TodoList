using Datas.DTOs;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }
        public async Task CreateNewItem(ItemDTO itemDTO)
        {
            await _repository.CreateNewItem(itemDTO);
        }

        public async Task DeleteItemAsync(int Id)
        {
            await _repository.DeleteItemAsync(Id);
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task<ItemDTO> GetItemAsync(int id)
        {
            return await _repository.GetItemById(id);
        }

        public async Task UpdateItemAsync(ItemDTO itemDTO)
        {
            await _repository.UpdateItemAsync(itemDTO);
        }
    }
}
