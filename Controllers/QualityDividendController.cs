using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodQualityDividend.Interfaces;
using GoodQualityDividend.Model;
using Microsoft.AspNetCore.Mvc;

namespace GoodQualityDividend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityDividendController : Controller 
    {

        private readonly IDividendQualityCriteria _dividendQualtiyCriteria;
        private readonly ICriteriaSortingAlgorithm criteriaSortingAlgorithm;

        public QualityDividendController(IDividendQualityCriteria _dividendQualtiyCriteria,
        ICriteriaSortingAlgorithm criteriaSortingAlgorithm)
        {
            this._dividendQualtiyCriteria = _dividendQualtiyCriteria;
            this.criteriaSortingAlgorithm = criteriaSortingAlgorithm;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {

            //Testing the sorting Algorithm
            var list = new List<IDividendCriteria>(){

                new DividendCriteria {
                    Company = "A",
                    Dividend = 0.12M,
                    PricePerStock = 20M,
                    PercentageYield = 4M,
                    PercentagePayoutRatio = 63M,
                    Percentage10yrGrowthRate = 3.4M,
                    _5_10Ratio = 1.4M,
                    Year = DateTime.UtcNow
                },
                new DividendCriteria {
                    Company = "B",
                    Dividend = 0.1M,
                    PricePerStock = 15M,
                    PercentageYield = 10M,
                    PercentagePayoutRatio = 118M,
                    Percentage10yrGrowthRate = 36.7M,
                    _5_10Ratio = 0.3M,
                    Year = DateTime.UtcNow
                },
                new DividendCriteria {
                    Company = "C",
                    Dividend = 0.1M,
                    PricePerStock = 12M,
                    PercentageYield = 4.5M,
                    PercentagePayoutRatio = 65M,
                    Percentage10yrGrowthRate = 6.4M,
                    _5_10Ratio = 1.2M,
                    Year = DateTime.UtcNow
                },
                new DividendCriteria {
                    Company = "D",
                    Dividend = 5M,
                    PricePerStock = 35M,
                    PercentageYield = 2.5M,
                    PercentagePayoutRatio = 43M,
                    Percentage10yrGrowthRate = 13.6M,
                    _5_10Ratio = 1.1M,
                    Year = DateTime.UtcNow
                },
                new DividendCriteria {
                    Company = "E",
                    Dividend = 0.5M,
                    PricePerStock = 15M,
                    PercentageYield = 4.5M,
                    PercentagePayoutRatio = 78M,
                    Percentage10yrGrowthRate = 14.4M,
                    _5_10Ratio = 0.8M,
                    Year = DateTime.UtcNow
                }

            };
            
            //var goodQualityDividend = await this._dividendQualtiyCriteria.GoodQualityDividendList();

            var model = this.criteriaSortingAlgorithm.SortByPercentageYield(list);

            return Ok(model);
        }
    }
}