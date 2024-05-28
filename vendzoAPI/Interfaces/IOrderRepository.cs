namespace vendzoAPI.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrders();
        ICollection<OrderEntry> GetAllEntries();
        Order GetOrder(string id);
        OrderEntry GetEntry(string id);
        ICollection<Order> GetOrdersOfUser(string userId);
        ICollection<Order> GetOrdersOfSeller(string userId);
        ICollection<OrderEntry> GetEntriesOfOrder(string orderId);
        bool OrderExists(string orderId);
        bool EntryExists(string entryId);
        bool AddOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool AddEntry(OrderEntry entry);
        bool UpdateEntry(OrderEntry entry);
        bool DeleteEntry(OrderEntry entry);
        bool Save();


    }
}
