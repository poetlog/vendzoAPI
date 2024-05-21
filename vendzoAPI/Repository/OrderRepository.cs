using vendzoAPI.Interfaces;
using vendzoAPI.Models;

namespace vendzoAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly VendzoContext _context;

        public OrderRepository(VendzoContext context)
        {
            _context = context;
        }

        public bool AddEntry(OrderEntry entry)
        {
            _context.Add(entry);
            return Save();
        }

        public bool AddOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public bool DeleteEntry(OrderEntry entry)
        {
            _context.Remove(entry);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        public bool EntryExists(string entryId)
        {
            return _context.OrderEntries.Where(a => a.Id == entryId).Any();
        }

        public ICollection<OrderEntry> GetAllEntries()
        {
            return _context.OrderEntries.ToList();
        }

        public ICollection<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public ICollection<OrderEntry> GetEntriesOfOrder(string orderId)
        {
            return _context.OrderEntries.Where(a => a.OrderId == orderId).ToList();
        }

        public OrderEntry GetEntry(string id)
        {
            return _context.OrderEntries.Where(a => a.Id == id).FirstOrDefault();
        }

        public Order GetOrder(string id)
        {
            return _context.Orders.Where(a => a.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrdersOfBuyer(string userId)
        {
            return _context.Orders.Where(a => a.UserId == userId).ToList();
        }

        public ICollection<Order> GetOrdersOfSeller(string userId)
        {
            return _context.Orders.Where(a => a.SellerId == userId).ToList();
        }

        public bool OrderExists(string orderId)
        {
            return _context.Orders.Where(a => a.Id == orderId).Any();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateEntry(OrderEntry entry)
        {
            _context.Update(entry);
            return Save();
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
