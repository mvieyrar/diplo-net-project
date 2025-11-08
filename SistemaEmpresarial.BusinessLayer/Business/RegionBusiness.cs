using SistemaEmpresarial.Core.Dtos;
using SistemaEmpresarial.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.BusinessLayer.Business
{
    public class RegionBusiness
    {
        private readonly GeneralRepository _generalRepository;

        public RegionBusiness(GeneralRepository generalRepository) 
        {
            this._generalRepository = generalRepository;
        }

        public async Task<List<RegionDto>> GetAll()
        {
            List<RegionDto> listaRegionDto = new List<RegionDto>();

            var regiones = await _generalRepository.Region.GetAllRegionsAsync();

            listaRegionDto = regiones.Select(r => new RegionDto
            {
                reg_id = r.reg_id,
                reg_nombre = r.reg_nombre
            }).ToList();

            return listaRegionDto;
        }
    }
}
