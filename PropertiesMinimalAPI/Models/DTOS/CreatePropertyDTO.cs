﻿namespace PropertiesMinimalAPI.Models.DTOS
{
    public class CreatePropertyDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
