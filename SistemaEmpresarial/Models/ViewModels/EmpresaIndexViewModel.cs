using SistemaEmpresarial.Infrastructure;

namespace SistemaEmpresarial.Models.ViewModels
{
    public class EmpresaIndexViewModel
    {
        public EmpresaFilterViewModel Filtro { get; set; } = new EmpresaFilterViewModel();

        public PaginatedList<Empresa>? Resultados { get; set; }
    }
}
