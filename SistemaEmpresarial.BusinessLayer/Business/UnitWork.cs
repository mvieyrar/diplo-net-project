using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.BusinessLayer.Business
{
    public class UnitWork
    {
        public RegionBusiness RegionBusiness { get; }

        public SucursalBusiness SucursalBusiness { get; }

        public UnitWork(RegionBusiness regionBusiness, SucursalBusiness sucursalBusiness)
        {
            RegionBusiness = regionBusiness;
            SucursalBusiness = sucursalBusiness;
        }
        
    }
}
