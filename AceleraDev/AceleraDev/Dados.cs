using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AceleraDev
{
    class Dados
    {
        [JsonPropertyName("Acelera")]
        public string NumeroCasas { get; set; }
        public int Numero_casas { get; set; }
        public string Token { get; set; }
        public string Cifrado { get; set; }
        public string Decifrado { get; set; }
        public string Resumo_criptografico { get; set; }
    }
}
