using pindogramApp.Entities;
using System.Collections.Generic;



namespace pindogramApp.Services.Interfaces
{
    public interface IMemeService
    {
        Meme Create(string title, User author);
        void Upvote(int memeId, User user);
        void Downvote(int memeId, User user);
        IEnumerable<Meme> GetAll();
        Meme GetById(int id);
        void Delete(int id);
        int GetRate(int memeId);
        User GetLoggedUser(string strAutId);
    }
}
