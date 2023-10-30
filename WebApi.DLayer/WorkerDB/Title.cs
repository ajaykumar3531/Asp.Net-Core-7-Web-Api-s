using System;
using System.Collections.Generic;

namespace WebApi.DLayer.WorkerDB
{
    public partial class Title
    {
        public int? WorkerRefId { get; set; }
        public string? WorkerTitle { get; set; }
        public DateTime? AffectedFrom { get; set; }

        public virtual Worker? WorkerRef { get; set; }
    }
}
