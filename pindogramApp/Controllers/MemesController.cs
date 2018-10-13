using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pindogramApp.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;
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
            _memeService = MemeService;
        }

        [HttpPost("createMeme")]
        public async Task<IActionResult> Create(string title)
        {

            User loggedUser = _memeService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
            if (loggedUser == null)
            {
                return BadRequest(new { message = "Not logged in" });
            }
            if (String.IsNullOrEmpty(title))
            {
                return BadRequest(new { message = "Cannot add untitled meme" });
            }
            try
            {
                // save 
                _memeService.Create(title, loggedUser);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("upvoteMeme")]
        public async Task<IActionResult> Upvote(int memeId)
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
        public async Task<IActionResult> Downvote(int memeId)
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
        public IEnumerable<Meme> GetMemes()
        {
            return _memeService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var meme = _memeService.GetById(id);
                return Ok(meme);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
           
        }

        [AllowAnonymous]
        [HttpGet("getMemeRate")]
        public async Task<IActionResult> GetRate(int memeId)
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
        public async Task<IActionResult> DeleteMeme([FromRoute] int id)
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