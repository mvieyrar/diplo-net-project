using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEmpresarial.Repository.Entities

{
    public class Empresa
    {
        [Key]
        [Display(Name = "emp_id")]
        public int emp_id { get; set; }

        [Display(Name = "emp_nombre")]
        [Required, StringLength(50)]
        public string emp_nombre { get; set; }

        [Display(Name = "emp_tipo")]
        public char emp_tipo { get; set; } //c.	Radio button codificando en 1 campo

        [Display(Name = "emp_pais")]
        public string emp_pais { get; set; }

        [Display(Name = "emp_cp")]
        public string emp_cp { get; set; }

        [Display(Name = "emp_poblacion")]
        public string emp_poblacion { get; set; }

        [Display(Name = "emp_provincia")]
        public string emp_provincia { get; set; }

        [Display(Name = "emp_direccion")]
        public string emp_direccion { get; set; }

        [Display(Name = "emp_telefono")]
        public string emp_telefono { get; set; }

        [Display(Name = "emp_observaciones")]
        [DataType(DataType.MultilineText)]
        public string? emp_observaciones { get; set; } //h.	TextArea

        [Display(Name = "emp_clasificacioncla_id")]
        [ForeignKey("emp_clasificacioncla_id")]
        public int? emp_clasificacioncla_id { get; set; }

        public virtual Clasificacion emp_clasificacion { get; set; }


        [Display(Name = "emp_web")]
        public string emp_web { get; set; }

        [Display(Name = "emp_latitud")]
        public double emp_latitud { get; set; } //g.	Número

        [Display(Name = "emp_longitud")]
        public double emp_longitud { get; set; } //g.	Número

        [DataType(DataType.Date)]
        public DateTime emp_fecha_creacion { get; set; } = DateTime.Now; //f.	Fecha

        [Display(Name = "emp_estatus")]
        public bool emp_estatus { get; set; } //i.	Switch
    }
}
