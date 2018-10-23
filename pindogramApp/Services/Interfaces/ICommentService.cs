using System.Collections.Generic;
using pindogramApp.Dtos;
using pindogramApp.Entities;


namespace pindogramApp.Services.Interfaces
{
    public interface ICommentService
    {
        Comment Create(string content, Meme meme, User author);
        Comment GetById(int id);
        IEnumerable<Comment> GetAll();
        IEnumerable<Comment> GetAllFromMeme(int MemeId);
        Comment Edit(int CommentId, string newContent);
        void Delete(int id);
        User GetLoggedUser(string strAutId);
        User GetCommentAuthor(int CommentId);
        Meme GetCommentMeme(int CommentId);
    }
}
