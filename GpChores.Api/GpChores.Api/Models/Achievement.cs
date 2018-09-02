using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpJobs.Api.Models
{
    public class Achievement
    {
        public string Id { get; set; }
        public string JobId { get; set; }
        public AchievementStatus Status { get; set; }
        public int Points { get; set; }
    }
}
