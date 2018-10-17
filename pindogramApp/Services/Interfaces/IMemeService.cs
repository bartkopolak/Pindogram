using pindogramApp.Entities;
using System.Collections.Generic;
using pindogramApp.Dtos;


namespace pindogramApp.Services.Interfaces
{
    public interface IMemeService
    {
        Meme Create(CreateMemeDto title, User author);
        void Upvote(int memeId, User user);
        void Downvote(int memeId, User user);
        User GetMemeAuthor(int memeId);
        IEnumerable<Meme> GetAll();
        Meme GetById(int id);
        void Delete(int id);
        int GetRate(int memeId);
        User GetLoggedUser(string strAutId);
    }
}
