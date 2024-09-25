using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("trn_funding")]

    public class trn_funding
    {
        [Key]

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Column("loan_id")]
        [ForeignKey("MstLoans")]
        public string Loan_Id { get; set; }
        [Required]
        [Column("lender_id")]
        [ForeignKey("MstUser2")]
        public string Lender_id { get; set; } 
        [Required]
        [Column("Amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("funded_at")]
        public DateTime funded_at { get; set; } = DateTime.UtcNow;

        public MstUser2 User { get; set; }  

        public MstLoans Loans { get; set; }

    }
}
