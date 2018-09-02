using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpJobs.Api.Models
{
    public class JobProfile: IDataItem
    {
        public string Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string CategoryId { get; set; }
    }
}
