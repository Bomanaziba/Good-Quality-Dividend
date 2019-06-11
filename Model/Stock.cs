using System;
using GoodQualityDividend.Interfaces;

namespace GoodQualityDividend.Model
{
    public class Stock : IStock
    {
        //Symbol of the Company
        public string Symbol { get; set; }

        //Currency of Stock
        public string Currency { get; set; }

        //Dividend Amount
        public decimal Amount { get; set; }

        //Price Per Share
        public decimal Close { get; set; }

        //Earnings Per Share
        public decimal EPS { get; set; }

        //Type of Market
        public string Asset { get; set; }

        //Year of Stock Details 
        public DateTime Year { get; set; }
    }
}