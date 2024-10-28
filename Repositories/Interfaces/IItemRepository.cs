using Datas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task <ItemDTO> GetItemById(int id);
        Task CreateNewItem(ItemDTO itemDTO);
        Task DeleteItemAsync(int Id);
        Task UpdateItemAsync(ItemDTO itemDTO);
    }
}
