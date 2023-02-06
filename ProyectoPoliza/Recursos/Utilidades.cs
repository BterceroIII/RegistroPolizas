using System.Security.Cryptography;
using System.Text;

namespace ProyectoPoliza.Recursos
{
    public class Utilidades
    {
        public static string EncriptarClave(string clave)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 has = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = has.ComputeHash(enc.GetBytes(clave));

                foreach (byte b in result )
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
