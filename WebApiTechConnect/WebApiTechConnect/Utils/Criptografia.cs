namespace API_FitTrack.Utils
{
    public static class Criptografia
    {
        public static string GerarHash(string googleId)
        {
            return BCrypt.Net.BCrypt.HashPassword(googleId);
        }

        public static bool CompararHash(string googleIdForm, string googleIdBanco)
        {
            return BCrypt.Net.BCrypt.Verify(googleIdForm, googleIdBanco);
        }
    }
}
