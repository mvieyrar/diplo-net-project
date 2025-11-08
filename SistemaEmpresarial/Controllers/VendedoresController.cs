using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEmpresarial.Data;
using SistemaEmpresarial.Infrastructure;
using SistemaEmpresarial.Models;
using SistemaEmpresarial.Models.ViewModels;

namespace SistemaEmpresarial.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly AppDbContext _context;

        public VendedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vendedores
        // Lista con filtro + paginación
        [HttpGet]
        public async Task<IActionResult> Index([Bind(Prefix = "Filtro")] VendedorFilterViewModel filtro, string? mode)
        {
            //Hidratar combos del filtro
            filtro.SucursalesOptions = SucursalesOptionsVendedor();
            filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
            filtro.PageSize = 10;

            IQueryable<Vendedor> q = _context.Vendedores.AsNoTracking();

            bool aplicarFiltro = mode == "filter" ||
                (!filtro.MostrarTodos &&
                    (filtro.ven_id.HasValue ||
                    !string.IsNullOrWhiteSpace(filtro.ven_nombre_like) ||                    
                    (filtro.SucursalesSeleccionadas?.Any() ?? false)));

            if (aplicarFiltro)
            {
                if (filtro.ven_id.HasValue)
                    q = q.Where(c => c.ven_id == filtro.ven_id.Value);

                if (!string.IsNullOrWhiteSpace(filtro.ven_nombre_like))
                    q = q.Where(c => EF.Functions.Like(c.ven_nombre, $"%{filtro.ven_nombre_like}%"));                

                if ((filtro.SucursalesSeleccionadas != null) && (filtro.SucursalesSeleccionadas.Any()))
                {                    
                    var set = filtro.SucursalesSeleccionadas.Select(s => s).ToHashSet();
                    q = q.Where(c => set.Contains((int)c.ven_sucursalsuc_id));                   

                }

            }
            q = q.OrderBy(c => c.ven_id);
            var paged = await PaginatedList<Vendedor>.CreateAsync(q, filtro.Page, filtro.PageSize);
            var vm = new VendedorIndexViewModel { Filtro = filtro, Resultados = paged };
            return View(vm);

        }

        private static IEnumerable<SelectListItem> SucursalesOptionsVendedor() => new[]
        {
            new SelectListItem { Value = "1", Text = "MEX" },
            new SelectListItem { Value = "2", Text = "GDL" },
            new SelectListItem { Value = "3", Text = "MTY" },
            new SelectListItem { Value = "4", Text = "TIJ" }
        };

        // Otros métodos (Create, Edit, Delete, Details) pueden ser implementados aquí según sea necesario
        
        // GET: Vendedores/Create
        public async Task<IActionResult> Create()
        {
            var vm = await BuildFormVMAsync();
            return View(vm);
        }

        private async Task<VendedorFormViewModel> BuildFormVMAsync(Vendedor? v = null)
        {
            var vm = new VendedorFormViewModel
            {
                Vendedor = v ?? new Vendedor { },

                Sucursales = await _context.Sucursales.AsNoTracking()
                .OrderBy(x => x.suc_id)
                .Select(x => new SelectListItem(x.suc_nombre, x.suc_id.ToString(), v != null && v.ven_sucursalsuc_id == x.suc_id))
                .ToListAsync()
            };
                        
            return vm;
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendedorFormViewModel vm)
        {            
            _context.Vendedores.Add(vm.Vendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var p = await _context.Vendedores.FindAsync(id);
            if (p == null) return NotFound();
            var vm = await BuildFormVMAsync(p);
            return View(vm);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VendedorFormViewModel vm)
        {
            if (id != vm.Vendedor.ven_id) return BadRequest();
              
            /*Se comenta porque no estaba guardando
            if(!ModelState.IsValid)
            {
                var reload = await BuildFormVMAsync(vm.Vendedor);                
                return View(reload);
            }
            */
            
            _context.Entry(vm.Vendedor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _context.Vendedores
                .Include(x => x.ven_sucursal)
                .FirstOrDefaultAsync(x => x.ven_id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {            
            var p = await _context.Vendedores
                .Include(x => x.ven_sucursal)
                .FirstOrDefaultAsync(x => x.ven_id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _context.Vendedores.FindAsync(id);
            if (p == null) return NotFound();
            _context.Vendedores.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
