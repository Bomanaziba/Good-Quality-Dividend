using GoodQualityDividend.Interfaces;
using System;

namespace GoodQualityDividend.Model
{
    public class Dividend : IDividend
    {
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}