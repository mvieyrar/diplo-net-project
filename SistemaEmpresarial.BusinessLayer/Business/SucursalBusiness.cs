using SistemaEmpresarial.Core.Dtos;
using SistemaEmpresarial.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.BusinessLayer.Business
{
    public class SucursalBusiness
    {
        private readonly GeneralRepository _generalRepository;

        public SucursalBusiness(GeneralRepository generalRepository)
        {
            this._generalRepository = generalRepository;
        }

        public async Task<List<SucursalDto>> GetAll()
        {
            List<SucursalDto> listaSucursalDto = new List<SucursalDto>();

            var sucursales = await _generalRepository.Sucursal.GetAllSucursalesAsync();

            listaSucursalDto = sucursales.Select(s => new SucursalDto
            {
                suc_id = s.suc_id,
                suc_nombre = s.suc_nombre
            }).ToList();
            
            return listaSucursalDto;
        }
    }
}
