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
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentService _commService;
        private readonly IMemeService _memeService;
        private readonly IMapper _mapper;

        public CommentsController(
            ICommentService userService,
            IMemeService memeService,
            IMapper mapper)
        {
            _commService = userService;
            _mapper = mapper;
            _memeService = memeService;
        }

        [HttpPost("[Action]")]
        public IActionResult CreateComment([FromBody]CreateCommentDto createCommDto)
        {
            try
            {
                User loggedUser = _memeService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
                if (loggedUser == null)
                {
                    return BadRequest(new { message = "Użytkownik nie jest zalogowany" });
                }
                if (String.IsNullOrEmpty(createCommDto.Content))
                {
                    return BadRequest(new { message = "Nie można dodać pustego komentarza" });
                }
                // save 
                var meme = _memeService.GetSingleApprovedById(createCommDto.MemeId);
                _commService.Create(createCommDto.Content, meme, loggedUser);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("[Action]")]
        public IActionResult EditComment([FromBody]EditCommentDto editCommentDto)
        {
            try { 
            User loggedUser = _commService.GetLoggedUser(this.User.FindFirst(ClaimTypes.Name).Value);
            if (loggedUser == null)
            {
                return BadRequest(new { message = "Użytkownik nie jest zalogowany" });
            }
            if (String.IsNullOrEmpty(editCommentDto.NewContent))
            {
                return BadRequest(new { message = "Nie można dodać pustego komentarza" });
            }

            var comm = _commService.GetById(editCommentDto.CommentId);

            if(comm == null)
            {
                return BadRequest(new { message = "Komentarz nie istnieje" });
            }
            if (loggedUser != comm.Author && loggedUser.GroupId != 1)
            {
                return BadRequest(new { message = "Brak uprawnień do edycji komentarza" });
            }
                _commService.Edit(editCommentDto.CommentId, editCommentDto.NewContent);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("[Action]/{id}")]
        public IActionResult GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var comment = _commService.GetById(id);
                var commentDto = _mapper.Map<CommentDto>(comment);
                commentDto.MemeId = _commService.GetCommentMeme(id).Id;
                commentDto.Author = _mapper.Map<AuthorDto>(_commService.GetCommentAuthor(id));
                return Ok(commentDto);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("[Action]/{id}")]
        public IEnumerable<CommentDto> GetCommentsFromMeme([FromRoute] int id)
        {
            var comms = _commService.GetAllFromMeme(id);

            var commDto = _mapper.Map<IEnumerable<CommentDto>>(comms);
            foreach (var comm in commDto)
            {
                comm.MemeId = _commService.GetCommentMeme(comm.Id).Id;
                comm.Author = _mapper.Map<AuthorDto>(_commService.GetCommentAuthor(comm.Id));
            }

            return commDto.OrderByDescending(x => x.DateAdded);
        }

        [HttpGet("[Action]")]
        public IEnumerable<CommentDto> GetAllComments()
        {
            var comms = _commService.GetAll();

            var commDto = _mapper.Map<IEnumerable<CommentDto>>(comms);
            foreach (var comm in commDto)
            {
                try
                {
                    comm.MemeId = _commService.GetCommentMeme(comm.Id).Id;
                    comm.Author = _mapper.Map<AuthorDto>(_commService.GetCommentAuthor(comm.Id));
                }
                catch (AppException)
                {

                }
            }

            return commDto.OrderByDescending(x => x.DateAdded);
        }




        [Authorize(Policy = "ADMIN")]
        [HttpDelete("[Action]/{id}")]
        public IActionResult DeleteCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _commService.Delete(id);
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