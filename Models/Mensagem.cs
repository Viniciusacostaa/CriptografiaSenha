using System;

namespace RSAConsole.Models {
    public class Mensagem {
        public int Id { get; set; }
        public int RemetenteId { get; set; }
        public int DestinatarioId { get; set; }
        public string ConteudoCriptografado { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; }
    }
}