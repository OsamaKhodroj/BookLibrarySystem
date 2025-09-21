namespace BookLibrary.Infrastrcature
{
    public class Encryption
    {
        public static string Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException($"{nameof(text)} is required");

            var result = BCrypt.Net.BCrypt.HashPassword(text);
            return result;
        }

        public static bool Verify(string text , string hash)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException($"{nameof(text)} is required");

            if (string.IsNullOrEmpty(hash))
                throw new ArgumentNullException($"{nameof(hash)} is required");

            var valid = BCrypt.Net.BCrypt.Verify(text, hash);
            return valid;
        }
    }
}
