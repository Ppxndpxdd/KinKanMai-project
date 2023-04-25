namespace KinKanMaiUI.Models.DTOs
{
    public class MenuDisplayModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<Shop> Shops { get; set; }

        public string Sterm { get; set; } = "";
        public int ShopId { get; set; } = 0;
    }
}
