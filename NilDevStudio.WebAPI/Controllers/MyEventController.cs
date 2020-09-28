using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NilDevStudio.Domain;
using NilDevStudio.Repository;
using AutoMapper;
using System.Collections.Generic;
using NilDevStudio.WebAPI.DTO;

namespace NilDevStudio.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyEventController : ControllerBase
    {
        private readonly INilDevRepository _repository;
        private readonly IMapper _mapper;

        public MyEventController(INilDevRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var myEvents = await _repository.GetAllEvent(true);
				var results = _mapper.Map<IEnumerable<MyEventDTO>>(myEvents);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }
        }

        [HttpGet("{MyEventId}")]
        public async Task<IActionResult> Get(int MyEventId)
        {
            try
            {
                var myEvent = await _repository.GetEventById(MyEventId, true);
				var results = _mapper.Map<MyEventDTO>(myEvent);
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

        [HttpPut("{MyEventId}")]
        public async Task<IActionResult> Put(int MyEventId, MyEvent model)
        {
            try
            {
                var myEvent = await _repository.GetEventById(MyEventId, false);

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

        [HttpDelete("{MyEventId}")]
        public async Task<IActionResult> Delete(int MyEventId)
        {
            try
            {
                var myEvent = await _repository.GetEventById(MyEventId, false);

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
