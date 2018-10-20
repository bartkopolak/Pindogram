using System;
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
    [Route("api/[controller]/")]
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

        [HttpPost("[Action]")]
        public IActionResult CreateMeme([FromBody]CreateMemeDto createMemeDto)
        {

            User loggedUser = _memeService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
            if (loggedUser == null)
            {
                return BadRequest(new { message = "Użytkownik nie jest zalogowany" });
            }
            if (String.IsNullOrEmpty(createMemeDto.Title))
            {
                return BadRequest(new { message = "Nie można dodać mema bez tytułu" });
            }
            if (String.IsNullOrEmpty(createMemeDto.Image))
            {
                return BadRequest(new { message = "Nie można dodać mema bez zdjęcia" });
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

        [HttpPost("[Action]")]
        public IActionResult UpvoteMeme(int memeId)
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

        [HttpPost("[Action]")]
        public IActionResult DownvoteMeme(int memeId)
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

        [HttpGet("[Action]")]
        public IEnumerable<MemeDto> GetAllApprovedMemes()
        {
            var memes = _memeService.GetAllApproved();

            var memesDto = _mapper.Map<IEnumerable<MemeDto>>(memes);

            foreach (var meme in memesDto)
            {
                meme.Likes = _memeService.GetRate((int)meme.Id);
                meme.Author = _mapper.Map<AuthorDto>(_memeService.GetMemeAuthor((int)meme.Id));
                meme.ActiveDown =
                    _memeService.IsActiveDown((int)meme.Id, int.Parse(this.User.FindFirst(ClaimTypes.Name).Value));
                meme.ActiveUp =
                    _memeService.IsActiveUp((int)meme.Id, int.Parse(this.User.FindFirst(ClaimTypes.Name).Value));
            }

            return memesDto.OrderByDescending(x => x.DateAdded);
        }

        [Authorize(Policy = "ADMIN")]
        [HttpGet("[Action]")]
        public IEnumerable<MemeDto> GetAllUnapprovedMemes()
        {
            var memes = _memeService.GetAllUnapproved();
            var memesDto = _mapper.Map<IEnumerable<MemeDto>>(memes);

            foreach (var meme in memesDto)
            {
                meme.Likes = _memeService.GetRate((int)meme.Id);
                meme.Author = _mapper.Map<AuthorDto>(_memeService.GetMemeAuthor((int)meme.Id));
                meme.ActiveDown =
                    _memeService.IsActiveDown((int)meme.Id, Int32.Parse(this.User.FindFirst(ClaimTypes.Name).Value));
                meme.ActiveUp =
                    _memeService.IsActiveUp((int)meme.Id, Int32.Parse(this.User.FindFirst(ClaimTypes.Name).Value));
            }

            return memesDto.OrderByDescending(x => x.DateAdded);
        }

        [Authorize(Policy = "ADMIN")]
        [HttpGet("[Action]/{id}")]
        public IActionResult GetSingleUnapprovedMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var meme = _memeService.GetSingleUnapprovedById(id);
                var memeDto = _mapper.Map<MemeDto>(meme);
                memeDto.Likes = _memeService.GetRate(meme.Id);
                memeDto.Author = _mapper.Map<AuthorDto>(_memeService.GetMemeAuthor(meme.Id));
                memeDto.ActiveDown =
                    _memeService.IsActiveDown((int)meme.Id, Int32.Parse(this.User.FindFirst(ClaimTypes.Name).Value));
                memeDto.ActiveUp =
                    _memeService.IsActiveUp((int)meme.Id, Int32.Parse(this.User.FindFirst(ClaimTypes.Name).Value));

                return Ok(memeDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetSingleApprovedMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var meme = _memeService.GetSingleApprovedById(id);
                var memeDto = _mapper.Map<MemeDto>(meme);
                memeDto.Likes = _memeService.GetRate(meme.Id);
                memeDto.Author = _mapper.Map<AuthorDto>(_memeService.GetMemeAuthor(meme.Id));
                memeDto.ActiveDown =
                    _memeService.IsActiveDown((int)meme.Id, Int32.Parse(this.User.FindFirst(ClaimTypes.Name).Value));
                memeDto.ActiveUp =
                    _memeService.IsActiveUp((int)meme.Id, Int32.Parse(this.User.FindFirst(ClaimTypes.Name).Value));

                return Ok(memeDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Policy = "ADMIN")]
        [HttpPost("[Action]/{id}")]
        public IActionResult ApproveMeme([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var memeTitle =_memeService.ApproveMeme(id);
                return Ok(new { message = $"Zaakceptowałeś {memeTitle}" });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetRateOfMemeById(int memeId)
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
        [Authorize(Policy = "ADMIN")]
        [HttpDelete("[Action]/{id}")]
        public IActionResult DeleteMemeById([FromRoute] int id)
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