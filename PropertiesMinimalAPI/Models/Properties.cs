﻿using System.ComponentModel.DataAnnotations;

namespace PropertiesMinimalAPI.Models
{
    public class Properties
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
