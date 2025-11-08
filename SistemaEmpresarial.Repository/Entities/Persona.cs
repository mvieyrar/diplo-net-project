using System.ComponentModel.DataAnnotations;

namespace SistemaEmpresarial.Repository.Entities

{
    public class Persona
    {
        [Key]
        [Display(Name = "per_id")]
        public int per_id { get; set; }

        [Display(Name = "per_nombre")]
        public string per_nombre { get; set; }
        [Display(Name = "per_rfc")]
        public string per_rfc { get; set; }
        [Display(Name = "per_correo")]
        public string per_correo { get; set; }
        [Display(Name = "per_movil")]
        public string per_movil { get; set; }

        [Display(Name = "per_fecha_creacion")]
        [DataType(DataType.Date)]
        public DateTime per_fecha_creacion { get; set; } = DateTime.Now;
    }
}
