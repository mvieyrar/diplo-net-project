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
    public class SucursalRepository
    {
        private readonly AppDbContext _appDbContext;

        public SucursalRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        //Metodo para obtener todas las sucursales
        public async Task<List<Sucursal>> GetAllSucursalesAsync()
        {
            //return await Task.FromResult(_appDbContext.Sucursales.ToList());
            List<Sucursal> lista;
            lista = await _appDbContext.Sucursales.ToListAsync();
            return lista;
        }
    }
}
