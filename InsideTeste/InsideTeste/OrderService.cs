using InsideTeste.Database.Enumerator;
using InsideTeste.Language;
using InsideTeste.Models;

namespace InsideTeste;

public class OrderService(IOrderCommandStore orderCommandStore, IOrderQueryStore orderQueryStore) : IOrderService
{
    private readonly IOrderCommandStore _orderCommandStore = orderCommandStore;
    private readonly IOrderQueryStore _orderQueryStore = orderQueryStore;

    public async Task<Guid> RegistryNewOrder()
    {
        return await _orderCommandStore.AddNewOrder();
    }

    public async Task<bool> AddProductToOrder(ProductOrder products)
    {
        if (await _orderQueryStore.IsOrderClosed(products.OrderId))
            throw new Exception(Messages.AddProductClosedOrder);

        List<Exception> exceptions = [];
        foreach (var productId in products.ProductsId)
        {
            if (await _orderQueryStore.ProductExists(productId))
                await _orderCommandStore.AddProductToOrder(products.OrderId, productId);
            else
                exceptions.Add(new Exception(string.Format(Messages.ProductDoesNotExist, productId)));
        }

        if(exceptions != null)
            throw new AggregateException(Messages.MultipleProductsMissing, exceptions);

        return Task.CompletedTask.IsCompletedSuccessfully;
    }

    public async Task<bool> RemoveProductFromOrder(ProductOrder products)
    {
        if (await _orderQueryStore.IsOrderClosed(products.OrderId))
            throw new Exception(Messages.AddProductClosedOrder);
        foreach (var productId in products.ProductsId)
        {
            await _orderCommandStore.RemoveProductFromOrder(products.OrderId, productId);
        }
        return Task.CompletedTask.IsCompletedSuccessfully;
    }

    public async Task<bool> CloseOrder(Guid orderId)
    {
        if (await _orderQueryStore.OrderHasProduct(orderId))
            await _orderCommandStore.CloseOrder(orderId);
        else
            throw new Exception(Messages.OrderCantBeClosedNoProducts);
        return Task.CompletedTask.IsCompletedSuccessfully;
    }

    public async Task<List<OrderModel>> GetOrders(EOrderStatus? orderStatus)
    {
        return await _orderQueryStore.GetOrders(orderStatus);
    }

    public async Task<OrderItemsModel> GetOrderAndProducts(Guid orderId)
    {
        return await _orderQueryStore.GetOrderAndProducts(orderId);
    }

}
