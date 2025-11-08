using SistemaEmpresarial.Infrastructure;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class ContactoIndexViewModel
    {
        public ContactoFilterViewModel Filtro { get; set; } = new ContactoFilterViewModel();

        public PaginatedList<Contacto>? Resultados { get; set; }
    }
}
