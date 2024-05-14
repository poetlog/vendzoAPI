using vendzoAPI.Interfaces;

namespace vendzoAPI.Repository
{
    public class ItemRepository : IItemRepository
    {
        public bool Add(Item item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Item item)
        {
            throw new NotImplementedException();
        }

        public Item GetItem(string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public ICollection<Item> GetItemsOfCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Item> GetItemsOfUser(string userId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
