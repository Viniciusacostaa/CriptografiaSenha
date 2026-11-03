using System;
using System.Security.Cryptography;
using RSAConsole.Models;
using System.Collections.Generic;

namespace RSAConsole.Services {

    public class AutenticacaoService {
        private List<Usuario> usuarios;

        public AutenticacaoService(List<Usuario> usuarios) {
            this.usuarios = usuarios;
        }


        public string GerarSalt() {
            byte[] secureBytes = RandomNumberGenerator.GetBytes(16);

            string base64Token = Convert.ToBase64String(secureBytes);

            return base64Token;

        }


        public string GerarHashSenha(string senha, string salt) {
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                senha,
                saltBytes,
                600000,
                HashAlgorithmName.SHA256,
                32
                );

            string hash64Token = Convert.ToBase64String(hash);

            return hash64Token;
        }


        public bool VerificarSenha(string senha, string salt, string hash) {

            string senhaProvisoria = GerarHashSenha(senha, salt);

            byte[] hashCalculado = Convert.FromBase64String(senhaProvisoria);
            byte[] hashArmazenado = Convert.FromBase64String(hash);

            return CryptographicOperations.FixedTimeEquals(
               hashCalculado,
               hashArmazenado
            );
        }

        public Usuario? AutenticarUsuario(string username, string senha) {

            Usuario? usuarioEncontrado = usuarios.Find(u => u.Username == username);

            if (usuarioEncontrado != null && VerificarSenha(senha, usuarioEncontrado.Salt, usuarioEncontrado.SenhaHash)) {

                return usuarioEncontrado;

            }
            else return null;
        }

    }
}
