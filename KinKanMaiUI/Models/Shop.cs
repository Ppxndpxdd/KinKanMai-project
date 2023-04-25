using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KinKanMaiUI.Models
{
    [Table("Shop")]
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string MenuName { get; set; }

        public List<Shop> Shops;
    }
}
