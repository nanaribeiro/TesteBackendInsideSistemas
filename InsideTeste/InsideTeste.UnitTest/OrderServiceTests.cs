using InsideTeste.Language;
using NSubstitute;

namespace InsideTeste.UnitTest
{
    public sealed class OrderServiceTests
    {
        private readonly IOrderCommandStore _orderCommandStore = Substitute.For<IOrderCommandStore>();
        private readonly IOrderQueryStore _orderQueryStore = Substitute.For<IOrderQueryStore>();

        [Fact(DisplayName = "Ao adicionar produtos em um novo pedido deve retornar true, indicando que os produtos" +
                            "foram adicionados com sucesso ao pedido")]
        [Trait("Camada", "Service")]
        public async void NewOrder_WhenAddProducts_MustReturnTrue()
        {

            var service = new OrderService(_orderCommandStore, _orderQueryStore);
            var idOrder = await service.RegistryNewOrder();

            var query = new Models.ProductOrder()
            {
                OrderId = idOrder,
                ProductsId = [new Guid("42D42D5C-3FF2-EF11-A1FB-4484C582882F"), new Guid("A0B0F07C-3FF2-EF11-9037-EF84C582882F")]
            };

            _orderQueryStore.ProductExists(Arg.Any<Guid>())
                 .Returns(Task.FromResult(true));

            var result = await service.AddProductToOrder(query);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Ao adicionar produtos inexistentes em um novo pedido deve disparar exceção")]
        [Trait("Camada", "Service")]
        public async void NewOrder_WhenAddInexistentProducts_MustThrowException()
        { 

            var service = new OrderService(_orderCommandStore, _orderQueryStore);
            var idOrder = await service.RegistryNewOrder();

            var query = new Models.ProductOrder()
            {
                OrderId = idOrder,
                ProductsId = [new Guid("42D42D5C-3FF2-EF11-A1FB-4484C582882F"),
                              new Guid("A0B0F07C-3FF2-EF11-9037-EF84C582882F")]
            };

            _orderQueryStore.ProductExists(Arg.Any<Guid>())
                 .Returns(Task.FromResult(false));

            Task act() => service.AddProductToOrder(query);

            // Assert
            Exception exception = await Assert.ThrowsAsync<AggregateException>(act);
            Assert.Equal("Existem produtos inexistentes (O produto 42d42d5c-3ff2-ef11-a1fb-4484c582882f não existe) " +
                "(O produto a0b0f07c-3ff2-ef11-9037-ef84c582882f não existe)", exception.Message);
        }

        [Fact(DisplayName = "Ao adicionar produtos em um pedido fechado deve disparar exceção")]
        [Trait("Camada", "Service")]
        public async void ClosedOrder_WhenAddProducts_MustThrowException()
        {

            var service = new OrderService(_orderCommandStore, _orderQueryStore);
            var idOrder = await service.RegistryNewOrder();

            var query = new Models.ProductOrder()
            {
                OrderId = idOrder,
                ProductsId = [new Guid("42D42D5C-3FF2-EF11-A1FB-4484C582882F")]
            };

            _orderQueryStore.ProductExists(Arg.Any<Guid>())
                .Returns(Task.FromResult(true));

            await service.AddProductToOrder(query);

            _orderQueryStore.OrderHasProduct(Arg.Any<Guid>())
                .Returns(Task.FromResult(true));

            await service.CloseOrder(idOrder);

            var newQuery = new Models.ProductOrder()
            {
                OrderId = idOrder,
                ProductsId = [new Guid("A0B0F07C-3FF2-EF11-9037-EF84C582882F")]
            };

            _orderQueryStore.IsOrderClosed(Arg.Any<Guid>())
                .Returns(Task.FromResult(true));

            Task act() => service.AddProductToOrder(newQuery);

            // Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal(Messages.AddProductClosedOrder, exception.Message);
        }

        [Fact(DisplayName = "Ao remover produtos em um novo pedido deve retornar true, indicando que os produtos" +
                            "foram removidos com sucesso do pedido")]
        [Trait("Camada", "Service")]
        public async void NewOrder_WhenRemoveProducts_MustReturnTrue()
        {

            var service = new OrderService(_orderCommandStore, _orderQueryStore);
            var idOrder = await service.RegistryNewOrder();

            var query = new Models.ProductOrder()
            {
                OrderId = idOrder,
                ProductsId = [new Guid("42D42D5C-3FF2-EF11-A1FB-4484C582882F"),
                              new Guid("A0B0F07C-3FF2-EF11-9037-EF84C582882F")]
            };

            _orderQueryStore.ProductExists(Arg.Any<Guid>())
                 .Returns(Task.FromResult(true));

            await service.AddProductToOrder(query);

            _orderQueryStore.IsOrderClosed(Arg.Any<Guid>())
                .Returns(Task.FromResult(false));

            var result = await service.RemoveProductFromOrder(query);

            // Assert
            Assert.True(result);
        }
    }
}
