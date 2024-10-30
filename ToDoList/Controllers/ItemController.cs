using AutoMapper;
using Datas.DTOs;
using Datas.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemById(int id)
        {
            var item = await _itemService.GetItemAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ItemDTO>> CreateNewItem(ItemDTO itemDTO)
        {
            await _itemService.CreateNewItem(itemDTO);
            var item = _mapper.Map<Item>(itemDTO);
            return CreatedAtAction(nameof(GetItemById), new { id = item .Id}, itemDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, ItemDTO itemDTO)
        {
            var result = await _itemService.UpdateItemAsync(id, itemDTO);
            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var item = await _itemService.GetItemAsync(id);
            if (item != null)
            {
                await _itemService.DeleteItemAsync(id);
                return NoContent();
            }
            
            return NotFound(id);
        }
    }
}
