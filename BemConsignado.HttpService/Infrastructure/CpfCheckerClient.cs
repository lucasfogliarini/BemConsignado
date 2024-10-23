namespace BemConsignado.HttpService.Infrastructure
{
    public class CpfCheckerClient : ICpfCheckerClient
    {
        public bool IsActive(string cpf)
        {
            return true;
        }
    }

    public interface ICpfCheckerClient
    {
        bool IsActive(string cpf);
    }
}
