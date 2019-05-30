using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodQualityDividend.Interfaces;
using GoodQualityDividend.Model;
using Newtonsoft.Json;

namespace GoodQualityDividend.Services
{
    public class DividendQualityCriteria : IDividendQualityCriteria
    {
        private readonly CriteriaSortingAlgorithm criteriaSort;

        public DividendQualityCriteria(CriteriaSortingAlgorithm criteriaSort)
        {
            this.criteriaSort = criteriaSort;
        }

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
            int YearPrev = 0;
            decimal DividendPrev = 0;

            foreach(var stock in stockCollection){

                decimal growthRate = 0;

                if(YearPrev > 0 && DividendPrev > 0) {
                    growthRate = ((stock.Dividend - DividendPrev)/DividendPrev) * 100;
                }
                
                YearPrev = stock.Year.Year;
                DividendPrev = stock.Dividend;

                _10YrGrowthRate += growthRate;
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

            decimal _5YrGrowthRate = 0;
            decimal _10YrGrowthRate = 0;
            int YearPrev = 0;
            decimal DividendPrev = 0;
            int count = 0;

            foreach(var stock in stockCollection){

                decimal growthRate = 0;

                if(YearPrev > 0 && DividendPrev > 0) {
                    growthRate = ((stock.Dividend - DividendPrev)/DividendPrev) * 100;
                }

                if(count <= 5){
                    _5YrGrowthRate += growthRate;
                }

                _10YrGrowthRate += growthRate;

                YearPrev = stock.Year.Year;
                DividendPrev = stock.Dividend;

            }

            decimal _5_10YrGrowRate = _5YrGrowthRate/_10YrGrowthRate;

            return _5_10YrGrowRate;
        }
        
        //Get a sorted list of Good quality dividends by ascending order based on the criteria
        public IList<IDividendCriteria> GoodQualityDividendList(IList<IDividendCriteria> stock){

            //WebAPI that gets Nigeria Stock Exchange market price, dividends and ...
 /*           string stockCollection = string.Empty;

            var dividendCriteriaCollection = new List<IDividendCriteria>();
 
            var dividendCriteria = new DividendCriteria();

            //Get List of Share Price in the Nigeria stock exchange market 
            var _stockCollection = JsonConvert.DeserializeObject<IStock[]>(stockCollection);

            foreach(var cell in _stockCollection){

                //Stock exchange WebAPI from Nigeria Stock Exchange Market for Stock 10 Years Stock History
                var stockHistory = "" + cell.Company;

                //Get Share Price History of 10 yr period of a specific company
                var _stockHistory = JsonConvert.DeserializeObject<IStock[]>(stockHistory);

                dividendCriteria.Company = cell.Company;
                dividendCriteria.PricePerStock = cell.PricePerShare;
                dividendCriteria.Dividend = cell.Dividend;
                dividendCriteria.Year = cell.Year;
                dividendCriteria.Rating = 0;
                
                if(cell.Year.Year == DateTime.Now.Year) {

                    dividendCriteria.PercentageYield = PercentageYield(cell);
                    dividendCriteria.PercentagePayoutRatio = PercentagePayoutRatio(cell);

                }

                dividendCriteria.Percentage10yrGrowthRate = Percentage10yrGrowthRate(_stockHistory);

                dividendCriteria._5_10Ratio = _5_10yrRatio(_stockHistory);
                
                dividendCriteriaCollection.Add(dividendCriteria);
            }
 */
            //Sorting ALgorithm for the four criteria. Based on the set condition
            
            var sortByPercentageYield = this.criteriaSort.SortByPercentageYield(stock);
            var sortedByPercentagePayoutRatio = this.criteriaSort.SortByPercentagePayoutRatio(stock);
            var sortedByPercentage10YrGrowthRate = this.criteriaSort.SortByPercentage10YrGrowthRate(stock);
            var sortedByPercentage_5_10YrRate = this.criteriaSort.SortBy_5_10_YrGrowthRateRatio(stock);

            for(int i = 0; i < stock.Count; i++){

                foreach(var cells in sortByPercentageYield){
                    if(stock[i].Company == cells.Company) 
                        //dividendCriteriaCollection[i].Rating++;
                        stock[i].Rating++;
                        break;
                }   

                foreach(var container in sortedByPercentagePayoutRatio){
                    if(stock[i].Company == container.Company) 
                        //dividendCriteriaCollection[i].Rating++;
                        stock[i].Rating++;
                        break;
                }

                foreach(var pocket in sortedByPercentage10YrGrowthRate){
                    if(stock[i].Company == pocket.Company) 
                        //dividendCriteriaCollection[i].Rating++;
                        stock[i].Rating++;
                        break;
                }

                foreach(var box in sortedByPercentage_5_10YrRate){
                    if(stock[i].Company == box.Company) 
                        //dividendCriteriaCollection[i].Rating++;
                        stock[i].Rating++;
                        break;
                }

            }

            stock.OrderBy(p=>p.Rating);
            
            return stock;

            //dividendCriteriaCollection.OrderBy(p=>p.Rating);

            //return dividendCriteriaCollection;
        } 
    }
}