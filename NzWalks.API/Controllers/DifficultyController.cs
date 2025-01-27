using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Attributes;
using NzWalks.API.Dtos.Difficulty;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper, IUriService uriService)
        {
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] DifficultyToCreateDto difficultyToCreateDto)
        {
            var difficultyDomain = _mapper.Map<Difficulty>(difficultyToCreateDto);

            await _difficultyRepository.CreateAsync(difficultyDomain);

            return Ok(_mapper.Map<DiffIcultyToDisplayDto>(difficultyDomain));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;

            var difficultyDomain = await _difficultyRepository.GetAllAsync(paginationFilter);

            var totalRecords = await _difficultyRepository.GetTotalCountAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse<Difficulty>(difficultyDomain, paginationFilter, totalRecords, _uriService, route);

            return Ok(pagedResponse);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var difficulty = await _difficultyRepository.GetByIdAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DiffIcultyToDisplayDto>(difficulty));
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] DifficultyToUpdateDto difficultyToUpdateDto)
        {

            var difficultyDomain = _mapper.Map<Difficulty>(difficultyToUpdateDto);

            await _difficultyRepository.UpdateAsync(id, difficultyDomain);

            if (difficultyDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DiffIcultyToDisplayDto>(difficultyDomain));


        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute,] long id)
        {
            var difficultyDomain = await _difficultyRepository.DeleteAsync(id);

            if (difficultyDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DiffIcultyToDisplayDto>(difficultyDomain));
        }

    }
}
