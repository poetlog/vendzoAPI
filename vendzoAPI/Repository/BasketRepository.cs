using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly VendzoContext _context;

        public BasketRepository(VendzoContext context)
        {
            _context = context;
        }

        public bool Add(Basket basket)
        {
            _context.Add(basket);
            return Save();
        }

        public bool BasketExists(string id)
        {
            return _context.Baskets.Where(a => a.Id == id).Any();
        }

        public bool Delete(Basket basket)
        {
            _context.Remove(basket);
            return Save();
        }

        public ICollection<Basket> GetAllBaskets()
        {
            return _context.Baskets.ToList();
        }

        public Basket GetBasket(string id)
        {
            return _context.Baskets.Where(a => a.Id == id).FirstOrDefault();
        }

        public Basket GetBasket(string userId, string itemId)
        {
            return _context.Baskets
                .Where(a => a.UserId == userId && a.ItemId == itemId && a.IsDeleted == false)
                .FirstOrDefault();
        }

        public ICollection<Basket> GetBasketOfUser(string userId)
        {
            return _context.Baskets.Where(a => a.UserId == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool Update(Basket basket)
        {
            _context.Update(basket);
            return Save();
        }
    }
}
