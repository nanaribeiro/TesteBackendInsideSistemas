namespace InsideTeste.Models
{
    public class ProductOrder
    {
        public Guid OrderId { get; set; }
        public List<Guid> ProductsId { get; set; } = default!;
    }
}
