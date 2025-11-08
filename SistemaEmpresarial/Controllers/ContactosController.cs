using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaEmpresarial.Data;
using SistemaEmpresarial.Infrastructure;
using SistemaEmpresarial.Models;
using SistemaEmpresarial.Models.ViewModels;

namespace SistemaEmpresarial.Controllers
{
    public class ContactosController : Controller
    {
        private readonly AppDbContext _context;

        public ContactosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Contactos
        // Lista con filtro + paginación
        [HttpGet]
        public async Task<IActionResult> Index([Bind(Prefix = "Filtro")] ContactoFilterViewModel filtro, string? mode)
        {            
            //Hidratar combos del filtro
            filtro.RegionesOptions = RegionesOptionsContacto();
            filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
            filtro.PageSize = 10;

            IQueryable<Contacto> q = _context.Contactos.AsNoTracking();

            bool aplicarFiltro = mode == "filter" || 
                (!filtro.MostrarTodos && 
                    (filtro.con_id.HasValue || 
                    !string.IsNullOrWhiteSpace(filtro.con_nombre_like) ||
                    filtro.con_fecha_creacion_from.HasValue ||
                    filtro.con_fecha_creacion_to.HasValue ||
                    (filtro.RegionesSeleccionadas?.Any() ?? false)));

            if (aplicarFiltro)
            {
                if(filtro.con_id.HasValue)
                    q = q.Where(c => c.con_id == filtro.con_id.Value);

                if (!string.IsNullOrWhiteSpace(filtro.con_nombre_like))
                    q = q.Where(c => EF.Functions.Like(c.con_nombre, $"%{filtro.con_nombre_like}%"));

                if (filtro.con_fecha_creacion_from.HasValue)
                    q = q.Where(c => c.con_fecha_creacion >= filtro.con_fecha_creacion_from.Value.Date);

                if (filtro.con_fecha_creacion_to.HasValue)
                {
                    var hasta = filtro.con_fecha_creacion_to.Value.Date.AddDays(1).AddTicks(-1);
                    q = q.Where(c => c.con_fecha_creacion <= hasta);
                }

                if ((filtro.RegionesSeleccionadas != null) && (filtro.RegionesSeleccionadas.Any()))
                {
                    //var set = filtro.AreaInteresSeleccionadas.Select(s => s[0]).ToHashSet();
                    var set = filtro.RegionesSeleccionadas.Select(s => s).ToHashSet();
                    q = q.Where(c => set.Contains((int)c.con_regionreg_id));
                    //q = q.Where(c => c.Contains(c.con_area_interes));

                }
                       
            }
            q = q.OrderBy(c => c.con_id);
            var paged = await PaginatedList<Contacto>.CreateAsync(q, filtro.Page, filtro.PageSize);
            var vm = new ContactoIndexViewModel { Filtro = filtro, Resultados = paged };
            return View(vm);
            
        }

        private static IEnumerable<SelectListItem> RegionesOptionsContacto() => new[]
            {
                new SelectListItem { Value = "1", Text = "NORTE" },
                new SelectListItem { Value = "2", Text = "SUR" },
                new SelectListItem { Value = "3", Text = "ESTE" },
                new SelectListItem { Value = "4", Text = "OESTE" }
            };
           

        /*
        public async Task<IActionResult> Index()
        {
            //var lista = await _context.Contactos.OrderBy(c => c.con_id).ToListAsync();
            var lista = await _context.Contactos
                .Include(p => p.con_region)
                .ToListAsync();
            return View(lista);
        //return View(await _context.Contactos.ToListAsync());
        
        }*/

        // GET: Contactos/Create
        public async Task<IActionResult> Create()
        {
            var vm = await BuildFormVMAsync();
            return View(vm);
        }

        private async Task<ContactoFormViewModel> BuildFormVMAsync(Contacto? c = null)
        {
            var vm = new ContactoFormViewModel
            {
                Contacto = c ?? new Contacto { },

                Regiones = await _context.Regiones.AsNoTracking()
                .OrderBy(x => x.reg_id)
                .Select(x => new SelectListItem(x.reg_nombre, x.reg_id.ToString(), c != null && c.con_regionreg_id == x.reg_id))
                .ToListAsync(),

                AreaInteresOptions = Enum.GetValues<AreaInteres>()
                .Where(f => f != AreaInteres.NONE)
                .Select(f => new SelectListItem(f.ToString(), ((int)f).ToString(),
                    c != null && c.con_area_interes.HasFlag(f)))
                .ToList()
            };

            // Preselecciónpara multi-select de areas de interes
            vm.AreaInteresSeleccionadas = c == null ? Array.Empty<int>() : Enum.GetValues<AreaInteres>()
                .Where(f => f != AreaInteres.NONE && c.con_area_interes.HasFlag(f))
                .Select(f => (int)f).ToArray();
                



            return vm;
        }

        // POST: Contactos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactoFormViewModel vm)
        {
            //Reconstruir banderas desde el multiselect
            vm.Contacto.con_area_interes = (AreaInteres)(vm.AreaInteresSeleccionadas?.Sum(v => v) ?? 0);                    
            /*Se comenta porque no dejaba guardar
            if(!ModelState.IsValid)
            {                
                //Recargar listas si hay error.
                var reload = await BuildFormVMAsync(vm.Contacto);
                reload.AreaInteresSeleccionadas = vm.AreaInteresSeleccionadas;
                return View(reload);
            }
            */
            _context.Contactos.Add(vm.Contacto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Contactos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var p  = await _context.Contactos.FindAsync(id);
            if(p == null) return NotFound();
            var vm = await BuildFormVMAsync(p);
            return View(vm);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactoFormViewModel vm)
        {
            if (id != vm.Contacto.con_id) return BadRequest();

            vm.Contacto.con_area_interes = (AreaInteres)(vm.AreaInteresSeleccionadas?.Sum(v => v) ?? 0);

            /*Se comenta porque no estaba guardando
            if(!ModelState.IsValid)
            {
                var reload = await BuildFormVMAsync(vm.Contacto);
                reload.AreaInteresSeleccionadas = vm.AreaInteresSeleccionadas;
                return View(reload);
            }
            */

            _context.Entry(vm.Contacto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var p = await _context.Contactos
                .Include(x => x.con_region)
                .FirstOrDefaultAsync(x => x.con_id == id);
            if(p == null) return NotFound();
            return View(p);
        }
        
        // GET: Contactos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            /*
            var p = await _context.Contactos.FindAsync(id);
            if (p == null) return NotFound();            
            return View(p);
            */
            var p = await _context.Contactos
                .Include(x => x.con_region)
                .FirstOrDefaultAsync(x => x.con_id == id);
            if (p == null) return NotFound();
            return View(p);
        }
        
        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _context.Contactos.FindAsync(id);
            if (p == null) return NotFound();
            _context.Contactos.Remove(p);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /* Se comentan las acciones
        // GET: Contactos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.con_id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }
        /*
        // GET: Contactos/Create
        public IActionResult Create()
        {
            var model = new Contacto
            {
                
            };
            CargarListasUI(model);
            //return View();
            return View(model);
        }
        *//*

        // GET: Contactos/Create
        public async Task<IActionResult> Create()
        {
            var vm = await BuildFormVMAsync();
            return View(vm);
        }

        private async Task<ContactoFormViewModel> BuildFormVMAsync(Contacto? c = null)
        {
            var vm = new ContactoFormViewModel
            {
                Contacto = c ?? new Contacto { },

                Regiones = await _context.Regiones.AsNoTracking()
                .OrderBy(x => x.reg_id)
                .Select(x => new SelectListItem(x.reg_nombre, x.reg_id.ToString(), c != null && c.con_regionreg_id == x.reg_id))
                .ToListAsync()
            };
            return vm;
        }

        private void CargarListasUI(Contacto? model = null)
        {
            
            //a.	Dropdown List usando una lista de código
            ViewBag.Regiones = new SelectList(new[]
            {
                new { Value = 1, Text = "NORTE" },
                new { Value = 2, Text = "SUR" },
                new { Value = 3, Text = "ESTE" },
                new { Value = 4, Text = "OESTE" }
            },"Value","Text", model?.con_region);

            //e.	Lista de selección múltple
            ViewBag.AreaInteresOptions = Enum.GetValues<AreaInteres>()
                .Where(f => f != AreaInteres.NONE)
                .Select(f => new SelectListItem(f.ToString(), ((int) f).ToString(),
                model != null && model.con_area_interes.HasFlag (f))).ToList();
            
        }


        // POST: Contactos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("con_id,con_nombre,con_paterno,con_materno,con_empresa,con_region,con_cargo,con_red_social,con_tipo_red_social,con_genero,con_disponibilidad_matutino,con_disponibilidad_vespertino,con_fecha_creacion,con_peso,con_area_interes,con_comentario,con_estatus")] Contacto contacto, int[]? AreaInteresSeleccionadas)
        {
            //Combinar seleccion multiple en banderas            
            if (AreaInteresSeleccionadas != null && AreaInteresSeleccionadas.Length > 0)
            {
                contacto.con_area_interes = AreaInteresSeleccionadas.Select(v => (AreaInteres)v).Aggregate(AreaInteres.NONE, (acc, f) => acc | f);
            }
            else contacto.con_area_interes = AreaInteres.NONE;
            

            if (ModelState.IsValid)
            {
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }
            CargarListasUI(contacto);
            return View(contacto);
        }

        // POST: Contactos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("con_id,con_nombre,con_paterno,con_materno,con_empresa,con_region,con_cargo,con_red_social,con_tipo_red_social,con_genero,con_disponibilidad_matutino,con_disponibilidad_vespertino,con_fecha_creacion,con_peso,con_comentario,con_estatus")] Contacto contacto, int[]? AreaInteresSeleccionadas)
        {
            if (id != contacto.con_id)
            {                
                return BadRequest();
            }

            //Este funciona            
            if (AreaInteresSeleccionadas != null && AreaInteresSeleccionadas.Length > 0)
            {
                contacto.con_area_interes = AreaInteresSeleccionadas.Select(v => (AreaInteres)v).Aggregate(AreaInteres.NONE, (acc, f) => acc | f);
            }
            else contacto.con_area_interes = AreaInteres.NONE;
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contacto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.con_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // GET: Contactos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacto = await _context.Contactos
                .FirstOrDefaultAsync(m => m.con_id == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return View(contacto);
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto != null)
            {
                _context.Contactos.Remove(contacto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id)
        {
            return _context.Contactos.Any(e => e.con_id == id);
        }*/
    }
}
