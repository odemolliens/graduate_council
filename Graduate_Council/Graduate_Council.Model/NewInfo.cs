using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduate_Council.Model
{
    public class NewInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Detail { get; set; }
        public DateTime SubDateTime { get; set; }
        public string Category { get; set; }
        public int PageView { get; set; }
    }
}
