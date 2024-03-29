﻿using System.Security.Cryptography;
using System.Text;

namespace Domain
{
    public static class Utils
    {
        public static bool ValidateCpf(long cpf)
        {
            string cpfString = cpf.ToString("D11"); // Converte o long em uma string de 11 dígitos

            // Verificar o comprimento
            if (cpfString.Length != 11)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpfString.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpfString.EndsWith(digito);
        }

        public static string EncryptKey(string key)
        {
            // Lógica para criptografar a chave
            // Implementação específica de criptografia
            // Exemplo simplificado usando a classe MD5
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(key);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}