using System.Collections.Generic;

namespace GoodQualityDividend.Interfaces
{
    public interface ICriteriaSortingAlgorithms
    {
         IList<IDividendCriteria> SortByPercentageYield(IList<IDividendCriteria> dividendCriteriaCollection);
         IList<IDividendCriteria> SortByPercentagePayoutRatio(IList<IDividendCriteria> dividendCriteriaCollection);
         IList<IDividendCriteria> SortByPercentage10YrGrowthRate(IList<IDividendCriteria> dividendCriteriaCollection);
         IList<IDividendCriteria> SortBy_5_10_YrGrowthRateRatio(IList<IDividendCriteria> dividendCriteriaCollection);
    }
}