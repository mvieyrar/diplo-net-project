using SistemaEmpresarial.Infrastructure;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class VendedorIndexViewModel
    {
        public VendedorFilterViewModel Filtro { get; set; } = new VendedorFilterViewModel();

        public PaginatedList<Vendedor>? Resultados { get; set; }
    }
}
