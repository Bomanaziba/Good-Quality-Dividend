using System;
using GoodQualityDividend.Interfaces;

namespace GoodQualityDividend.Model
{
    public class Stock : IStock
    {
        public string Company { get; set; }
        public decimal Dividend { get; set; }
        public decimal PricePerShare { get; set; }
        public decimal AnnualRevenue { get; set; }
        public decimal OutStandingNumberOfShares { get; set;}
        public DateTime Year { get; set; }
    }
}