using System;

namespace KooliProjekt.Data
{
    public class Tasks : Entity
    {

        public string Title { get; set; }

        public DateTime TaskStart { get; set; }

    // The migration used TimeSpan for ExpectedTime
    public TimeSpan ExpectedTime { get; set; }

    // Migration has ProjectId and AssignedUserId FKs
    public int ProjectId { get; set; }
    public int? AssignedUserId { get; set; }

    // Newer UI field; keep NotMapped so EF doesn't require a Performer column
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public string InCharge { get; set; }

        public string Description { get; set; }

        public Boolean WorkDone { get; set; }

    }
}
