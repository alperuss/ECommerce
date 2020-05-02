namespace ECommerce.Data.Interfaces
{
    public interface IUserRepository : IRepository<Entities.User>
    {
        Entities.User GetByEmailAndPassword(string email, string password);
        Entities.User GetByEmail(string email);
        Entities.User GetByAutoLoginKey(System.Guid autoLoginKey);
        Entities.User GetById(int id);
    }
}
