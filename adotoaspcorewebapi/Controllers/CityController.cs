using adotoaspcorewebapi.Data;
using adotoaspcorewebapi.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace adotoaspcorewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly CityDataAccess _CityDataAccess;

        public CityController(CityDataAccess cityDataAccess)
        {
            _CityDataAccess = cityDataAccess;
        }

        [HttpGet]
        public IActionResult getallCity()
        {

            var citys = _CityDataAccess.getallCity();
            return CustomResult("Data Fetched", citys, HttpStatusCode.OK);

        }

        [HttpGet("{id}")]
        public IActionResult getallCitybyid(int id)
        {
            var country = _CityDataAccess.getallCitybyid(id);
            if (country == null)
            {
                return CustomResult("data not found", HttpStatusCode.NotFound);
            }
            else
            {
                var citys = _CityDataAccess.getallCitybyid(id);
                return CustomResult("Data Fetched", citys, HttpStatusCode.OK);
            }
            
        }


        [HttpPost]
        public IActionResult insertCitymaster([FromBody] CityMaster cityMaster)
        {

            _CityDataAccess.insertCitymaster((string)cityMaster.CityName, (int)cityMaster.StateId, (int)cityMaster.CountryId);
            return CustomResult("Data added", HttpStatusCode.OK);

        }


        [HttpPut("{id}")]
        public IActionResult updateCitymaster(int id, [FromBody] CityMaster cityMaster)
        {

            if (id != cityMaster.CityId)
            {
                return CustomResult(HttpStatusCode.BadRequest);
            }
            var country = _CityDataAccess.getallCitybyid(id);
            if (country == null)
            {
                return CustomResult("data does not exits", HttpStatusCode.NotFound);
            }
            _CityDataAccess.updateCitymaster((int)cityMaster.CityID, (string)cityMaster.CityName);
            return CustomResult("data updated", HttpStatusCode.OK);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteCitymaster(int id)
        {
            var country = _CityDataAccess.getallCitybyid(id);
            if (country == null)
            {
                return CustomResult("data not found", HttpStatusCode.NotFound);
            }
            else
            {
                _CityDataAccess.deleteCitymaster(id);
                return CustomResult("data deleted", HttpStatusCode.OK);
            }
        }
    }

}
