using MyShop.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Model.Models
{
    [Table("NganhNghes")]
    public class NganhNghe : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Ten { set; get; }

        [MaxLength(256)]
        public string Alias { set; get; }

        public int? ParentId { set; get; }

        public virtual IEnumerable<TinTuyenDungNganhNghe> TinTuyenDungNganhNghes { set; get; }
    }
}