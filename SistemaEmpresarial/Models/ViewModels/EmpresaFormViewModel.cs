using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class EmpresaFormViewModel
    {
        [Required]
        public Empresa Empresa { get; set; } = new Empresa();

        //Listas para selects/radios/checkboxes
        public IEnumerable<SelectListItem> Clasificaciones { get; set; } = new List<SelectListItem>();
    }
}
