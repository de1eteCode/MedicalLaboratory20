using DataModels.Interfaces.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MedicalLaboratory20.WebAPI.Controllers
{
#if DEBUG
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly IUnitRoot _unitRoot;

        public ValueController(IUnitRoot unitRoot)
        {
            _unitRoot = unitRoot;
        }

        [HttpGet]
        [Route("getuser")]
        public IActionResult GetUser()
        {
            return Ok(_unitRoot.Users.GetAll());
        }

        [HttpGet]
        [Route("getpatient")]
        public IActionResult GetPatient()
        {
            return Ok(_unitRoot.Patients.GetAll().First());
        }
    }
#endif
}
