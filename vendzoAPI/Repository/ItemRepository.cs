using vendzoAPI.Interfaces;

namespace vendzoAPI.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly VendzoContext _context;
        private readonly int _pageSize = 9;

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

        public ICollection<Item> FilterItems(string category, int page)
        {
            return _context.Items.Where(a => a.Category == category).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
        }

        public ICollection<Item> GetFeaturedItems()
        {
            var random = new Random(0);
            return _context.Items
                .AsEnumerable()
                .OrderBy(item => random.Next())
                .Take(20)
                .ToList();
        }

        public Item GetItem(string id)
        {
            return _context.Items.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Item> GetItems()
        {
            return _context.Items.ToList();
        }

        public int GetItemsCount()
        {
            return _context.Items.Count();
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

        public ICollection<Item> GetItemsPagination(int page)
        {
            return _context.Items.Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
        }

        public string GetSellerOfItem(string itemId)
        {
            string sellerId = _context.Items.Where(a => a.Id == itemId).Select(a => a.SellerId).FirstOrDefault();
            return _context.Users.Where(a => a.Id == sellerId).Select(a => a.Username).FirstOrDefault();
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

        public ICollection<Item> SearchItems(string searchTerm, int page)
        {
            return _context.Items.Where(a => a.Title.Contains(searchTerm) || a.Description.Contains(searchTerm)).Skip((page - 1) * _pageSize).Take(_pageSize).ToList(); ;
            
        }

        public ICollection<Item> SortItems(string sortTerm, int page)
        {
            if(sortTerm == "priceAsc")
                return _context.Items.OrderBy(a => a.Price).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            else if(sortTerm == "priceDesc")
                return _context.Items.OrderByDescending(a => a.Price).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            else if(sortTerm == "title")
                return _context.Items.OrderBy(a => a.Title).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            else if(sortTerm == "dateAsc")
                return _context.Items.OrderBy(a => a.CreatedAt).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            else if(sortTerm == "dateDesc")
                return _context.Items.OrderByDescending(a => a.CreatedAt).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            else return null;
           
        }

        public bool Update(Item item)
        {
            _context.Update(item);

            // Get all baskets
            var baskets = _context.Baskets.Where(a => a.Id == item.Id).ToList();
            if(baskets != null)
            {
                // Loop through each basket
                foreach (var basket in baskets)
                {
                    // Set isDeleted to 1
                    basket.IsDeleted = true;

                    // Update the basket
                    _context.Update(basket);
                }
            }
                
            //TODO: add integration to user ?
            return Save();
        }
    }
}
