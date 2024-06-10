using AutoMapper;
using vendzoAPI.DTO;
using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly VendzoContext _context;
        private readonly IMapper _mapper;

        public BasketRepository(VendzoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            return _context.Baskets.Where(a => a.UserId == userId && a.IsDeleted == false).ToList();
        }

        public ICollection<BasketDetailsDTO> GetBasketDetailsOfUser(string userId)
        {
            var baskets = _mapper.Map<List<BasketDetailsDTO>>(
                _context.Baskets
                .Where(a => a.UserId == userId && a.IsDeleted == false)
                .ToList()
            );

            baskets = baskets.Select(basket =>
            {
                if (basket.ItemId == null)
                {
                    return basket;
                }
                var item = _context.Items.Where(a=> a.Id == basket.ItemId).FirstOrDefault();
                if (item != null)
                {
                    basket.ItemName = item.Title;
                    basket.ItemStock = item.Stock;
                    basket.ItemPrice = item.Price * basket.Quantity;
                }
                else basket.ItemName = basket.ItemId;
                return basket;
            }).ToList();

            return baskets;

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

        public bool ClearBasketOfUser(string userId)
        {
            _context.Baskets
                .Where(a => a.UserId == userId)
                .ToList()
                .ForEach(a => a.IsDeleted = true);
            return Save();
        }
    }
}
