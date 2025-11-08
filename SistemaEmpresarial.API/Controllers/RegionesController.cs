using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaEmpresarial.BusinessLayer.Business;
using SistemaEmpresarial.Core.Dtos;

namespace SistemaEmpresarial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionesController : ControllerBase
    {
        private readonly UnitWork unitWork;

        /*[HttpGet]
        public IActionResult GetRegiones()
        {
        var regiones = new List<RegionDto>
        {
        new RegionDto{ reg_id = 1, reg_nombre = "Norte" },
        new RegionDto{ reg_id = 2, reg_nombre = "Sur" },
        new RegionDto{ reg_id = 3, reg_nombre = "Este" },
        new RegionDto{ reg_id = 4, reg_nombre = "Oeste" }
        };
        return Ok(regiones);
        }*/

        public RegionesController(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegiones()
        {
            List<RegionDto> listaRegiones = new List<RegionDto>();
            listaRegiones = await unitWork.RegionBusiness.GetAll();
            return Ok(listaRegiones);
        }
    }
}
