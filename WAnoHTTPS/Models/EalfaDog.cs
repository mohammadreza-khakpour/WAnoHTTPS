using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAnoHTTPS.Models
{
    public class EalfaDog
    {
        public int Id { get; set; }
        public string EalfaDogName { get; set; }
        public string EalfaDogSize { get; set; }
        public bool EalfaDogIsHealthy { get; set; }
        public byte[] EalfaDogPhoto { get; set; }
        public byte[] EalfaDogThumbnailPhoto { get; set; }
    }
}
