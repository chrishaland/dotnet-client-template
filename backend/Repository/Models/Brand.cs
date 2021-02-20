using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        // Relationships
        public List<Make> Makes { get; set; }
    }
}
