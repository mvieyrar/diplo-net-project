using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class ContactoFilterViewModel
    {
        public int? con_id { get; set; }

        [Display(Name = "Patrón de Nombre")]
        public string? con_nombre_like { get; set; }
        
        [Display(Name = "Alta desde"), DataType(DataType.Date)]
        public DateTime? con_fecha_creacion_from { get; set; }

        [Display(Name = "Alta hasta"), DataType(DataType.Date)]
        public DateTime? con_fecha_creacion_to { get; set; }

        //Filtros adicionales pueden ser agregados aquí
        //public List<int> AreaInteresSeleccionadas { get; set; } = new List<int>();

        //public IEnumerable<SelectListItem> AreaInteresOptions { get; set; } = new List<SelectListItem>();

        public List<int> RegionesSeleccionadas { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> RegionesOptions { get; set; } = new List<SelectListItem>();

        public bool MostrarTodos { get; set; } //Para "Mostrar todos"

        //Para mantener el estado de la paginación
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
