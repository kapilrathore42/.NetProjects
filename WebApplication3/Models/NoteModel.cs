using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Notes field is required.")]
        public string Note { get; set; }
    }
}
