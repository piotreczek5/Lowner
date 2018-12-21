using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lowner.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        [Required]
        public string Lender { get; set; }
        [Required]
        public string Borrower { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]        
        public DateTime LoanPaymentDueDate{ get; set; }

        public override bool Equals(object obj)
        {
            Loan loan = (Loan)obj;

            if (loan == null)
            {
                return false;
            }

            return loan.Lender.Equals(Lender) &&
                loan.Borrower.Equals(Borrower) &&
                loan.LoanPaymentDueDate == LoanPaymentDueDate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
