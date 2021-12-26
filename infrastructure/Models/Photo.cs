using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infrastructure.Models
{
    public class Photo
    {
        public string id { set; get; }
        public string author { set; get; }
        public string width { set; get; }
        public string height { set; get; }
        public string url { set; get; }
        public string download_url { set; get; }
        public byte[] image { set; get; }
    }
}
