using System.ComponentModel.DataAnnotations;

namespace GestionDesStagesTFYA.Server.Models
{
    public class StageStatut
    {
        [Key]
        public int StageStatutId { get; set; }

        [StringLength(50)]
        public string DescriptionStatut { get; set; }
    }
}
