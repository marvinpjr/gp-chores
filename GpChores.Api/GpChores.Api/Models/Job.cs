using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpJobs.Api.Models
{
    public class Job
    {
        public string Id { get; set; }
        public string JobProfileId { get; set; }
        public List<JobStatus> Statuses { get; set; }
    }
}
