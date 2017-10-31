using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Users;
using FuckThisNumber.Interfaces.Repository;

namespace Business.Stores
{
    public class UserStore
    {
        protected readonly IAsyncRepository _repository;
        private IQueryable<AppUser> UsersQueriable => _repository.Entities<AppUser>();

        public UserStore(IAsyncRepository repository)
        {
            _repository = repository;
        }

        public AppUser GetUserByEmail(string email)
        {
            return UsersQueriable.FirstOrDefault(x => x.Email == email);
        }


    }
}
