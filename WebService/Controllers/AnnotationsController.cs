using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("api/annotation")]
    [ApiController]
    public class AnnotationsController : Controller
    {
        DataService _dataService;
        public AnnotationsController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        [HttpPost]
        public IActionResult PostAnnotations([FromBody]Annotations annotations)
        {
             _dataService.CreateAnnotation(annotations.Body, annotations.UserId,annotations.PostId);

            return Created($"api/annotation/{annotations}", annotations);
        } 

        [HttpPut("body")]
        public IActionResult UpdateAnnotation([FromBody]Annotations annotations)
        {
           
                var anno = _dataService.UpdateAnnotation(annotations.Body, annotations.UserId, annotations.PostId);
                if (anno == false)
                {
                    return NotFound();
                }
                return Ok(anno);
            
          
        }
        /*
        [HttpDelete]

        public IActionResult DeleteAnnotation(string body)
        {
            var delannotation = _dataService.DeleteAnnotation(body);
            if (delannotation==false)
            {
                return NotFound();
            }
            return Ok(delannotation);
        }
        */
    }
}