using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Http;
using DataLayer.Model;
using AutoMapper;

namespace WebService.Controllers
{
    /*[Route("api/posts")]
    [ApiController]
    public class PostsController : Controller
    {
        DataService _dataService;
        public PostsController(DataService dataService)
        {
            _dataService = dataService;
        }
        //-----------------------------Questions and Answers ----------------------//
        /*[HttpGet]
        public IActionResult GetPosts()
        {
            var data = _dataService.GetPosts();

            return Ok(data);
        }*/
       /* [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            var post = _dataService.GetPost(id);
                if (post == null) return NotFound();
            return Ok(post);
        }*/

       /* [HttpGet("questions")]
        public IActionResult GetQuestions()
        {
            var questions = _dataService.GetQuestions();
            return Ok(questions);
        }

        [HttpGet("questions/{id}")]
        public IActionResult GetQuestion(int id)
        {
            var question = _dataService.GetQuestion(id);
                if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpGet("questions/comments/{id}")]
        public IActionResult GetQuestionComment(int id)
        {
            var question = _dataService.GetQuestionComments(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpGet("questions/name/{name}")]
        public IActionResult GetQuestionByName(string name, int page = 0, int pageSize = 5)
        {
            var question = _dataService.GetQuestionsByString(name, page, pageSize);
            var numberOfItems = _dataService.GetNumberOfQuestions();
          
            if (question.Count == 0) return NotFound();
             return Ok(question);
            

        }*/
      /*  private string CreateLinkToNextPage(int page, int pageSize, int numberOfPages)
        {
            return page >= numberOfPages - 1
                ? null
                : CreateLink(page = page + 1, pageSize);
        }

        private string CreateLinkToPrevPage(int page, int pageSize)
        {
            return page == 0
                ? null
                : CreateLink(page - 1, pageSize);
        }

        */
      /*  // helper

        private Question CreateProductListModel(Question question)
        {
            var model = Mapper.Map<Question>(question);
            model.Url = Url.Link(nameof(GetProduct), new { id = product.Id });
            return model;
        }

        private static int ComputeNumberOfPages(int pageSize, int numberOfItems)
        {
            return (int)Math.Ceiling((double)numberOfItems / pageSize);
        }

        private string CreateLink(int page, int pageSize)
        {
            return Url.Link(nameof(GetProducts), new { page, pageSize });
        }
    }
    */
    /*[HttpGet("answers")]
        public IActionResult GetAnswers()
        {
            var answers = _dataService.GetAnswers();
            return Ok(answers);
        }

       [HttpGet("answers/{id}")]
       public IActionResult GetAnswer(int id)
        {
            var answer = _dataService.GetAnswer(id);
            if (answer == null) return NotFound(answer);
            return Ok(answer);

        }
    }*/
}
