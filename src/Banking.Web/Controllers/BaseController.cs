using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Banking.Core.Aggregate.Entities;
using Banking.Core.Interfaces.Repositories;
using Banking.Core.Utils;

namespace Banking.Web.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    [Authorize]
    public class BaseController<T> : ControllerBase where T : EntityBase
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseController(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<T>> GetAllItems()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetItemById(int id)
        {
            var item = await _repository.Get(id);
            if (item == null) return NotFound(MessageConstant.ItemNotFound(typeof(T)));
            return item;
        }

        // Implemented only in provided child controllers 
        // [HttpPost]
        // public virtual async Task<ActionResult> CreateItem(T item)

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                int? idResult = await _repository.Delete(id);
                if (idResult == null) {return NotFound(MessageConstant.ItemDeleted(typeof(T),id));}
                return Ok(new { message = MessageConstant.ItemDeleted(typeof(T),id),
                                id = id });
            }
            catch
            {
                return UnprocessableEntity(MessageConstant.ItemNotDeleted(typeof(T),id));
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> UpdateItem(int id, T item)
        {
            if (id != item.Id) return BadRequest(MessageConstant.ItemIdError(typeof(T),id,item.Id));
            try
            {
                int? idResult = await _repository.Update(item);
                if (idResult == null) {return NotFound(MessageConstant.ItemNotFound(typeof(T)));}
                return Ok(new { message = MessageConstant.ItemUpdated(typeof(T),id),
                                id = id });
            }
            catch
            {
                return UnprocessableEntity(MessageConstant.ItemNotUpdated(typeof(T),id));
            }
        }
    }
}