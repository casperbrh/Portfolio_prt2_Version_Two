using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/mark")]
    [ApiController]
    public class MarksController : Controller
    {
        DataService _dataService;
        public MarksController(DataService dataService)
        {
            _dataService = dataService;
        }


        [HttpPost]
        public IActionResult PostMarks([FromBody]Mark mark)
        {

            _dataService.CreateMarking(mark.PostId, mark.UserId);

            return Created($"api/mark/{mark}", mark);

        }


        [HttpDelete("userid")]
        public IActionResult DeleteMarks(int postid, int userid)
        {
            var del = _dataService.DeleteMarking(postid, userid);

            if (!del)
            {
                return NotFound();
            }
            return Ok(del);
        }

    }


}