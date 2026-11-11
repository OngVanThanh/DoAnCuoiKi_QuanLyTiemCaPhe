
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace DoAnCuoiKi_QuanLyTiemCaPhe
{
    internal static class SecurityHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        // Tạo salt ngẫu nhiên
        public static string GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        // Hash mật khẩu bằng PBKDF2
        public static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Mật khẩu không được để trống.");

            byte[] saltBytes;
            try
            {
                // Cố gắng giải mã nếu salt chuẩn Base-64
                saltBytes = Convert.FromBase64String(salt);
            }
            catch (FormatException)
            {
                // Nếu không phải Base-64, coi như salt là chuỗi text thuần túy (Plain text)
                saltBytes = Encoding.UTF8.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        // Kiểm tra mật khẩu nhập vào
        public static bool VerifyPassword(string password, string passwordHash, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(salt)) return false;

            try
            {
                string inputHash = HashPassword(password, salt);

                byte[] expected = Convert.FromBase64String(passwordHash);

                byte[] actual = Convert.FromBase64String(inputHash);

                if (expected.Length != actual.Length) return false;

                int difference = 0;

                for (int i = 0; i < expected.Length; i++)
                    difference |= expected[i] ^ actual[i];

                return difference == 0;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
