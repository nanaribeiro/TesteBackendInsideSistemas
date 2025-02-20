using InsideTeste.Database.Enumerator;

namespace InsideTeste.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public EOrderStatus Status { get; set; }
    }
}
