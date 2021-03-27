namespace Shopping.Sales
{
    public record OrderItem
    {
        public long ProductId { get; init; }
        public uint Count { get; set; }
    }
}