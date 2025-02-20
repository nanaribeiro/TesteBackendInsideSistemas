using InsideTeste.Database.Enumerator;
using InsideTeste.Models;

namespace InsideTeste
{
    public interface IOrderService
    {
        Task<Guid> RegistryNewOrder();
        Task<bool> AddProductToOrder(ProductOrder products);
        Task<bool> RemoveProductFromOrder(ProductOrder products);
        Task<bool> CloseOrder(Guid orderId);
        Task<List<OrderModel>> GetOrders(EOrderStatus? orderStatus);
        Task<OrderItemsModel> GetOrderAndProducts(Guid orderId);
    }
}
