using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.API.Attributes;
using NzWalks.API.Dtos.Region;
using NzWalks.API.Services.Interfaces;
using NzWalks.API.Utils;
using NzWalks.API.Utils.Pagination;
using NzWalks.MODEL;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public RegionController(IRegionRepository regionRepository, IMapper mapper, IUriService uriService)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] RegionToCreateDto regionToCreateDto)
        {
            var regionDomain = _mapper.Map<Region>(regionToCreateDto);

            await _regionRepository.CreateAsync(regionDomain);

            return Ok(_mapper.Map<RegionToDisplayDto>(regionDomain));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter paginationFilter)
        {
            var route = Request.Path.Value;

            var regionDomain = await _regionRepository.GetAllAsync(paginationFilter);

            var totalRecords = await _regionRepository.GetTotalCountAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse<Region>(regionDomain, paginationFilter, totalRecords, _uriService, route);

            return Ok(pagedResponse);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionToDisplayDto>(region));
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] RegionToUpdateDto regionToUpdateDto)
        {

            var regionDomain = _mapper.Map<Region>(regionToUpdateDto);

            await _regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionToDisplayDto>(regionDomain));


        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute,] long id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionToDisplayDto>(regionDomain));
        }

    }
}
