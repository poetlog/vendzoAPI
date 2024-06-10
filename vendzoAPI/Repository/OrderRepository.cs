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
            if (!Save())
                return false;

            var basketItems = _context.Baskets.Where(a => a.UserId == order.UserId && a.IsDeleted == false).ToList();

            foreach (var basket in basketItems)
            {
                var item = _context.Items.Where(a => a.Id == basket.ItemId).FirstOrDefault();
                var orderEntry = new OrderEntry
                {
                    OrderId = order.Id,
                    ItemId = basket.ItemId,
                    Quantity = (int)basket.Quantity,
                    Price = (decimal)item.Price,
                    BuyerId = order.UserId,
                    SellerId = item.SellerId,
                    Photo = item.Photo,
                    CreatedAt = DateTime.Now,
                    ItemTitle = item.Title,
                    SellerName = _context.Users.Where(a => a.Id == item.SellerId).Select(a => a.Username).FirstOrDefault(),
                };
                AddEntry(orderEntry);
            }
            return true;
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

        public ICollection<Order> GetOrdersOfUser(string userId)
        {
            return _context.Orders.Where(a => a.UserId == userId).ToList();
        }

        public ICollection<Order> GetOrdersOfSeller(string userId)
        {
            //return _context.Orders.Where(a => a.SellerId == userId).ToList();
            return null;
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
