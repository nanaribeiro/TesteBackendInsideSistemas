using InsideTeste.Database.Enumerator;

namespace InsideTeste.Models
{
    public class OrderItemsModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public EOrderStatus Status { get; set; }
        public List<ProductModel> Products { get; set; } = [];
    }
}
