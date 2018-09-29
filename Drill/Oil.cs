using System;
using System.Collections.Generic;

namespace Drill
{
    public partial class Oil
    {
        public decimal? Price { get; set; }
        public string Uom { get; set; }
        public DateTime? SampleDate { get; set; }
        public string Source { get; set; }
        public TimeSpan? SampleTime { get; set; }
        public int Id { get; set; }
    }
}
