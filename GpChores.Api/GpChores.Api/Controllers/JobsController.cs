using GpJobs.Api.Models;
using GpJobs.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GpJobs.Api.Controllers
{
    [Route("api/[controller]")]
    public class JobProfileController : Controller
    {
        IDataService<JobProfile> _jobService;

        public JobProfileController(IDataService<JobProfile> dataService)
        {
            _jobService = dataService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jobs = _jobService.GetAll();            
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var job = _jobService.GetById(id);
            return Ok(job);
        }

        [HttpPost]
        public IActionResult Post([FromBody]JobProfile job)
        {
            var newjob = _jobService.Create(job);
            return Ok(newjob);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]JobProfile job)
        {
            var updatedjob = _jobService.Update(id, job);
            return Ok(updatedjob);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _jobService.Delete(id);
        }
    }
}
