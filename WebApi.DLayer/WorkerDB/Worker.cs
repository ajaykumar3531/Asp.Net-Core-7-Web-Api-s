using System;
using System.Collections.Generic;

namespace WebApi.DLayer.WorkerDB
{
    public partial class Worker
    {
        public int WorkerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Salary { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? Department { get; set; }
    }
}
