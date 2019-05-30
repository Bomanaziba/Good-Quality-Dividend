using System;

namespace GoodQualityDividend.Interfaces
{
    public interface IDividendCriteria
    {
         
        string Company { get; set; }
        decimal Dividend { get; set; }
        decimal PricePerStock { get; set; }
        decimal PercentageYield { get; set; }
        decimal PercentagePayoutRatio { get; set; }
        decimal Percentage10yrGrowthRate { get; set; }
        decimal _5_10Ratio { get; set; }
        int Rating { get; set; }
        DateTime Year { get; set; }
    }
}