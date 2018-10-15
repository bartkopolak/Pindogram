using Microsoft.AspNetCore.Mvc;
using pindogramApp.Entities;
using pindogramApp.Services.Interfaces;


namespace pindogramApp.Controllers
{
    /// <summary>
    /// Group controller
    /// </summary>
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
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
            return _groupService.GetById(id);
        }

    }
}