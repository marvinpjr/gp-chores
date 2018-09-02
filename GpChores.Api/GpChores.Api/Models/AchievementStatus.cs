using System;

namespace GpJobs.Api.Models
{
    public class AchievementStatus
    {
        public DateTime Date { get; set; }
        public AchievementStatusName Name { get; set; }
    }

    public enum AchievementStatusName
    {
        Created,
        Pending,
        Completed,
        Credited
    }
}