using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.Repository.Repositories
{
    public class GeneralRepository
    {
        public RegionRepository Region { get; set; }

        public SucursalRepository Sucursal { get; set; }

        public GeneralRepository(RegionRepository regionRepository, SucursalRepository sucursalRepository) { 
            Region = regionRepository;
            Sucursal = sucursalRepository;
        }

        
    }
}
