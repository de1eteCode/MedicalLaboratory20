using DataModels.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_unit.Users.GetAll());
        }
    }
}
