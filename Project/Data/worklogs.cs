namespace KooliProjekt.Data
{
    public class WorkLogs : Entity
    {
        public new int Id { get; set; }

        public DateTime Date { get; set; }

        // The migration used TimeSpan for TimeCost (SQL time). Keep TimeSpan to match DB column.
        public TimeSpan TimeCost { get; set; }

        // Original migration stores UserId FK; newer code used Performer string. Keep both: map UserId to DB,
        // keep Performer as NotMapped for UI use.
        public int UserId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Performer { get; set; }

        public string Description { get; set; }
    }
}
