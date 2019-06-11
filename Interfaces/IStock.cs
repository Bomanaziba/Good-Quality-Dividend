using System;

namespace GoodQualityDividend.Interfaces
{
    public interface IStock
    {
        string Symbol { get; set; }
        string Currency { get; set; } 
        decimal Amount { get; set; }
        decimal Close { get; set; }
        decimal EPS { get; set; }
        string Asset { get; set; }
        DateTime Year { get; set; }
    }
}