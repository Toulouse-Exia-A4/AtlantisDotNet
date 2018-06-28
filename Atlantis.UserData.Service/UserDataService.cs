using Atlantis.UserData.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Atlantis.UserData.Service
{
    public class UserDataService : IUserDataService
    {
        public List<User> GetAllUsers()
        {
            using (var ctx = new UserDataContext())
            {
                var dao = new UserDAO(ctx);

                return dao.All();
            }
        }
    }
}
