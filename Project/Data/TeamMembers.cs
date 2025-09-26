using KooliProjekt.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace KooliProjekt.Data
{
    [Table("Team_Members")]
    public class TeamMembers : Entity
    {
        public new int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
