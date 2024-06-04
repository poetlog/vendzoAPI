using vendzoAPI.Interfaces;

namespace vendzoAPI.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly VendzoContext _context;

        public ItemRepository(VendzoContext context)
        {
            _context = context;
        }

        public bool Add(Item item)
        {
            _context.Add(item);
            //TODO: add integration to user
            return Save();
        }

        public bool Delete(Item item)
        {
            _context.Remove(item);
            //TODO: add integration to user
            return Save();
        }

        public Item GetItem(string id)
        {
            return _context.Items.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Item> GetItems()
        {
            return _context.Items.ToList();
        }

        public ICollection<Item> GetItemsOfCategory(string categoryName)
        {
            return _context.Items.Where(a => a.Category == categoryName).ToList();
        }

        public ICollection<Item> GetItemsOfUser(string userId)
        {
            //if(_context.Users.Where(a => a.UserId == userId).Any())
                return _context.Items.Where(a => a.SellerId == userId && a.IsDeleted == false).ToList();
            //else return null
        }

        public bool ItemExists(string id)
        {
            return _context.Items.Where(a => a.Id == id).Any();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool Update(Item item)
        {
            _context.Update(item);
            //TODO: add integration to user ?
            return Save();
        }
    }
}
