using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzWalks.MODEL
{
    public class Walk
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string ImageUrl { get; set; }
        public long DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }
        public long RegionId { get; set; }
        public Region Region { get; set; }
    }
}
