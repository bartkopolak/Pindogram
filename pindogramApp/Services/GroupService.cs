using System.Linq;
using pindogramApp.Entities;
using pindogramApp.Services.Interfaces;

namespace pindogramApp.Services
{
    public class GroupService : IGroupService
    {
        private readonly PindogramDataContext _context;

        public GroupService(PindogramDataContext context)
        {
            _context = context;
        }

        public Group GetById(int id)
        {
            return _context.Groups.FirstOrDefault(x=> x.Id == id);
        }
    }
}
