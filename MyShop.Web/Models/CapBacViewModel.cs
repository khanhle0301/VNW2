using System.ComponentModel.DataAnnotations;

namespace MyShop.Web.Models
{
    public class CapBacViewModel
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Ten { set; get; }

        public bool Status { set; get; }
    }
}