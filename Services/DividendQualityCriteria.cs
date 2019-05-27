using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodQualityDividend.Interfaces;
using GoodQualityDividend.Model;
using Newtonsoft.Json;

namespace GoodQualityDividend.Services
{
    public class DividendQualityCriteria : IDividendQualityCriteria
    {
        //Percentage Yield is amount you get per share
        //It should be greater than 4% 
        //But too high should raise a red flag
        public decimal PercentageYield(IStock stock){

            decimal percentageYield =  stock.Dividend/stock.PricePerShare;
            
            return percentageYield;
        }

        //Percentage Payout Ratio Calculate the regularity in which a company pay dividend
        //It is given as DPS/EPS
        //A good quality dividends should have a value less than 70%
        public decimal PercentagePayoutRatio(IStock stock){

            var EarningsPerShare = stock.AnnualRevenue/stock.OutStandingNumberOfShares;

            var payOutRatio = stock.Dividend/EarningsPerShare;

            return payOutRatio;
        }

        //This method Calculate the Average Growth Rate of a company Dividend over a period of 10 years
        //A Good Quality Dividend should be between 6% - 8%
        //6% is okay, but its considered low
        //Greater than 8% is till acceptable. Too high is questionable
        public decimal Percentage10yrGrowthRate(IList<IStock> stockCollection){

            decimal _10YrGrowthRate = 0;
            decimal percentage10YrGrowthRate = 0;

            foreach(var stock in stockCollection){

                //TODO: Calculate 10 Yr Growth Rate

            }
            
            percentage10YrGrowthRate = _10YrGrowthRate/10;

            return percentage10YrGrowthRate;
        }

        //_5_10 Yr Ratio Calculate the Growth Rate for a Share
        //Get the average 5 year growth rate
        //Get the average 10 year growth rate
        //Calculate the Ratio of 5/10 ratio
        //This ratio should be greater than or equal to 1
        //Equal to 1 means its a stable company
        public decimal _5_10yrRatio(IList<IStock> stockCollection){

            var _5YrGrowthRate = 0;
            var _10YrGrowthRate = 0;

            //TODO: Calculate 5/10 Ratio Growth Rate

            var _5_10YrGrowRate = _5YrGrowthRate/_10YrGrowthRate;

            return _5_10YrGrowRate;
        }
        public async Task<IList<IDividendCriteria>> GoodQualityDividendList(){

            //WebAPI that gets Nigeria Stock Exchange market price, dividends and ...
            string stockCollection = string.Empty;

            var dividendCriteriaCollection = new List<IDividendCriteria>();

            var dividendCriteria = new DividendCriteria();

            //Get List of Share Price in the Nigeria stock exchange market 
            var _stockCollection = JsonConvert.DeserializeObject<IStock[]>(stockCollection);

            foreach(var cell in _stockCollection){

                var stockHistory = "" + cell.Company;

                //Get Share Price History of 10 yr period of a specific company
                var _stockHistory = JsonConvert.DeserializeObject<IStock[]>(stockHistory);

                dividendCriteria.Company = cell.Company;
                dividendCriteria.PricePerStock = cell.PricePerShare;
                dividendCriteria.Dividend = cell.Dividend;
                dividendCriteria.Year = cell.Year;
                
                if(cell.Year.Year == DateTime.Now.Year) {

                    dividendCriteria.PercentageYield = PercentageYield(cell);
                    dividendCriteria.PercentagePayoutRatio = PercentagePayoutRatio(cell);

                }

                dividendCriteria.Percentage10yrGrowthRate = Percentage10yrGrowthRate(_stockHistory);

                dividendCriteria._5_10Ratio = _5_10yrRatio(_stockHistory);
                
                dividendCriteriaCollection.Add(dividendCriteria);
            }

            //TODO: Sorting ALgorithm for the four criteria. Based on the set condition


            return dividendCriteriaCollection;
        } 
    }
}