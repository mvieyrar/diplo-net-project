using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaEmpresarial.BusinessLayer.Business;
using SistemaEmpresarial.Core.Dtos;

namespace SistemaEmpresarial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly UnitWork unitWork;
        public SucursalesController(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetSucursales()
        {
            List<SucursalDto> listaSucursales = new List<SucursalDto>();
            listaSucursales = await unitWork.SucursalBusiness.GetAll();
            return Ok(listaSucursales);
        }
    }
}
