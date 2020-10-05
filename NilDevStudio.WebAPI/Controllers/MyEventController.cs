using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NilDevStudio.Domain;
using NilDevStudio.Repository;
using AutoMapper;
using System.Collections.Generic;
using NilDevStudio.WebAPI.DTO;
using System.IO;
using System.Net.Http.Headers;
using System.Linq;

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
				var results = _mapper.Map<MyEventDTO[]>(myEvents);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }
        }

		[HttpPost("upload")]
        public async Task<IActionResult> upload()
        {
            try
            {
				var file = Request.Form.Files[0];
				var folderName = Path.Combine("Resources", "Images");
				var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

				if(file.Length > 0)
				{
					var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
					var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

					using(var stream = new FileStream(fullPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}

				return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }

			//return BadRequest("Upload Error");
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
                var myEvents = await _repository.GetAllEventByTheme(theme, true);
				var results = _mapper.Map<MyEventDTO[]>(myEvents);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error occurred in database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MyEventDTO model)
        {
            try
            {
				var myEvent = _mapper.Map<MyEvent>(model);
                _repository.Add(myEvent);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/myEvent/{model.Id}", _mapper.Map<MyEventDTO>(myEvent));
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "[ERROR_DB] Error occurred in database");
            }

            return BadRequest();
        }

        [HttpPut("{MyEventId}")]
        public async Task<IActionResult> Put(int MyEventId, MyEventDTO model)
        {
            try
            {
                var myEvent = await _repository.GetEventById(MyEventId, false);
                if(myEvent == null) return NotFound();

				var idLots = new List<int>();
				var idSocial = new List<int>();

				model.Lots.ForEach(item => idLots.Add(item.Id));
                model.SocialNetworks.ForEach(item => idSocial.Add(item.Id));

				var lots = myEvent.Lots.Where(lot => !idLots.Contains(lot.Id)).ToArray();
				var socialNetworks = myEvent.SocialNetworks.Where(net => !idLots.Contains(net.Id)).ToArray();

                if(lots.Length > 0)
                    _repository.DeleteRange(lots);

                if(socialNetworks.Length > 0)
                    _repository.DeleteRange(socialNetworks);

				_mapper.Map(model, myEvent);

                _repository.Update(myEvent);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/myEvent/{model.Id}", _mapper.Map<MyEventDTO>(myEvent));
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
