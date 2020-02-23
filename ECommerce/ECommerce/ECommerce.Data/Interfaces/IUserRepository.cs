using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Interfaces
{
    public interface IUserRepository:IRepository<Entities.User>
    {
        Entities.User GetByEmailAndPassword(string email,string password);
    }
}
