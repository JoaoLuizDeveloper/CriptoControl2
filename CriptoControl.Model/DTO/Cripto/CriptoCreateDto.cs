using System.ComponentModel.DataAnnotations;

namespace CriptoControl.Model.DTO.Cripto
{
    public class CriptoCreateDto
    {
        [Required(ErrorMessage = "You must to set a description, for example where inside the place is the machine."), MinLength(3, ErrorMessage = "Minimum 3 characters by Description")]
        public string Description { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required int CurrentQtd { get; set; } = 0; 
        public int WishQtd { get; set; }
        public int User_Insert { get; set; }
        public DateTime DT_Insert { get; set; } = DateTime.Now;
    }
}