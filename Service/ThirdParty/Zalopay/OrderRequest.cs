﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ThirdParty.Zalopay {
    public class OrderRequest {
        public decimal Topup { get; set; }
        public int AppId { get; set; }
        public string AppTransId { get; set; }
        public string Mac { get; set; }
    }
}
