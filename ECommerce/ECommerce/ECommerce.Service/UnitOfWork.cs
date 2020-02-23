using ECommerce.Data.Contexts;
using ECommerce.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get ; set ; }
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
            UserRepository = new UserRepository(_dataContext);
        }
        
        public int Complete()
        {
            return _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
