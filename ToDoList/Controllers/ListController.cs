using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        public ListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            var items = await _listService.GetListItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(int id)
        {
            var item = await _listService.GetListItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List>> CreateList(List item)
        {
            var createdItem = await _listService.CreateListItemAsync(item);
            return CreatedAtAction(nameof(GetList), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, List item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            await _listService.UpdateListItemAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            await _listService.DeleteListItemAsync(id);
            return NoContent();
        }

    }
}
