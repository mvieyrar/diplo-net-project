using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class VendedorFilterViewModel
    {
        public int? ven_id { get; set; }

        [Display(Name = "Patrón de Nombre")]
        public string? ven_nombre_like { get; set; }

        public List<int> SucursalesSeleccionadas { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> SucursalesOptions { get; set; } = new List<SelectListItem>();

        public bool MostrarTodos { get; set; } //Para "Mostrar todos"

        //Para mantener el estado de la paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
