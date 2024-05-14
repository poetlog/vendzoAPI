namespace vendzoAPI.Interfaces
{
    public interface IItemRepository
    {
        ICollection<Item> GetItems();
        Item GetItem(string id);
        ICollection<Item> GetItemsOfUser(string userId);
        ICollection<Item> GetItemsOfCategory(string categoryName);
        bool Add(Item item);
        bool Update(Item item);
        bool Delete(Item item);
        bool Save();
    }
}
