using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Controllers
{
    public class UsersController : Controller
    {
        DataService _dataService;
        public UsersController(DataService dataService)
        {
            _dataService = dataService;
        }


    }
}
