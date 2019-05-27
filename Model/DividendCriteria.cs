using System;
using GoodQualityDividend.Interfaces;

namespace GoodQualityDividend.Model
{
    public class DividendCriteria : IDividendCriteria
    {
        public string Company { get; set; }
        public decimal Dividend { get; set; }
        public decimal PricePerStock { get; set; }
        public decimal PercentageYield { get; set; }
        public decimal PercentagePayoutRatio { get; set; }
        public decimal Percentage10yrGrowthRate { get; set; }
        public decimal _5_10Ratio { get; set; }
        public DateTime Year { get; set; }
    }
}