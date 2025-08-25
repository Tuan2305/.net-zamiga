using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Portfolios")]
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        
        public string AppUserId { get; set; }
        public int StockId { get; set; }
        
        // Navigation property
        public Stock Stock { get; set; }
    }
}