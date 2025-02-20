namespace InsideTeste
{
    public interface IOrderCommandStore
    {
        Task<Guid> AddNewOrder();
        Task AddProductToOrder(Guid orderId, Guid productId);
        Task RemoveProductFromOrder(Guid orderId, Guid productId);
        Task CloseOrder(Guid orderId);     
    }
}
