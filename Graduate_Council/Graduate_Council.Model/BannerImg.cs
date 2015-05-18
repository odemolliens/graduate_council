using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduate_Council.Model
{
    public class BannerImg
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsVisible { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

    }
}
