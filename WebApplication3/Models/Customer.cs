using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Customer
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Surname { get; set; }
        public DateTime PurchaseDate { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}