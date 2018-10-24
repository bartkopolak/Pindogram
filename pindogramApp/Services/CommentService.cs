using System;
using System.Collections.Generic;
using pindogramApp.Entities;
using pindogramApp.Helpers;
using System.Linq;
using AutoMapper;
using pindogramApp.Services.Interfaces;

namespace pindogramApp.Services
{
    public class CommentService : ICommentService
    {
        private readonly PindogramDataContext _context;

        public CommentService(PindogramDataContext context)
        {
            _context = context;
        }

        public Comment Create(string content, Meme meme, User author)
        {
            Comment comment = new Comment();
            comment.Content = content;
            comment.Author = author;
            comment.Meme = meme;
            comment.DateAdded = DateTime.Now;

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }

        public void Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                throw new AppException($"Nie ma komentarza o takim Id. Metoda: {nameof(Delete)}");
            }
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }

        public Comment Edit(int CommentId, string newContent)
        {
            var comment = _context.Comments.Find(CommentId);
            if (comment == null)
            {
                throw new AppException($"Nie ma komentarza o takim Id. Metoda: {nameof(Edit)}");
            }
            comment.Content = newContent;
            _context.Comments.Update(comment);
            _context.SaveChanges();
            return comment;
        }

        public IEnumerable<Comment> GetAll() => _context.Comments.OrderByDescending(x => x.DateAdded);

        public IEnumerable<Comment> GetAllFromMeme(int MemeId)
        {
            return _context.Comments.Where(x => x.MemeId == MemeId);
        }

        public Comment GetById(int id)
        {
            Comment comment = _context.Comments.FirstOrDefault(x => x.Id == id);
            if (comment == null)
            {
                throw new AppException($"Nie ma komentarza o takim Id. Metoda: {nameof(GetById)}");
            }
            return comment;
        }

        public User GetCommentAuthor(int CommentId)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == CommentId);
            if (comment == null)
            {
                throw new AppException("Nie ma komentarza o takim Id (" + CommentId + ").");
            }
            int authorId = (int)comment.AuthorId;
            User author = _context.Users.FirstOrDefault(x => x.Id == authorId);
            return author;
        }

        public Meme GetCommentMeme(int CommentId)
        {
            var comment = _context.Comments.FirstOrDefault(x => x.Id == CommentId);
            if (comment == null)
            {
                throw new AppException("Nie ma komentarza o takim Id (" + CommentId + ").");
            }
            int memeId = (int)comment.MemeId;
            Meme meme = _context.Memes.FirstOrDefault(x => x.Id == memeId);
            return meme;
        }

        public User GetLoggedUser(string strAutId)
        {
            int autId = int.Parse(strAutId);
            User loggedUser = _context.Users.First(x => x.Id == autId);
            return loggedUser;
        }
    }
}
