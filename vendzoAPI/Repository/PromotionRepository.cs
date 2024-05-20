using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly VendzoContext _context;

        public PromotionRepository(VendzoContext context)
        {
            _context = context;
        }

        public bool Add(Promotion promotion)
        {
            _context.Add(promotion);
            return Save();
        }

        public bool CheckIsValid(string code)
        {
            Promotion promotion = _context.Promotions.Where(a => a.PromoCode == code).FirstOrDefault();

            if (promotion != null && promotion.Expires.HasValue && !promotion.IsDeleted) 
            {
                return promotion.Expires.Value > DateTimeOffset.Now;
            }
            return false;
        }

        public bool Delete(Promotion promotion)
        {
            _context.Remove(promotion);
            return Save();
        }

        public Promotion GetByCode(string code)
        {
            return _context.Promotions.Where(a => a.PromoCode == code).FirstOrDefault();
        }

        public Promotion GetById(string id)
        {
            return _context.Promotions.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Promotion> GetPromotions()
        {
            return _context.Promotions.ToList();
        }

        public ICollection<Promotion> GetValidPromotions()
        {
            return _context.Promotions
               .Where(a => a.Expires.HasValue && a.Expires.Value > DateTimeOffset.Now)
               .ToList();

        }

        public bool PromotionExists(string id)
        {
            return _context.Promotions.Where(a => a.Id == id).Any();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool Update(Promotion promotion)
        {
            _context.Update(promotion);
            return Save();
        }
    }
}
