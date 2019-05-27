using System.Threading.Tasks;
using GoodQualityDividend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodQualityDividend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityDividendController : Controller 
    {

        private readonly IDividendQualityCriteria _dividendQualtiyCriteria;

        public QualityDividendController(IDividendQualityCriteria _dividendQualtiyCriteria)
        {
            this._dividendQualtiyCriteria = _dividendQualtiyCriteria;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var goodQualityDividend = await this._dividendQualtiyCriteria.GoodQualityDividendList();

            return Ok(goodQualityDividend);
        }
    }
}