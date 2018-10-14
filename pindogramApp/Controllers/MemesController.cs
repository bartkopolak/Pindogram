using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pindogramApp.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
using pindogramApp.Dtos;
using pindogramApp.Services.Interfaces;
using pindogramApp.Helpers;

namespace pindogramApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MemesController : ControllerBase
    {
        private readonly IMemeService _memeService;
        private readonly IMapper _mapper;

        public MemesController(IMapper mapper, IMemeService MemeService)
        {
            _mapper = mapper;
            _memeService = MemeService;
        }

        [HttpPost("createMeme")]
        public IActionResult Create([FromBody]CreateMemeDto createMemeDto)
        {

            User loggedUser = _memeService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
            if (loggedUser == null)
            {
                return BadRequest(new { message = "Not logged in" });
            }
            if (String.IsNullOrEmpty(createMemeDto.Title))
            {
                return BadRequest(new { message = "Cannot add untitled meme" });
            }
            if (String.IsNullOrEmpty(createMemeDto.Image))
            {
                return BadRequest(new { message = "Cannot create meme without image" });
            }
            try
            {
                // save 
                _memeService.Create(createMemeDto, loggedUser);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("upvoteMeme")]
        public IActionResult Upvote(int memeId)
        {
            User loggedUser = _memeService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
            try
            {
                _memeService.Upvote(memeId, loggedUser);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("downvoteMeme")]
        public IActionResult Downvote(int memeId)
        {
            User loggedUser = _memeService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
            try
            {
                _memeService.Downvote(memeId, loggedUser);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<MemeDto> GetMemes()
        {
            var memes = _memeService.GetAll();
            var memesDto = _mapper.Map<IEnumerable<MemeDto>>(memes);

            foreach (var meme in memesDto)
            {
                meme.Likes = _memeService.GetRate((int) meme.Id);
            }

            return memesDto;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var meme = _memeService.GetById(id);
                var memeDto = _mapper.Map<MemeDto>(meme);
                memeDto.Likes = _memeService.GetRate(meme.Id);

                return Ok(memeDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpGet("getMemeRate")]
        public IActionResult GetRate(int memeId)
        {
            try
            {
                int rate = 0;
                rate = _memeService.GetRate(memeId);
                return Ok(rate);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Memes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _memeService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}