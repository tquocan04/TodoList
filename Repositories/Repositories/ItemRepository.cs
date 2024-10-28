using AutoMapper;
using Datas;
using Datas.DTOs;
using Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        public ItemRepository(Context context)
        {
            _context = context;
        }

        public async Task CreateNewItem(Item item)
        {
            await _context.Items.AddAsync(item);
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

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            Console.WriteLine($"REPO: {item.Id}");
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Item?> GetItemById(int id)
        {
            return await _context.Items.FindAsync(id);
        }
    }
}
