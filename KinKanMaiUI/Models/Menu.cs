
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinKanMaiUI.Models
{
    [Table("Menu")]
    public class Menu
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? MenuName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }

        public List<CartDetail> CartDetail { get; set; }

        [NotMapped]
        public string ShopName { get; set; }
    }
}
