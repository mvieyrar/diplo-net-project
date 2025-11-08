using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.Repository.Repositories
{
    public class GeneralRepository
    {
        
        public GeneralRepository(RegionRepository regionRepository) { 
            Region = regionRepository;
        }

        public RegionRepository Region { get; set; }
    }
}
