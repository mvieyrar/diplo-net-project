using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemaEmpresarial.Core.Dtos
{    
    public class RegionDto
    {
        [JsonPropertyName("Id")]
        public int reg_id { get; set; }

        public string reg_nombre { get; set; }
        [JsonIgnore]
        public DateTime reg_fecha { get; set; } = DateTime.Now;
    }
}
