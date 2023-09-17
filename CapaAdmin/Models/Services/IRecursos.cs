namespace CapaAdmin.Models.Services
{
    public interface IRecursos
    {
        public string GetSHA2S6(string str);

        public bool SendEmail(string email, string subject, string mensaje);

        public string GetPassword();

    }
}
