using System;

namespace GoodQualityDividend.Interfaces
{
    public interface IStock
    {
        string Company { get; set; }
        decimal Dividend { get; set; }
        decimal PricePerShare { get; set; }
        decimal AnnualRevenue { get; set; }
        decimal OutStandingNumberOfShares { get; set;}
        DateTime Year { get; set; }
    }
}