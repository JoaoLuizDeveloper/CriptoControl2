using System.ComponentModel.DataAnnotations;

namespace CriptoControl.Model.Entity
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int User_Insert { get; set; }
        public DateTime DT_Insert { get; set; } = DateTime.Now;
        public int? User_Update { get; set; }
        public DateTime? DT_Update { get; set; }
    }
}