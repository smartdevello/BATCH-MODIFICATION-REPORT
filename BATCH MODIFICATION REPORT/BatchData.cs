using System;
using System.Collections.Generic;
using System.Text;

namespace BATCH_MODIFICATION_REPORT
{
    public class BatchData
    {
        public string description { get; set; }
        public string shortname { get; set; }
        public string type { get; set; }
        public int votinglocationnumber { get; set; }
        public string votinglocationname { get; set; }
        public string votetype { get; set; }
        public int batchid { get; set; }
        public int file { get; set; }
        public int ballots { get; set; }
        public int modified { get; set; }

    }
}
