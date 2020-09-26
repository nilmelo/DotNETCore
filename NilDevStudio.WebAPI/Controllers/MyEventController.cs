using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NilDevStudio.Domain;
using NilDevStudio.Repository;

namespace NilDevStudio.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEventController : ControllerBase
    {
        private readonly INilDevRepository _repository;

        public MyEventController(INilDevRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllEvent(true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }
        }

        [HttpGet("{EventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            try
            {
                var results = await _repository.GetEventById(eventId, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }
        }

        [HttpGet("getByTheme/{theme}")]
        public async Task<IActionResult> Get(string theme)
        {
            try
            {
                var results = await _repository.GetAllEventByTheme(theme, true);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MyEvent model)
        {
            try
            {
                _repository.Add(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/myEvent/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "[ERROR_DB] Error occurred in database");
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(int eventId, MyEvent model)
        {
            try
            {
                var myEvent = await _repository.GetEventById(eventId, false);

                if(myEvent == null) return NotFound();

                _repository.Update(model);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/myEvent/{model.Id}", model);
                }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int eventId)
        {
            try
            {
                var myEvent = await _repository.GetEventById(eventId, false);

                if(myEvent == null) return NotFound();

                _repository.Delete(myEvent);

                if(await _repository.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }

            return BadRequest();
        }

    }
}
