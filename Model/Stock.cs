namespace GoodQualityDividend.Model
{
    public class Stock
    {
        public string Company { get; set; }
        public decimal Dividend { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal AnnualRevenue { get; set; }
        public decimal OutStandingNumberOfShares { get; set;}
    }
}