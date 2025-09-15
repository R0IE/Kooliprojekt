namespace KooliProjekt.Data
{
    public class TeamMembers : Entity
    {
        public new int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}