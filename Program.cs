using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeradorSenhaSuporte
{
    class Program
    {
        // ── CHAVE SECRETA ─────────────────────────────────────────────
        // Esta string só existe no código-fonte (não fica no banco,
        // nem em arquivo, nem visível ao cliente).
        // Troque por algo único para o seu sistema.
        private const string CHAVE_SECRETA = "Hortifruti-2026-#Suporte!";

        static void Main()
        {
            Console.WriteLine("=== Gerador de Senha de Desbloqueio ===\n");

            Console.Write("Mês (1-12): ");
            int mes = int.Parse(Console.ReadLine());

            Console.Write("Ano (ex: 2026): ");
            int ano = int.Parse(Console.ReadLine());

            string senha = GeradorSenha(mes, ano);

            Console.WriteLine($"\nSenha para {mes:D2}/{ano}: {senha}");
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }

            /// <summary>
            /// Gera a senha de desbloqueio para um determinado mês/ano.
            /// Use esta função em uma ferramenta separada (ex: um pequeno
            /// console app) para gerar a senha que você passa ao cliente.
            /// </summary>
        public static string GeradorSenha(int mes, int ano)
            {
                // Monta a string base: "MM-yyyy" + chave secreta
                string base_ = $"{mes:D2}-{ano}-{CHAVE_SECRETA}";

                // Gera hash SHA256
                using (SHA256 sha = SHA256.Create())
                {
                    byte[] hashBytes = sha.ComputeHash(
                        Encoding.UTF8.GetBytes(base_));

                    // Converte para string hexadecimal
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                        sb.Append(b.ToString("x2"));

                    string hashCompleto = sb.ToString();

                    // Pega só os primeiros 8 caracteres + formata
                    // Ex: "A3F92B1C"
                    return hashCompleto.Substring(0, 8).ToUpper();
                }
            }
    }
}
