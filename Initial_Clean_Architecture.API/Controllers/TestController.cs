using Initial_Clean_Architecture.Application.Domain.Interfaces;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Initial_Clean_Architecture.Data.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            var repo = _unitOfWork.GetRepositoryAsync<Test>();
            //await repo.AddAsync(new Test() { TestString = "test" });
            //await _unitOfWork.SaveChangesAsync();
            var x = await repo.GetFirstAsync();
            x.TestString = "test 3";
            //x.ModificationDate = DateTime.UtcNow;
            repo.Update(x);
            await _unitOfWork.SaveChangesAsync();


            return Ok();
        }
    }
}
