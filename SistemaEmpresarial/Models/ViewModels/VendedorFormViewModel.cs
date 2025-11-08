using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class VendedorFormViewModel
    {
        [Required]
        public Vendedor Vendedor { get; set; } = new Vendedor();

        //Listas para selects/radios/checkboxes
        public IEnumerable<SelectListItem> Sucursales { get; set; } = new List<SelectListItem>();
    }
}
