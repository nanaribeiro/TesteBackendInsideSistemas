namespace InsideTeste.Database.Models
{
    public class Product()
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public List<Order> Oders { get; set; } = [];
    }
}
