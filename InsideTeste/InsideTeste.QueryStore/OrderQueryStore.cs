using InsideTeste.Database.Data;
using InsideTeste.Database.Enumerator;
using InsideTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace InsideTeste.QueryStore
{
    public class OrderQueryStore(ApplicationDbContext applicationDbContext) : IOrderQueryStore
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

        public async Task<List<OrderModel>> GetOrders(EOrderStatus? status)
        {
            try
            {
                List<OrderModel> orders = new();
                if (status != null)
                {
                    orders = await _applicationDbContext.Orders.Where(o => o.Status == status).
                        Select(x => new OrderModel()
                        {
                            Id = x.Id,
                            Date = x.Date,
                            Status = x.Status
                        }).ToListAsync();
                }
                else
                {
                    orders = await _applicationDbContext.Orders.
                                    Select(x => new OrderModel()
                                    {
                                        Id = x.Id,
                                        Date = x.Date,
                                        Status = x.Status
                                    }).ToListAsync();
                }
                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OrderItemsModel> GetOrderAndProducts(Guid orderId)
        {
            try
            {
                OrderItemsModel order = new();
                List<ProductModel> products = [];
                products = await _applicationDbContext.Items.Where(x => x.OrderId == orderId).
                                                            Select(x => new ProductModel()
                                                            {
                                                                Id = x.Product.Id,
                                                                Description = x.Product.Description,
                                                                Name = x.Product.Name,
                                                                Price = x.Product.Price
                                                            }).ToListAsync();

                order = await _applicationDbContext.Items.Where(x => x.OrderId == orderId).
                                                            Select(x => new OrderItemsModel()
                                                            {
                                                                Id = x.OrderId,
                                                                Date = x.Order.Date,
                                                                Products = products,
                                                                Status = x.Order.Status
                                                            }).FirstAsync();

                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> OrderHasProduct(Guid orderId)
        {
            try
            {
                return await _applicationDbContext.Orders.Where(p => p.Id == orderId && p.Products != default).AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ProductExists(Guid productId)
        {
            try
            {
                return await _applicationDbContext.Products.Where(p => p.Id == productId).AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsOrderClosed(Guid orderId)
        {
            try
            {
                return await _applicationDbContext.Orders.Where(p => p.Id == orderId && p.Status == EOrderStatus.Closed).AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
