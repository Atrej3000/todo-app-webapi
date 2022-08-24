using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TodoAPI.Models;
using TodoAPI.Repositories;
using TodoAPI.Services;
using TodoAPI.DTO;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _service;
        private readonly IMapper _mapper;
        public CardsController(ICardService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDTO>>> GetCards()
        {
            var cards = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CardDTO>>(cards));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDTO>> GetCard(int id)
        {
            var card = await _service.GetOneAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CardDTO>(card));
        }
        [HttpPost]
        public async Task<ActionResult<CardDTO>> PostCard([FromBody] CardDTO cardDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var card = _mapper.Map<Card>(cardDTO);
            var cardResult = await _service.AddAsync(card);
            if(cardResult == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<CardDTO>(cardResult));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CardDTO>> PutCard(int id, [FromBody] CardDTO cardDTO)
        {
            if (id != cardDTO.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _service.UpdateAsync(_mapper.Map<Card>(cardDTO));
            
            return Ok(cardDTO);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _service.GetOneAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(card.Id);

            return Ok();
        }
    }
}
