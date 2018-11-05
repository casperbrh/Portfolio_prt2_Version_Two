using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    //[Authorize]
    [Route("api/answers")]
    [ApiController]
    public class AnswersController : Controller
    {
        DataService _dataService;
        public AnswersController(DataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public IActionResult GetAnswers()
        {
            var answers = _dataService.GetAnswers();
            return Ok(answers);
        }
        
        [HttpGet("acceptedanswer/{id}", Name = nameof(GetAnswer))]
        public IActionResult GetAnswer(int id)
        {
            var answer = _dataService.GetAnswer(id);
            if (answer == null) return NotFound();
            var result = new
            {
                Link = Url.Link(nameof(GetAnswersByParent), new { answer.Id }),
                answer.CreationDate,
                answer.Score,
                answer.Body,
                Comments = Url.Link(nameof(GetCommentsByAnswer), new { id = answer.Id})
            };
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetAnswersByParent))]
        public IActionResult GetAnswersByParent(int id)
        {
            var answerbyparent = _dataService.GetAnswersByParent(id);
            
           /* if (answerbyparent.Count == 0) return NotFound();
            //for loop
            foreach (var item in answerbyparent)
            {
                //data tranfer object with url variable
                item.
                Url.Link(nameof(GetCommentsByAnswer), new { id = item.Id });
            }
            var result = new
            {
                items = answerbyparent
            
                
            };*/
           



            return Ok(answerbyparent);

        }

        [HttpGet("comments/{id}", Name = nameof(GetCommentsByAnswer))]
        public IActionResult GetCommentsByAnswer(int id)
        {
            var commentanswer = _dataService.GetCommentsByAnswer(id);
            if (commentanswer.Count == 0) return NotFound();
            return Ok(commentanswer);
        }
       
    }
}