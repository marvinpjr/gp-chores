using System;

namespace GpJobs.Api.Models
{
    public class JobStatus
    {
        public DateTime Date { get; set; }
        public JobStatusName Status { get; set; }
    }

    public enum JobStatusName
    {
        ToDo,
        InProgress,
        ReportedDone,
        ValidatedDone,
        Paid
    }
}