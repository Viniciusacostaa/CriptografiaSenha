using System;

namespace RSAConsole.Models {
    public class Usuario {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string ChavePublica { get; set; } = string.Empty;
        public string ChavePrivada { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
    }
}