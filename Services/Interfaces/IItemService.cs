using Datas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAllItemsAsync();
        Task<ItemDTO> GetItemAsync(int id);
        Task CreateNewItem(ItemDTO itemDTO);
        Task DeleteItemAsync(int Id);
        Task UpdateItemAsync(ItemDTO itemDTO);
    }
}
