using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Models
{
    public class Clasificacion
    {
        [Key]
        [DisplayName("cla_id")]
        public int cla_id { get; set; }

        [Required, StringLength(50)]
        public string cla_nombre { get; set; }

        public virtual List<Empresa> empresas { get; set; } = new List<Empresa>();
    }
}
