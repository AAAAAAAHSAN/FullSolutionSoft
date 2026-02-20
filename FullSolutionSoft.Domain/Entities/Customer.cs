using System;
using System.Collections.Generic;
using System.Text;

namespace FullSolutionSoft.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateddAt { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
