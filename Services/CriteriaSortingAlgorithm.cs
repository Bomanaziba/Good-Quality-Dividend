using System.Collections.Generic;
using GoodQualityDividend.Interfaces;
using System.Threading.Tasks;
using System;

namespace GoodQualityDividend.Services
{
    public class CriteriaSortingAlgorithm : ICriteriaSortingAlgorithm
    {
        //Percentage Yield should be above 4%.
        //That is it is sorted in descending order
        public IList<IDividendCriteria> SortByPercentageYield(IList<IDividendCriteria> dividendCriteriaCollection)
        {
            //var sortByPercentageYield = new List<IDividendCriteria>();
            if(dividendCriteriaCollection != null && dividendCriteriaCollection.Count > 0){

                for(int i = 0; i<dividendCriteriaCollection.Count-1; i++)
                {
                    for (int j = 0; j < dividendCriteriaCollection.Count - 1; j++)
                    {
                        if (dividendCriteriaCollection[j].PercentageYield < dividendCriteriaCollection[j + 1].PercentageYield)
                        {

                            var tempData = dividendCriteriaCollection[j];
                            dividendCriteriaCollection[j] = dividendCriteriaCollection[j + 1];
                            dividendCriteriaCollection[j + 1] = tempData;

                        }
                    }
                }   
            }

            return dividendCriteriaCollection;
        }

        //Percecntage Payout Ratio should be less than 70% 
        public IList<IDividendCriteria> SortByPercentagePayoutRatio(IList<IDividendCriteria> dividendCriteriaCollection)
        {
            //var sortByPercentageYield = new List<IDividendCriteria>();
            if(dividendCriteriaCollection != null && dividendCriteriaCollection.Count > 0){

                for(int i = 0; i<dividendCriteriaCollection.Count-1; i++)
                {
                    for (int j = 0; j < dividendCriteriaCollection.Count - 1; j++)
                    {
                        if (dividendCriteriaCollection[j].PercentagePayoutRatio > dividendCriteriaCollection[j + 1].PercentagePayoutRatio)
                        {
                            var tempData = dividendCriteriaCollection[j];
                            dividendCriteriaCollection[j] = dividendCriteriaCollection[j + 1];
                            dividendCriteriaCollection[j + 1] = tempData;

                        }
                    }
                }   
            }
            return dividendCriteriaCollection;
        }

        //Percentage 10Yr Growth Rate is acceptable between 6% - 8%
        //It is sorted in descending order
        //for 
        public IList<IDividendCriteria> SortByPercentage10YrGrowthRate(IList<IDividendCriteria> dividendCriteriaCollection){
            //var sortByPercentageYield = new List<IDividendCriteria>();
            if(dividendCriteriaCollection != null && dividendCriteriaCollection.Count > 0){

                for(int i = 0; i<dividendCriteriaCollection.Count-1; i++)
                {
                    for (int j = 0; j < dividendCriteriaCollection.Count - 1; j++)
                    {
                        if (dividendCriteriaCollection[j].Percentage10yrGrowthRate < dividendCriteriaCollection[j + 1].Percentage10yrGrowthRate)
                        {

                            var tempData = dividendCriteriaCollection[j];
                            dividendCriteriaCollection[j] = dividendCriteriaCollection[j + 1];
                            dividendCriteriaCollection[j + 1] = tempData;

                        }
                    }
                }   
            }

            return dividendCriteriaCollection;
        }

        //This ratio should be greater than or equal to 1
        //Equal to 1 means its a stable company
        //It is sort in descending order
        public IList<IDividendCriteria> SortBy_5_10_YrGrowthRateRatio(IList<IDividendCriteria> dividendCriteriaCollection){
            //var sortByPercentageYield = new List<IDividendCriteria>();
            if(dividendCriteriaCollection != null && dividendCriteriaCollection.Count > 0){

                for(int i = 0; i<dividendCriteriaCollection.Count-1; i++)
                {
                    for (int j = 0; j < dividendCriteriaCollection.Count - 1; j++)
                    {
                        if (dividendCriteriaCollection[j]._5_10Ratio < dividendCriteriaCollection[j + 1]._5_10Ratio)
                        {

                            var tempData = dividendCriteriaCollection[j];
                            dividendCriteriaCollection[j] = dividendCriteriaCollection[j + 1];
                            dividendCriteriaCollection[j + 1] = tempData;

                        }
                    }
                }   
            }

            return dividendCriteriaCollection;
        }

        
    }
}