using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEmpresarial.Data;
using SistemaEmpresarial.Infrastructure;
using SistemaEmpresarial.Models;
using SistemaEmpresarial.Models.ViewModels;

namespace SistemaEmpresarial.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly AppDbContext _context;

        public EmpresasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Empresas
        // Lista con filtro + paginación
        [HttpGet]
        public async Task<IActionResult> Index([Bind(Prefix = "Filtro")] EmpresaFilterViewModel filtro, string? mode)
        {
            //Hidratar combos del filtro
            filtro.ClasificacionesOptions = ClasificacionesOptionsEmpresa();
            filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
            filtro.PageSize = 10;

            IQueryable<Empresa> q = _context.Empresas.AsNoTracking();

            bool aplicarFiltro = mode == "filter" ||
                (!filtro.MostrarTodos &&
                    (filtro.emp_id.HasValue ||
                    !string.IsNullOrWhiteSpace(filtro.emp_nombre_like) ||
                    filtro.emp_fecha_creacion_from.HasValue ||
                    filtro.emp_fecha_creacion_to.HasValue ||
                    (filtro.ClasificacionesSeleccionadas?.Any() ?? false)));

            if (aplicarFiltro)
            {
                if (filtro.emp_id.HasValue)
                    q = q.Where(c => c.emp_id == filtro.emp_id.Value);

                if (!string.IsNullOrWhiteSpace(filtro.emp_nombre_like))
                    q = q.Where(c => EF.Functions.Like(c.emp_nombre, $"%{filtro.emp_nombre_like}%"));

                if (filtro.emp_fecha_creacion_from.HasValue)
                    q = q.Where(c => c.emp_fecha_creacion >= filtro.emp_fecha_creacion_from.Value.Date);

                if (filtro.emp_fecha_creacion_to.HasValue)
                {
                    var hasta = filtro.emp_fecha_creacion_to.Value.Date.AddDays(1).AddTicks(-1);
                    q = q.Where(c => c.emp_fecha_creacion <= hasta);
                }

                if ((filtro.ClasificacionesSeleccionadas != null) && (filtro.ClasificacionesSeleccionadas.Any()))
                {
                    var set = filtro.ClasificacionesSeleccionadas.Select(s => s).ToHashSet();
                    q = q.Where(c => set.Contains((int)c.emp_clasificacioncla_id));

                }

            }
            q = q.OrderBy(c => c.emp_id);
            var paged = await PaginatedList<Empresa>.CreateAsync(q, filtro.Page, filtro.PageSize);
            var vm = new EmpresaIndexViewModel { Filtro = filtro, Resultados = paged };
            return View(vm);

        }

        private static IEnumerable<SelectListItem> ClasificacionesOptionsEmpresa() => new[]
        {
            new SelectListItem { Value = "1", Text = "AAA" },
            new SelectListItem { Value = "2", Text = "A" },
            new SelectListItem { Value = "3", Text = "B" },
            new SelectListItem { Value = "4", Text = "C" }
        };

        // Otros métodos (Create, Edit, Delete, Details) pueden ser implementados aquí según sea necesario

        // GET: Vendedores/Create
        public async Task<IActionResult> Create()
        {
            var vm = await BuildFormVMAsync();
            return View(vm);
        }

        private async Task<EmpresaFormViewModel> BuildFormVMAsync(Empresa? e = null)
        {
            var vm = new EmpresaFormViewModel
            {
                Empresa = e ?? new Empresa { },

                Clasificaciones = await _context.Clasificaciones.AsNoTracking()
                .OrderBy(x => x.cla_id)
                .Select(x => new SelectListItem(x.cla_nombre, x.cla_id.ToString(), e != null && e.emp_clasificacioncla_id == x.cla_id))
                .ToListAsync()
            };

            return vm;
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpresaFormViewModel vm)
        {
            _context.Empresas.Add(vm.Empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var p = await _context.Empresas.FindAsync(id);
            if (p == null) return NotFound();
            var vm = await BuildFormVMAsync(p);
            return View(vm);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmpresaFormViewModel vm)
        {
            if (id != vm.Empresa.emp_id) return BadRequest();

            /*Se comenta porque no estaba guardando
            if(!ModelState.IsValid)
            {
                var reload = await BuildFormVMAsync(vm.Empresa);                
                return View(reload);
            }
            */

            _context.Entry(vm.Empresa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _context.Empresas
                .Include(x => x.emp_clasificacion)
                .FirstOrDefaultAsync(x => x.emp_id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var p = await _context.Empresas
                .Include(x => x.emp_clasificacion)
                .FirstOrDefaultAsync(x => x.emp_id == id);
            if (p == null) return NotFound();
            return View(p);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _context.Empresas.FindAsync(id);
            if (p == null) return NotFound();
            _context.Empresas.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
