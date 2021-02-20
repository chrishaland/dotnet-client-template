using System;
using System.Collections.Generic;

namespace Repository.Models
{
    public class Make
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        // Relationships
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<Model> Models { get; set; }
    }
}
