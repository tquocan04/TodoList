using Datas.DTOs;
using Datas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task <Item?> GetItemById(int id);
        Task CreateNewItem(Item item);
        Task DeleteItemAsync(int Id);
        Task UpdateItemAsync(Item item);
    }
}
