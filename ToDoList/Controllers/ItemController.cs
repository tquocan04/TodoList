using AutoMapper;
using Datas.DTOs;
using Datas.Entities;
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
        public async Task<ActionResult<ItemDTO>> CreateNewItem(ItemDTO itemDTO)
        {
            await _itemService.CreateNewItem(itemDTO);
            return CreatedAtAction(nameof(GetItemById), itemDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, ItemDTO itemDTO)
        {
            var item = _mapper.Map<Item>(itemDTO);
            if (id != item.Id)
            {
                return BadRequest();
            }

            await _itemService.UpdateItemAsync(itemDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
