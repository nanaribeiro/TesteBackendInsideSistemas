using InsideTeste.Database.Data;
using InsideTeste.Database.Enumerator;
using InsideTeste.Database.Models;
using InsideTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace InsideTeste.CommandStore
{
    public class OrderCommandStore(ApplicationDbContext applicationDbContext) : IOrderCommandStore
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<Guid> AddNewOrder()
        {
            try
            {
                var idToReturn = await _applicationDbContext.Orders.AddAsync(new Order() {Status = EOrderStatus.Open,
                                                                             Date = DateTime.UtcNow});
                await _applicationDbContext.SaveChangesAsync();
                return idToReturn.Entity.Id;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task AddProductToOrder(Guid orderId, Guid productId)
        {
            try
            {
                await _applicationDbContext.Items.AddAsync(new OrderItem() { OrderId = orderId, ProductId = productId });
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task RemoveProductFromOrder(Guid orderId, Guid productId)
        {
            try
            {
                _applicationDbContext.Items.Remove(new OrderItem() { OrderId = orderId, ProductId = productId });
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CloseOrder(Guid orderId)
        {
            try
            {
                var order = await _applicationDbContext.Orders.Where(o => o.Id == orderId).FirstAsync();
                order.Status = EOrderStatus.Closed;
                _applicationDbContext.Orders.Update(order);
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
