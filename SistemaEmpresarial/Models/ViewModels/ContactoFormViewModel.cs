using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class ContactoFormViewModel
    {
        [Required]
        public Contacto Contacto { get; set; } = new Contacto();

        //Listas para selects/radios/checkboxes
        public IEnumerable<SelectListItem> Regiones { get; set; } = new List<SelectListItem>();

        public IEnumerable<SelectListItem> AreaInteresOptions { get; set;} = new List<SelectListItem>();

        //Para multi-select de areas de interes (banderas)
        public int[]? AreaInteresSeleccionadas { get; set; }


    }
}
