using adotoaspcorewebapi.Data;
using adotoaspcorewebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace adotoaspcorewebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryDataAccess _countryDataAccess;

        public CountryController(CountryDataAccess countryDataAccess)
        {
            _countryDataAccess = countryDataAccess;
        }

        [HttpGet]
        public IActionResult GetAllCountry()
        {
            var responce = new ResponceModel<IEnumerable<CountryMaster>>();
            try
            {
                var country = _countryDataAccess.GetAllCountry();
                if(country != null)
                {
                    responce.Status = (int)HttpStatusCode.OK;
                    responce.Data = country;
                }
            }
            catch (Exception ex)
            {
                responce.Status = (int)HttpStatusCode.BadRequest;
                return BadRequest(ex.Message);
            }
            return Ok(responce);
           
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var responce = new ResponceModel<CountryMaster>();
            try
            {
                var countrys = _countryDataAccess.GetCountryById(id);
                if (countrys == null)
                {
                    responce.Status = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    responce.Status = (int)HttpStatusCode.OK;
                    responce.Data = countrys;
                }  
            }
            catch (Exception ex)
            {
                responce.Status = (int)HttpStatusCode.BadRequest;
                return BadRequest(ex.Message);
            }
            return Ok(responce);
            
        }

        [HttpPost]
        public IActionResult AddCountry(int id, [FromBody] CountryMaster countryMaster)
        {
            var responce = new ResponceModel<string>();
            try
            {
                var country = _countryDataAccess.GetCountryById(id);
                if (country == null)
                {
                    _countryDataAccess.AddCountry(countryMaster);
                    responce.Status = (int)HttpStatusCode.OK;
                    responce.Data = "Data Added";
                    
                }
                else
                {
                    responce.Status = (int)HttpStatusCode.Continue;
                    responce.Data = "Id Exist!";
                }
                
                //return CreatedAtAction(nameof(GetCountryById), new { id = countryMaster.CountryId }, countryMaster);
            }
            catch (Exception ex)
            {
                responce.Status = (int)HttpStatusCode.BadRequest;
                return BadRequest(ex.Message);
            }
        return Ok(responce);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryMaster countryMaster)
        {
            var responce = new ResponceModel<string>();
            try
            {
                var country = _countryDataAccess.GetCountryById(id);
                if (id != countryMaster.CountryId)
                {
                    responce.Status = (int)HttpStatusCode.BadRequest;
                    responce.Data = "Id & parameter Does not Match!";
                }
                else if (country == null)
                {
                    responce.Status = (int)HttpStatusCode.NotFound;
                    responce.Data = "Id Does not Exist!";
                }
                else
                {
                    _countryDataAccess.UpdateCountry(countryMaster);
                    responce.Status = (int)HttpStatusCode.OK;
                    responce.Data = "data updated";
                }
            }
            catch (Exception ex)
            {
                responce.Status = (int)HttpStatusCode.BadRequest;
                return BadRequest(ex.Message);
            }
            return Ok(responce);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletecountry(int id)
        {
            var responce = new ResponceModel<string>();
            try
            {
                var country = _countryDataAccess.GetCountryById(id);
                if (country == null)
                {
                    responce.Status = (int)HttpStatusCode.BadRequest;
                    responce.Data = "Id Does not Exist!";
                }
                else
                {
                    _countryDataAccess.Deletecountry(id);
                    responce.Status = (int)HttpStatusCode.OK;
                    responce.Data = "Data Deleted";
                }
            }
            catch (Exception ex)
            {
                responce.Status = (int)HttpStatusCode.BadRequest;
                return BadRequest(ex.Message);
            }
        return Ok(responce);
 
        }
    }
}
