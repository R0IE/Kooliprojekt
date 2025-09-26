using KooliProjekt.Models;
using System;

namespace KooliProjekt.Data
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Department { get; set; }

        // Migration expects CreatedAt to be non-nullable. Initialize with UTC now so inserts don't send NULL.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // The migration does not include a Password column; if this property is used for UI purposes keep it NotMapped.
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? Password { get; set; }
    }
}
