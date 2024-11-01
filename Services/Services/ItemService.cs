using AutoMapper;
using Datas.DTOs;
using Datas.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;
        public ItemService(IItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateNewItem(ItemDTO itemDTO)
        {
            var item = _mapper.Map<Item>(itemDTO);// tao moi
            await _repository.CreateNewItem(item);
        }

        public async Task DeleteItemAsync(int Id)
        {
            await _repository.DeleteItemAsync(Id);
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {

            var items = await _repository.GetAllItemsAsync();
            return _mapper.Map<IEnumerable<ItemDTO>>(items);
        }

        public async Task<ItemDTO?> GetItemAsync(int id)
        {
            var item = await _repository.GetItemById(id);
            if (item == null)
            {
                return null; 
            }
            return _mapper.Map<ItemDTO>(item);
        }

        public async Task<bool> UpdateItemAsync(int id, ItemDTO itemDTO)
        {
            var item = await _repository.GetItemById(id);
            if (item == null)
            {
                return false;
            }
            _mapper.Map(itemDTO, item); // update, gan gia tri item = dto
            await _repository.UpdateItemAsync(item);
            return true;
        }
    }
}
