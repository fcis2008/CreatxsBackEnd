namespace Application.DTOs
{
    public class OrderDetailsCreateDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public double Price { get; set; }
    }

    public class OrderDetailsDto : OrderDetailsCreateDto
    {
        public int OrderId { get; set; }

        public int Id { get; set; }
    }
}
