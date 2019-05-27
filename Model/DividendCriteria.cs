namespace GoodQualityDividend.Model
{
    public class DividendCriteria
    {
        public string Company { get; set; }
        public decimal Dividend { get; set; }
        public decimal PricePerStock { get; set; }
        public decimal PercentageYield { get; set; }
        public decimal PercentagePayoutRatio { get; set; }
        public decimal Percentage10yrGrowthRate { get; set; }
        public decimal _5_10Ratio { get; set; }
    }
}