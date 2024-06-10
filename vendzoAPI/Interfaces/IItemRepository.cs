namespace vendzoAPI.Interfaces
{
    public interface IItemRepository
    {
        ICollection<Item> GetItems();
        ICollection<Item> GetItemsPagination(int page);
        ICollection<Item> SearchItems(string searchTerm, int page);
        ICollection<Item> FilterItems(string category, int page);
        ICollection<Item> SortItems(string sortTerm, int page);
        ICollection<Item> GetFeaturedItems();
        Item GetItem(string id);
        ICollection<Item> GetItemsOfUser(string userId);
        ICollection<Item> GetItemsOfCategory(string categoryName);
        string GetSellerOfItem(string itemId);
        int GetItemsCount();
        bool ItemExists(string id);
        bool Add(Item item);
        bool Update(Item item);
        bool Delete(Item item);
        bool Save();
    }
}
