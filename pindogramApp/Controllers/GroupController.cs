using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pindogramApp.Entities;


namespace pindogramApp.Controllers
{
    /// <summary>
    /// Group controller
    /// </summary>
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly PindogramDataContext _pindogramDataContext;

        public GroupController(PindogramDataContext pindogramDataContext)
        {
            _pindogramDataContext = pindogramDataContext;
        }

        /// <summary>
        /// Gets single group by Id
        /// </summary>
        /// <param name="id">Id of group</param>
        /// <returns></returns>
        //GET api/Group/GetSingle/$
        [HttpGet]
        [Route("/api/[controller]/GetSingle/{id}")]
        public Group GetSingle(int id)
        {
            return _pindogramDataContext.Groups.FirstOrDefault(x=> x.Id == id);
        }

        /// <summary>
        /// Get all groups
        /// </summary>
        /// <returns></returns>
        //GET api/Group/GetAll
        [HttpGet]
        [Route("/api/[controller]/GetAll")]
        public IEnumerable<Group> GetAll()
        {
            return _pindogramDataContext.Groups;
        }
    }
}