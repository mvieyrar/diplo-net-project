using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models
{
    public class Sucursal
    {
        [Key]
        [DisplayName("suc_id")]
        public int suc_id { get; set; }

        [Required, StringLength(50)]
        public string suc_nombre { get; set; }

        public virtual List<Vendedor> vendedores { get; set; } = new List<Vendedor>();

    }
}
