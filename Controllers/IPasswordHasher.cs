namespace PoolGameAPI.Controllers
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        private const int SaltSize = 16;
        private const int HashSize = 16;
        private const int Iterations = 100000;
    }
}
