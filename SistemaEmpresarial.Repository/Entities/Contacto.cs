using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEmpresarial.Repository.Entities

{

    public enum TipoRedSocial
    {
        FACEBOOK = 1,
        TWITTER = 2,
        LINKEDIN = 3,
        INSTAGRAM = 4,
        WHATSAPP = 5
    }

    [Flags]    
    public enum AreaInteres
    {
        NONE = 0,
        TECNOLOGIA = 1 << 0, // 1
        SALUD = 1 << 1, // 2
        EDUCACION = 1 << 2, // 4
        DEPORTES = 1 << 3, // 8
        ARTE = 1 << 4, // 16
        MUSICA = 1 << 5, // 32
        CINE = 1 << 6, // 64
        LITERATURA = 1 << 7 // 128
    }
    public class Contacto
    {
        [Key]
        [Display(Name = "con_id")]
        public int con_id { get; set; }

        [Display(Name = "con_nombre")]
        [Required, StringLength(50)]
        public string con_nombre { get; set; }

        [Display(Name = "con_paterno")]
        public string con_paterno { get; set; }

        [Display(Name = "con_materno")]
        public string con_materno { get; set; }
        
        [Display(Name = "con_empresa")]
        public string con_empresa { get; set; }

        [Display(Name = "con_regionreg_id")]
        [ForeignKey("con_regionreg_id")]
        //public int con_region { get; set; } //a.	Dropdown List usando una lista de código
        public int? con_regionreg_id { get; set; }
        public virtual Region con_region { get; set; }
        
        [Display(Name = "con_red_social")]
        public string con_red_social { get; set; }
        
        [Display(Name = "con_tipo_red_social")]
        public TipoRedSocial con_tipo_red_social { get; set; } = TipoRedSocial.WHATSAPP; //b.	Dropdown List usando una Enumeración

        [Display(Name = "con_genero")]
        public char con_genero { get; set; } //c.	Radio button codificando en 1 campo


        [Display(Name = "con_disponibilidad_matutino")]
        public bool con_disponibilidad_matutino { get; set; } //d.	Checkboxes

        [Display(Name = "con_disponibilidad_vespertino")]
        public bool con_disponibilidad_vespertino { get; set; } //d.	Checkboxes

        [Display(Name = "con_area_interes")]
        public AreaInteres con_area_interes { get; set; } //e.	Lista de selección múltple

        [DataType(DataType.Date)]
        public DateTime con_fecha_creacion { get; set; } = DateTime.Now; //f.	Fecha

        [Display(Name = "con_peso")]
        public double con_peso { get; set; } //g.	Número

        [Display(Name = "con_comentario")]
        [DataType(DataType.MultilineText)]
        public string? con_comentario { get; set; } //h.	TextArea

        [Display(Name = "con_estatus")]
        public bool con_estatus { get; set; } //i.	Switch
        

    }
}
