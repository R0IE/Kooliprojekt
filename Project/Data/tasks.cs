using System;

namespace KooliProjekt.Data
{
    public class Tasks : Entity
    {
        public new int Id { get; set; }

        public string Title { get; set; }

        public DateTime TaskStart { get; set; }

        public TimeSpan ExpectedTime { get; set; }

        public Boolean InCharge { get; set; }

        public string? Description { get; set; }

        public Boolean WorkDone { get; set; }

        public string Files { get; set; }
    }
}