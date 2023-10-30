using System;
using System.Collections.Generic;

namespace WebApi.DLayer.WorkerDB
{
    public partial class Bonu
    {
        public int? WorkerRefId { get; set; }
        public int? BonusAmount { get; set; }
        public DateTime? BonusDate { get; set; }

        public virtual Worker? WorkerRef { get; set; }
    }
}
