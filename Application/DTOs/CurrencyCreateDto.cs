namespace Application.DTOs
{
    public class CurrencyCreateDto
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float ExchangeRate { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class CurrencyDto : CurrencyCreateDto
    {
        public int Id { get; set; }
    }
}
