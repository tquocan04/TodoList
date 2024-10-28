using AutoMapper;
using Datas;
using Datas.DTOs;
using Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public ItemRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateNewItem(ItemDTO itemDTO)
        {
            var newItem = _mapper.Map<Item>(itemDTO);
            await _context.Items.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int Id)
        {
            var item = await _context.Items.FindAsync(Id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ItemDTO>> GetAllItemsAsync()
        {
            var items = await _context.Items.ToListAsync();
            return  _mapper.Map<IEnumerable<ItemDTO>>(items);
        }

        public async Task UpdateItemAsync(ItemDTO itemDTO)
        {
            var item = _mapper.Map<Item>(itemDTO);
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ItemDTO> GetItemById(int id)
        {
            var item = await _context.Items.FindAsync(id);
            return _mapper.Map<ItemDTO>(item);
        }
    }
}
