using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Models.Contracts
{
    public class PayLoanContract
    {
        public int LoanId { get; set; }
        public decimal Quantity{ get; set; }
    }
}
