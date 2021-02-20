using System;

namespace Repository.Models
{
    public class Model
    {
        public Guid Id { get; set; }
        public string Description { get; set; }


        // Relationships
        public Guid MakeId { get; set; }
        public Make Make { get; set; }
    }
}
