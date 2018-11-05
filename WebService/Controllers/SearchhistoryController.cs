using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/searchhistory")]
    [ApiController]
    public class SearchhistoryController : Controller
    {
        DataService _dataService;
        public SearchhistoryController(DataService dataService)
        {
            _dataService = dataService;
        }


        [HttpGet("{userid}")]
        public IActionResult GetSearchhistory(int userid)
        {
            var searchh = _dataService.SearchHistories(userid);
            if (searchh == null) return NotFound();
            return Ok(searchh);
        }

        [HttpDelete("{userid}")]
        public IActionResult DeleteSearchhistory(int userid)
        {
            var Searchh = _dataService.SearchHistories(userid);
            if (Searchh == null) return NotFound();
            return Ok(Searchh);
        }
    }
}