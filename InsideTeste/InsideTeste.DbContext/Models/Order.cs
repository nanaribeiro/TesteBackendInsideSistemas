using InsideTeste.Database.Enumerator;

namespace InsideTeste.Database.Models
{
    public class Order()
    {       
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public EOrderStatus Status { get; set; }
        public List<Product> Products { get; set; } = [];

    }
}
