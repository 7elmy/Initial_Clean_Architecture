using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IUnitOfWork _unitOfWork;

        public TestController(ITestService testService, IUnitOfWork unitOfWork)
        {
            _testService = testService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            throw new Exception();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person p)
        {
            _testService.LogTest();
            return Ok();
        }

        /*
         * Person 
         *       []
         *       NID : 14 num interger
         *       Name : between 8 , 32
         *       Phone : 11 number
         *       BirthDate : validate('dd/mm/yyyy')
         */
    }

    public class Person
    {
        [Required]
        public int NId { get; set; }
        [MinLength(8)]
        public string Name { get; set; }
    }
}
