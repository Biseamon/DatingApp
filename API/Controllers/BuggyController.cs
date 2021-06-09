using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _dataContext;
        public BuggyController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _dataContext.Users.Find(-1);

            if(thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {

            //try{

                var thing = _dataContext.Users.Find(-1);

                var thingToReturn = thing.ToString();

                return thingToReturn;

            // }catch(Exception e){
            //     return StatusCode(500, "Computer says no!" + e);
            // }
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()    
        {
            return BadRequest();
        }
    }
}