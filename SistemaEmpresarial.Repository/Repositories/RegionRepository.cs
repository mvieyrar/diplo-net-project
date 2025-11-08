using Microsoft.EntityFrameworkCore;
using SistemaEmpresarial.Repository.Contexts;
using SistemaEmpresarial.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEmpresarial.Repository.Repositories
{
    public class RegionRepository
    {
        private readonly AppDbContext _appDbContext;

        public RegionRepository(AppDbContext appDbContext1)
        {
            this._appDbContext = appDbContext1;
        }

        //Metodo para obtener todas las regiones
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            //return await Task.FromResult(_appDbContext.Regiones.ToList());
            List<Region> lista;
            lista = await _appDbContext.Regiones.ToListAsync();
            return lista;
        }
    }
}
