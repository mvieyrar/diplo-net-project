using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class EmpresaFilterViewModel
    {
        public int? emp_id { get; set; }

        [Display(Name = "Patrón de Nombre")]
        public string? emp_nombre_like { get; set; }

        [Display(Name = "Alta desde"), DataType(DataType.Date)]
        public DateTime? emp_fecha_creacion_from { get; set; }

        [Display(Name = "Alta hasta"), DataType(DataType.Date)]
        public DateTime? emp_fecha_creacion_to { get; set; }

        public List<int> ClasificacionesSeleccionadas { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> ClasificacionesOptions { get; set; } = new List<SelectListItem>();

        public bool MostrarTodos { get; set; } //Para "Mostrar todos"

        //Para mantener el estado de la paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
