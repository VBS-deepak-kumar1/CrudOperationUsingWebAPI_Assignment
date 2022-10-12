using BackEndWebAPI.Repositories;
using DataAccessLayer;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndWebAPI.Controllers
{
    [Route("api/[Controller]")]
    public class DuserController : ControllerBase
    {
        private readonly IDuserRepository _duserRepository;
        public DuserController(IDuserRepository duserRepository)
        {
            _duserRepository = duserRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetDusers()
    {

        try
        {
            return Ok(await _duserRepository.GetDusers());
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving from Database");
        }
    }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Duser>> GetDuser(int Id)
        {
            try
            {
                var result = await _duserRepository.GetDuser(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Duser>> CreateDuser([FromBody] Duser duser)
        {
          
            try
            {
                if (duser == null)
                {
                    return BadRequest();
                }
                var CreateDuser = await _duserRepository.AddDuser(duser);
                return CreatedAtAction(nameof(GetDuser), new { id = CreateDuser.DuserID }, CreateDuser);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Duser>> UpdateDuser(int Id, [FromBody] Duser duser)
        {
            try
            {
                if (Id != duser.DuserID)
                {
                    return BadRequest("Id Mismatch");
                }
                var duserUpdate = await _duserRepository.GetDuser(Id);
                if (duserUpdate == null)
                {
                    return NotFound($"Duser Id ={Id} not Found");
                }
               return  await _duserRepository.UpdateDuser(duser);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in Retrieving Data from Database");
            }
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Duser>> DeleteDuser(int Id)
        {
            try
            {
                var duserDelete = await _duserRepository.GetDuser(Id);
                if (duserDelete == null)
                {
                    return NotFound($"Duser Id={Id} not found");
                }
                return await _duserRepository.DeleteDuser(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
