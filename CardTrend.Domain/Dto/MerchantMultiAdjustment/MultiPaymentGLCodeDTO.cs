﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.Domain.Dto.MerchantMultiAdjustment
{
   public class MultiPaymentGLCodeDTO
    {
       public string GLAcctNo { get; set; }
       public string Descp { get; set; }
       public string TxnType { get; set; }
    }
}
