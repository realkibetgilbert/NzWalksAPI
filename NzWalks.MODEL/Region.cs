using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzWalks.MODEL
{
    public class Region
    {
        public Guid  Id { get; set; }
        public string  Code { get; set; }
        public string  Name { get; set; }
        public string?  ImageUrl { get; set; }
    }
}
