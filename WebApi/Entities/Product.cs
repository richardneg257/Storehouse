﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
