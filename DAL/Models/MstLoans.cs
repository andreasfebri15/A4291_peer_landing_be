using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("mst_loans")]
   public  class MstLoans
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [Column("borrower_id")]
        [ForeignKey("user")]
        
        public string BorrowerId { get; set; }

        [Required]
        [Column ("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("interest_rate")]

        public decimal InteresestRate { get; set; }

        [Required]
        [Column("duration")]

        public int Duration { get; set; }

        [Required]
        [Column("status")]

        public string Status { get; set; } = "requested";

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  


        [Required]
        [Column("Update_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public MstUser2 User { get; set; }

    }
}
