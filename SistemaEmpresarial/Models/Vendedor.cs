using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEmpresarial.Models
{
    public class Vendedor
    {
        [Display(Name = "ven_id")]
        public int ven_id { get; set; }

        [Display(Name = "ven_nombre")]
        [Required, StringLength(50)]
        public string ven_nombre { get; set; }

        [Display(Name = "ven_paterno")]
        public string ven_paterno { get; set; }

        [Display(Name = "ven_materno")]
        public string ven_materno { get; set; }

        [Display(Name = "ven_sucursalsuc_id")]
        [ForeignKey("ven_sucursalsuc_id")]        
        public int? ven_sucursalsuc_id { get; set; }

        public virtual Sucursal ven_sucursal { get; set; }

        [Display(Name = "ven_rol")]
        public char ven_rol { get; set; } //c.	Radio button codificando en 1 campo

        [Display(Name = "ven_clave_empleado")]
        public int ven_clave_empleado { get; set; }

        [Display(Name = "ven_clave_agente")]
        [StringLength(5)]
        public string ven_clave_agente { get; set; }

        [Display(Name = "ven_estatus")]
        public bool ven_estatus { get; set; } //i.	Switch

        [Display(Name = "ven_objetivo")]
        public double ven_objetivo { get; set; } //g.	Número
    }
}
