using System;
using System.Security.Cryptography;
using RSAConsole.Services;
using RSAConsole.Models;
using System.Collections.Generic;

class Program {
    static void Main() {
        Console.WriteLine("---------Sistema de criptografia---------");
        Console.WriteLine("\n1 - Cadastrar Usuario\n2 - Entrar na conta\n3 - Listar Usuarios\n4 - Criptografar mensagem\n5 - Descriptografar mensagem\n6 - Sair\n");
        Console.WriteLine("Escolha uma opção: ");

        int opcao;

        while (!int.TryParse(Console.ReadLine(), out opcao) || opcao < 1 || opcao > 6) {
            Console.WriteLine("Opção inválida!\nDigite novamente: ");
        }

        Console.WriteLine($"Você escolheu a opção: {opcao}");

        Usuario u = new Usuario();
        ArquivoService arquivo = new ArquivoService();
        List<Usuario> usuarios = arquivo.CarregarUsuarios();
        AutenticacaoService aunt = new AutenticacaoService(usuarios);

        if (opcao == 1) {
            u.Id = usuarios.Count + 1;

            Console.Write("Nome: ");
            u.Nome = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(u.Nome)) {
                Console.WriteLine("Formato incorreto.\nDigite novamente: ");
                u.Nome = Console.ReadLine() ?? string.Empty;
            }

            Console.Write("\nUsername: ");
            u.Username = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(u.Username) || usuarios.Exists(x => x.Username == u.Username)) {
                Console.WriteLine("Username inválido ou já utilizado.\nDigite novamente: ");
                u.Username = Console.ReadLine() ?? string.Empty;
            }

            Console.Write("\nSenha: ");
            string senha = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(senha)) {
                Console.WriteLine("Formato incorreto.\nDigite novamente: ");
                senha = Console.ReadLine() ?? string.Empty;
            }

            u.Salt = aunt.GerarSalt();
            u.SenhaHash = aunt.GerarHashSenha(senha, u.Salt);
            u.DataCadastro = DateTime.UtcNow;

            usuarios.Add(u);
            arquivo.SalvarUsuarios(usuarios);
        }

        // FIM OPCAO 1

        // INICIO OPCAO 2
        if (opcao == 2) {
            Console.Write("\nDigite seu username: ");
            string username = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrEmpty(username)) {
                Console.WriteLine("Username inválido.\nDigite novamente: ");
                username = Console.ReadLine() ?? string.Empty;
            }



            Console.Write("\nSenha: ");
            string senha = Console.ReadLine() ?? string.Empty;

            while (string.IsNullOrWhiteSpace(senha)) {
                Console.WriteLine("Formato incorreto.\nDigite novamente: ");
                senha = Console.ReadLine() ?? string.Empty;
            }

            Usuario? usuarioLogado = aunt.AutenticarUsuario(username, senha);

            if (usuarioLogado != null) {
                Console.WriteLine("Login correto.");
                Console.Write("\nBem-vindo, " + usuarioLogado.Nome);
            }
            else Console.WriteLine("Login incorreto");
        }
        // FIM OPCAO 2
    } // FIM DO MÉTODO MAIN

}