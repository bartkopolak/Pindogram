using pindogramApp.Entities;
using pindogramApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using pindogramApp.Dtos;
using pindogramApp.Helpers;

namespace pindogramApp.Services
{
    public class MemeService : IMemeService
    {
        private readonly PindogramDataContext _context;

        public MemeService(PindogramDataContext context)
        {
            _context = context;
        }

        public void Downvote(int memeId, User user)
        {
            Meme meme = _context.Memes.Find(memeId);
            if (meme == null)
                throw new AppException($"Nie ma mema o takim Id. Metoda: {nameof(Downvote)}");
            MemeRate rate = _context.MemeRates.FirstOrDefault(x => x.Meme == meme && x.User == user);
            if (rate == null)
                rate = CreateMemeRate(meme, user);
            if (!rate.isUpvote)
            {
                _context.MemeRates.Remove(rate);
            }else
            {
                rate.isUpvote = false;
                _context.MemeRates.Update(rate);
            }
            _context.SaveChanges();
        }

        public void Upvote(int memeId, User user)
        {
            Meme meme = _context.Memes.Find(memeId);
            if( meme == null)
                throw new AppException($"Nie ma mema o takim Id. Metoda: {nameof(Upvote)}");
            MemeRate rate = _context.MemeRates.FirstOrDefault(x => x.Meme == meme && x.User == user);
            if (rate == null)
                rate = CreateMemeRate(meme, user);
            if (rate.isUpvote)
            {
                _context.MemeRates.Remove(rate);
            }
            else
            {
                rate.isUpvote = true;
                _context.MemeRates.Update(rate);
            }

            _context.SaveChanges();
        }

        public Meme Create(CreateMemeDto memeObject, User author)
        {
            Meme meme = new Meme();
            meme.Title = memeObject.Title;
            meme.Image = Convert.FromBase64String(memeObject.Image);
            meme.Author = author;
            meme.DateAdded = DateTime.Now;
            _context.Memes.Add(meme);
            _context.SaveChanges();

            return meme;
        }

        public User GetMemeAuthor(int memeId)
        {
            Meme meme = _context.Memes.FirstOrDefault(x => x.Id == memeId);
            if(meme == null)
            {
                throw new AppException($"Nie ma mema o takim Id. Metoda: {nameof(GetById)}");
            }
            int authorId = meme.AuthorId;
            User author = _context.Users.FirstOrDefault(x => x.Id == authorId);
            return author;
        }

        public int GetRate(int memeId)
        {
            int id = memeId;
            var votes = _context.MemeRates.Where(m => m.MemeId == memeId);
            int upvotedCount = votes.Count(x => x.isUpvote == true);
            int downvotedCount = votes.Count(x => x.isUpvote == false);
            return upvotedCount - downvotedCount;
        }

        public bool IsActiveUp(int memeId, int userId)
        {
            return _context.MemeRates.Any(x => x.MemeId == memeId && x.UserId == userId && x.isUpvote);
        }

        public bool IsActiveDown(int memeId, int userId)
        {
            return _context.MemeRates.Any(x => x.MemeId == memeId && x.UserId == userId && !x.isUpvote);
        }

        //helpers

        private MemeRate CreateMemeRate(Meme meme, User user)
        {
            MemeRate newLike = new MemeRate();
            newLike.Meme = meme;
            newLike.User = user;
            _context.MemeRates.Add(newLike);
            _context.SaveChanges();
            return newLike;
        }

        public IEnumerable<Meme> GetAll() => _context.Memes;

        public Meme GetById(int id)
        {
            var meme = _context.Memes.Find(id);
            if (meme == null)
            {
                throw new AppException($"Nie ma mema o takim Id. Metoda: {nameof(GetById)}");
            }
            return meme;
        }

        public void Delete(int id)
        {
            var meme = _context.Memes.Find(id);
            if (meme == null)
            {
                throw new AppException($"Nie ma mema o takim Id. Metoda: {nameof(Delete)}");
            }
            var rates = _context.MemeRates.Where(x => x.MemeId == id);
            foreach(MemeRate mr in rates)
            {
                _context.MemeRates.Remove(mr);
            }
            
            _context.Memes.Remove(meme);
            _context.SaveChanges();
        }

        public User GetLoggedUser(string strAutId)
        {
            int autId = int.Parse(strAutId);
            User loggedUser = _context.Users.First(x => x.Id == autId);
            return loggedUser;
        }
    }
}
