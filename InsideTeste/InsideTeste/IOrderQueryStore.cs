using InsideTeste.Database.Enumerator;
using InsideTeste.Models;

namespace InsideTeste
{
    public interface IOrderQueryStore
    {
        Task<List<OrderModel>> GetOrders(EOrderStatus? status);
        Task<OrderItemsModel> GetOrderAndProducts(Guid orderId);
        Task<bool> OrderHasProduct(Guid orderId);
        Task<bool> ProductExists(Guid productId);
        Task<bool> IsOrderClosed(Guid orderId);
    }
}
