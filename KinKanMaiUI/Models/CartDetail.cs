using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KinKanMaiUI.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }

        [Required]
        public int ShoppingCart_Id { get; set; }
        [Required]
        public int MenuId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Menu Menu { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
