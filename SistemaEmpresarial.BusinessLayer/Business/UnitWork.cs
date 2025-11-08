using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.BusinessLayer.Business
{
    public class UnitWork
    {

        public UnitWork(RegionBusiness regionBusiness)
        {
            RegionBusiness = regionBusiness;
        }

        public RegionBusiness RegionBusiness { get; }
    }
}
