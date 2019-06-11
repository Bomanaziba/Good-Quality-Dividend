using System;

namespace GoodQualityDividend.Interfaces
{
    public interface IDividend
    {
        string Symbol { get; set; }
        decimal Amount { get; set; }
        string Type { get; set; }
        DateTime PaymentDate { get; set; }
    }
}