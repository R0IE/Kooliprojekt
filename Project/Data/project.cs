namespace KooliProjekt.Data
{
    public class Project : Entity
    {
        public string ProjectName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Budget { get; set; }
        public decimal HourlyRate { get; set; }
        // The DB schema (initial migration) contains a Description column. Ensure it is non-null when inserting.
        public string Description { get; set; } = string.Empty;

        // `Team` is a newer property the UI may use. Keep it optional (nullable) and not mapped to the DB.
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string? Team { get; set; }
    }
}
