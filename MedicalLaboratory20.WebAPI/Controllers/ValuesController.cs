using DataModels.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MedicalLaboratory20.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            var user = _unit.Users.GetAll().First();
            return Ok(user);
        }

        [HttpGet("getrole")]
        public IActionResult GetRole()
        {
            var role = _unit.Roles.GetAll().First();
            return Ok(role);
        }

        [HttpGet("getpatient")]
        public IActionResult GetPatient()
        {
            return Ok(_unit.Patients.GetAll().First());
        }
    }
}
