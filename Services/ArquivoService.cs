using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;


using RSAConsole.Models;


namespace RSAConsole.Services {

    public class ArquivoService {

        string path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data",
            "usuarios.json"
        );
        string pathData = Path.Combine(
            Directory.GetCurrentDirectory(),
            "Data"
            );

        public void  SalvarUsuarios(List<Usuario> usuarios) {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            string json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }



        public List<Usuario> CarregarUsuarios() {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            if (!File.Exists(path)) {
                return new List<Usuario>();
            }

            string json = File.ReadAllText(path);


            if (string.IsNullOrWhiteSpace(json)) {
                return new List<Usuario>();
            }

            List<Usuario>? usuarios = JsonSerializer.Deserialize<List<Usuario>>(json);

            return usuarios ?? new List<Usuario>();
        }
    }
}