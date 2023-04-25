namespace KinKanMaiUI
{
    public interface IHomeRepository
    {

        Task<IEnumerable<Menu>> GetMenus(string sTerm = "", int shopId = 0);
        Task<IEnumerable<Shop>> Shops();
    }
}