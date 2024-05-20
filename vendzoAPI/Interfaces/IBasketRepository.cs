namespace vendzoAPI.Interfaces
{
    public interface IBasketRepository
    {
        ICollection<Basket> GetAllBaskets();
        ICollection<Basket> GetBasketOfUser(string userId);
        Basket GetBasket(string id);
        Basket GetBasket(string userId, string itemId);
        bool BasketExists(string id);
        bool Add(Basket basket);
        bool Update(Basket basket);
        bool Delete(Basket basket);
        bool Save();



    }
}
