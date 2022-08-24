using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.DTO
{
    public class CardDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(160)]
        [MaxLength(160)]
        public string Text { get; set; }
        [DefaultValue(false)]
        public bool Complete { get; set; }
    }
}
