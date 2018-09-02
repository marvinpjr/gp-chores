using System;

namespace GpJobs.Api.Models
{
    public class BalanceRecord
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
