using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemaEmpresarial.Core.Dtos
{
    public class SucursalDto
    {
        [JsonPropertyName("Id")]
        public int suc_id { get; set; }

        public string suc_nombre { get; set; }

        public DateTime suc_fecha { get; set; } = DateTime.Now;

    }
}
