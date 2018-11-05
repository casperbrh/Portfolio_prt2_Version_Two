using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionsController : Controller
    {
        DataService _dataService;
        public QuestionsController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpGet(Name =nameof(GetQuestions))]
        public IActionResult GetQuestions(int page = 0, int pageSize = 10)
        {
            var questions = _dataService.GetQuestions(page, pageSize)
                .Select(x => new
                {
                    Link =Url.Link(
                        nameof(GetQuestion),
                        new {x.Id}),
                        x.Title
                    
                });
            var total = _dataService.GetNumberOfQuestions();
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 ? Url.Link(nameof(GetQuestions), new { page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetQuestions), new { page = page + 1, pageSize }) : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = questions
            };
            return Ok(result);
        }
        
        [HttpGet("{id}", Name = nameof(GetQuestion))]
        public IActionResult GetQuestion(int id)
        {
            var question = _dataService.GetQuestion(id);
           // var answer = _dataService.GetAnswersByParent(id); //by parent
            if (question == null) return NotFound();
            
            
                var result = new
                {
                    Link = Url.Link(nameof(GetQuestion), new { question.Id }),
                    question.Title,
                    question.CreationDate,
                    question.Score,
                    question.Body,
                    AcceptedAnswer = Url.Link(nameof(AnswersController.GetAnswer)
                    , new {id = question.AcceptedAnswerId }),
                    //something wrong
                    Answers = Url.Link(nameof(AnswersController.GetAnswersByParent)
                    , new {id = question.Id}),
                    Comments = Url.Link(nameof(GetQuestionComment), new { id = question.Id}),

                    
                    

                };
                return Ok(result);
    
        }


        [HttpGet("comments/{id}", Name = nameof(GetQuestionComment))]
        public IActionResult GetQuestionComment(int id)
        {
            var questioncomments = _dataService.GetQuestionComments(id);
            if (questioncomments.Count == 0) return NotFound();
            return Ok(questioncomments);


        }

       
        [HttpGet("name/{name}")]
        public IActionResult GetQuestionByName(string name, int page = 0, int pageSize = 5)
        {
            var question = _dataService.GetQuestionsByString(name, page, pageSize);
            var numberOfItems = _dataService.GetNumberOfQuestions();

            var total = _dataService.GetNumberOfQuestions();
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 ? Url.Link(nameof(GetQuestions), new { page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetQuestions), new { page = page + 1, pageSize }) : null;

            if (question.Count == 0) return NotFound();
            return Ok(question);


        }
    }
  
}