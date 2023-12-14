using CriptoControl.Model.Entity;
using System.ComponentModel.DataAnnotations;

namespace CriptoControl.Model
{
    public class Cripto: BaseEntity
    {
        [Required(ErrorMessage = "You must to set a description, for example where inside the place is the machine."), MinLength(3, ErrorMessage = "Minimum 3 characters by Description")]
        public string Description { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required int CurrentQtd { get; set; } = 0;
 
        public int WishQtd { get; set; }
    }
}