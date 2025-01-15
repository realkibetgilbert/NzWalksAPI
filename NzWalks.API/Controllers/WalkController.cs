using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Attributes;
using NzWalks.API.Dtos.Walks;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IwalkRepository _iwalkRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public WalkController(IwalkRepository iwalkRepository, IMapper mapper, IUriService uriService)
        {
            _iwalkRepository = iwalkRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] WalkToCreateDto walkToCreateDto)
        {
            var walkDomain = _mapper.Map<Walk>(walkToCreateDto);

            await _iwalkRepository.CreateAsync(walkDomain);

            return Ok(_mapper.Map<WalkToDisplayDto>(walkDomain));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;

            var walkDomain = await _iwalkRepository.GetAllAsync(paginationFilter);

            var totalRecords = await _iwalkRepository.GetTotalCountAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse<Walk>(walkDomain, paginationFilter, totalRecords, _uriService, route);

            return Ok(pagedResponse);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var walk = await _iwalkRepository.GetByIdAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkToDisplayDto>(walk));
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] WalkToUpdateDto walkToUpdateDto)
        {

            var walkDomain = _mapper.Map<Walk>(walkToUpdateDto);

            await _iwalkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkToDisplayDto>(walkDomain));


        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute,] long id)
        {
            var walkDomain = await _iwalkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkToDisplayDto>(walkDomain));
        }

    }
}
