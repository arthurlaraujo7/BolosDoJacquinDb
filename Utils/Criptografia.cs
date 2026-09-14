using System.Security.Cryptography;
using System.Text;

namespace BolosDoJacquinDb.Utils;

public static class Criptografia
{
    public static string GerarHash(string texto)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("X2"));
        }
        return builder.ToString();
    }

    public static bool VerificarHash(string texto, string hashEsperado)
    {
        var hashTexto = GerarHash(texto);
        return string.Equals(hashTexto, hashEsperado, StringComparison.OrdinalIgnoreCase);
    }
}