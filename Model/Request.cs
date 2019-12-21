using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchfight
{
    public class Request
    {
        public String Title { get; set; }
        public String TotalResults { get; set; }
        public String SearchTerms { get; set; }
        public String Count { get; set; }
        public String StartIndex { get; set; }
    }
}
