namespace WebPOS.Infrastructure.Commons.Foundation.Request
{
    public class BasePaginationRequest
    {
        public int PageNumber { get; set; } = 1;

        public int RowsNumber { get; set; } = 10;

        private readonly int MaxRowsNumber = 50;

        public string OrderType { get; set; } = "asc";

        public string? Sort { get; set; } = null;

        public int Records
        {
            get => RowsNumber;
            set
            {
                RowsNumber = value > MaxRowsNumber ? MaxRowsNumber : value;
            }
        }
    }
}
