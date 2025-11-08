using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEmpresarial.Repository.Entities

{
    [Table("region")]
    public class Region
    {
        [Key]
        [DisplayName("reg_id")]
        public int reg_id { get; set; }

        [Required, StringLength(50)]
        public string reg_nombre { get; set; }

        public virtual List<Contacto> contactos { get; set; } = new List<Contacto>();
    }
}
