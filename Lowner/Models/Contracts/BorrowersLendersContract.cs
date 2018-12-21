using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Models.Contracts
{
    public class BorrowersLendersContract
    {
        public BorrowersLendersContract()
        {
            Borrowers = new List<string>();
            Lenders = new List<string>();
        }

        public List<string> Borrowers { get; set; }
        public List<string> Lenders { get; set; }
    }
}
